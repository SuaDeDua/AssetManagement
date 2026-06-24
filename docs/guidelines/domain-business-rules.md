# Sổ Tay Nghiệp Vụ & Kiến Trúc Domain (Assetly)

Tài liệu này tổng hợp toàn bộ các quyết định kiến trúc cốt lõi, ranh giới ngữ cảnh (Bounded Contexts) và luật kinh doanh (Business Rules) đã được thống nhất cho dự án Assetly, áp dụng các tiêu chuẩn Enterprise của Domain-Driven Design (DDD).

## 1. Ranh Giới Module & Aggregate Root

Hệ thống được chia thành các Module độc lập, mỗi Module chịu một trách nhiệm rạch ròi. Không Module nào được phép ghi đè dữ liệu của Module khác trực tiếp.

### 1.1. Phân biệt Dữ liệu Danh mục (Catalog) vs Dữ liệu Vật lý
*   **Danh mục (Global Master Data):** `Manufacturer`, `Category`, `Location`, `AssetModel`.
    *   *Quy tắc:* Vòng đời độc lập, không gắn liền với `UserId`. Admin thiết lập 1 lần, mọi người dùng chung.
    *   *Liên kết:* Trỏ tới nhau bằng ID (`Guid ManufacturerId`), tuyệt đối không lưu Object lồng nhau.
*   **Vật lý (Physical Instance):** `Asset` (Tài sản thực tế cầm trên tay).
    *   *Quy tắc:* Bắt buộc phải có `AssignedUserId` (Ai đang cầm) và `Status` (Available, InUse, Broken) để làm "Ổ khóa tồn kho" (Inventory Lock), chống cấp phát trùng lặp.

### 1.2. Phân chia 3 Trụ cột Vận hành (Workflow)
*   **Module `Ticketing` (Quy trình/Con người):** Quản lý luồng xin phép, chat hỗ trợ, sếp duyệt. Cấu trúc linh hoạt, chứa nhiều logic giao tiếp.
*   **Module `Assignments` (Biên bản/Pháp lý):** Đóng vai trò "Phòng công chứng". Quản lý `HandoverRecord` (Biên bản), sinh mã QR, lưu chữ ký điện tử và lịch sử ai đã từng cầm máy.
*   **Module `Assets` (Tồn kho/Vật lý):** Đóng vai trò "Thủ kho". Giữ trạng thái của thiết bị hiện tại.

*Luồng chạy chuẩn:* User tạo Ticket -> IT phê duyệt -> Sinh mã QR Biên bản (Assignments) -> User quét mã ký xác nhận -> Bắn Event -> Assets cập nhật tồn kho & Ticketing đóng luồng. (Xử lý mượt mà cả Onboarding không cần Ticket).

## 2. Luật Kinh Doanh Về Loại Tài Sản (Asset Classification)

### 2.1. Dynamic Categories (Bảng Database)
Những danh mục dễ sinh sôi nảy nở như "Laptop", "Màn hình", "Bàn ghế" được lưu dưới dạng Aggregate Root trong bảng `Categories`. Admin có toàn quyền Thêm/Sửa/Xóa trên giao diện.

### 2.2. Smart Enum: `AssetType` (Gắn liền với Business Rule)
Những loại tài sản làm thay đổi luồng chạy của code phải được cấu hình bằng C# Smart Enum (Ví dụ: class `AssetType : SmartEnum<AssetType>`).

**Luật nghiệp vụ cốt lõi:**
1.  **Primary Asset (Thiết bị chính - Laptop/Desktop):**
    *   `RequiresFormalHandover = true`: Bắt buộc đi qua luồng tạo Biên bản, quét mã QR và ký chữ ký điện tử.
    *   `RevokesItSubsidy = true`: Tự động cắt trợ cấp thiết bị (BYOD) của nhân sự.
2.  **Accessory (Phụ kiện - Chuột, Bàn phím):**
    *   `RequiresFormalHandover = false`: Giao thẳng, cập nhật DB, không cần ký biên bản rườm rà.
    *   `RevokesItSubsidy = false`: Nhân sự vẫn giữ nguyên các khoản trợ cấp khác.

### 2.3. Custom Fields (Độ phân giải siêu cao)
*   Không dùng mô hình EAV lằng nhằng.
*   Mỗi `AssetModel` chứa một `FieldSetId` quy định form nhập liệu (Ví dụ: Model MacBook M3 bắt nhập Neural Engine, Model Dell bắt nhập Card rời).
*   Thực thể `Asset` lưu cấu hình thực tế vào cột `CustomFieldValues` định dạng **JSONB** (PostgreSQL) để tốc độ Query (Dapper) đạt tối đa và không vỡ Schema Database.

## 3. Kiến Trúc Hướng Sự Kiện (Event-Driven Integration)

Triệt để tuân thủ nguyên tắc: Các Module không "thò tay" vào túi nhau. Giao tiếp qua Integration Events.

**Ví dụ: Bài toán Cắt trợ cấp (Subsidies)**
1.  Command Handler cấp máy thuộc Module `Assets` thực hiện cập nhật `AssignedUserId`.
2.  Nó kiểm tra `asset.Type.RevokesItSubsidy`. Nếu `true`, nó KHÔNG gọi DB của bảng lương. Nó dùng Message Broker bắn ra `EmployeeItSubsidyRevokedIntegrationEvent(UserId)`.
3.  Module `Payroll` (Kế toán/Nhân sự) có một Background Handler lắng nghe Event này. Khi nghe thấy, nó tự động lấy hồ sơ lương của User ra và cắt phụ cấp. 
*(Lợi ích: Nếu Module Kế toán sập hoặc rớt mạng mạng, Event được lưu ở Outbox và tự động Retry. Cấp máy vẫn thành công, báo cáo kế toán sẽ được đồng bộ sau - Eventual Consistency).*

## 4. Phân Quyền (Permission-Based Access Control - PBAC)

Từ bỏ cách phân quyền cứng theo chức danh (RBAC - `[Authorize(Roles="IT")]`). Áp dụng mô hình ma trận động như Discord.

*   **API Security:** Khóa API bằng Hành động: `[HasPermission("Assets.Create")]`.
*   **Database:** Có bảng `Permissions` (Cố định), `Roles` (Động), và bảng nối `Role_Permissions`.
*   **Dynamic Role:** Tài khoản Super Admin có thể tự do tạo Role mới (Ví dụ: "Thực tập sinh") và tick chọn phân quyền cho Role đó ngay trên giao diện mà không cần Deploy lại code.
*   **Hiệu năng:** Sau khi User đăng nhập, danh sách Permissions được nhúng thẳng vào JWT Token dưới dạng Claims. API xác thực siêu tốc trên RAM không cần Query Database.

---
*Lưu ý cho Developer:* 
*   LUÔN DÙNG **`Result` + `Error` record** ở tầng Application để bắt lỗi nghiệp vụ (Không dùng Exception).
*   LUÔN DÙNG **`GlobalExceptionHandlingMiddleware`** để chụp lỗi hệ thống 500 hoặc các lỗi Validation do `Guard` (Exception) gây ra từ tầng Domain.
*   Domain Event ưu tiên dùng **Thin Event** (Chỉ chứa ID) để giảm thiểu độ phình to JSON và tránh rò rỉ dữ liệu.
