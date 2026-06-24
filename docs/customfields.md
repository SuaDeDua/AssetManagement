Để hiểu **tường tận nhất** về bài toán `CustomField` và `FieldSet`, chúng ta cần đi sâu vào cách bóc tách dữ liệu động (Dynamic Data) trong hệ thống quản lý tài sản. Đây là phần khó nhất, nhưng nếu làm đúng, hệ thống của bạn sẽ linh hoạt như Jira hay Asset Panda.

Sự bổ sung **`FieldSet` (Tập hợp các trường tùy chỉnh)** của bạn là một bước đi mang tầm kiến trúc sư. Nó giải quyết được bài toán **Tái sử dụng (Reusability)** tuyệt vời.

Chúng ta hãy mổ xẻ tường tận theo 3 khía cạnh: **Mô hình hóa (DDD), Luồng hoạt động, và Lưu trữ (Database).**

---

### 1. Phân tích mô hình (Domain Model)

Thay vì gắn trực tiếp Custom Field vào `AssetModel`, chúng ta tách ra thành các mảnh ghép độc lập:

#### A. Mảnh ghép 1: `FieldSet` và `CustomField`
*   **`FieldSet` (Aggregate Root):** Là một bộ khuôn (template) chứa nhiều trường dữ liệu có chung một ngữ cảnh.
    *   *Ví dụ:* FieldSet có tên là `"Thông số Máy tính"` (Computer Specs).
*   **`CustomField` (Entity / Value Object nằm trong FieldSet):** Định nghĩa cấu trúc của một trường dữ liệu đơn lẻ.
    *   *Ví dụ:* Nằm trong FieldSet `"Thông số Máy tính"`, ta có các CustomField:
        1. `Name`: "RAM", `DataType`: Number, `Unit`: "GB", `IsRequired`: true.
        2. `Name`: "CPU", `DataType`: Text, `IsRequired`: true.
        3. `Name`: "Có Card rời không?", `DataType`: Boolean, `IsRequired`: false.

#### B. Mảnh ghép 2: `AssetModel`
`AssetModel` giờ đây sẽ cực kỳ gọn nhẹ. Nó chỉ cần tham chiếu (tham chiếu khóa ngoại) đến một hoặc nhiều `FieldSet`.
*   *Ví dụ 1:* `AssetModel` **"MacBook Pro M3"** sẽ gắn với `FieldSet` `"Thông số Máy tính"`.
*   *Ví dụ 2:* `AssetModel` **"Toyota Vios 2024"** sẽ gắn với `FieldSet` `"Thông số Xe Cộ"` (gồm: Biển số, Số khung, Số máy).
*   *Lợi ích cực lớn:* Nếu ngày mai công ty quyết định tất cả máy tính phải theo dõi thêm "Địa chỉ MAC", admin chỉ cần vào sửa `FieldSet` "Thông số Máy tính" thêm trường "MAC Address". Lập tức *toàn bộ* các `AssetModel` dùng FieldSet này (MacBook, Dell, HP) đều tự động có thêm trường này!

#### C. Mảnh ghép 3: `Asset` (Tài sản thực tế) và `FieldValue`
`Asset` là nơi duy nhất lưu trữ **Giá trị thực tế (Data Value)**. Nó không quan tâm trường đó tên là gì, kiểu gì (việc đó của FieldSet lo). Nó chỉ lưu cặp Key-Value.
*   *Ví dụ:* Chiếc MacBook có mã `IT-001` sẽ lưu Data Value dưới dạng JSON: `{"RAM": 16, "CPU": "Apple M3", "Có Card rời không?": false}`.

---

### 2. Luồng hoạt động (Workflow) và Validation (Kiểm tra tính đúng đắn)

Sức mạnh của DDD nằm ở việc kiểm soát logic (Validation). Khi một nhân viên nhập thông tin cho tài sản mới, hệ thống sẽ hoạt động như sau:

1. **Hiển thị Form (UI/Frontend):**
   * Người dùng chọn tạo tài sản thuộc Model `"MacBook Pro M3"`.
   * Frontend gọi API xuống lấy `AssetModel`. API thấy Model này gắn với `FieldSet` `"Thông số Máy tính"`.
   * API trả về định nghĩa các `CustomField` (RAM, CPU...). Frontend dựa vào đó tự động vẽ ra các ô input (ô nhập số cho RAM, ô text cho CPU, ô checkbox cho Card rời).

2. **Lưu dữ liệu (Backend / Domain Logic):**
   * Frontend gửi data lên: `{"RAM": "Mười Sáu", "CPU": "M3"}`.
   * **Trong Entity `Asset`**, hàm `SetCustomFieldValues(Dictionary<string, object> rawValues, FieldSet fieldSet)` sẽ được gọi.
   * Logic kiểm tra (Validation Rule) chạy:
     * *Check 1:* Hàm duyệt qua `FieldSet`. Nó thấy trường "RAM" yêu cầu kiểu `Number`. Nhưng data gửi lên là chuỗi `"Mười Sáu"`. -> **Bắn lỗi (Domain Exception): "RAM phải là số".**
     * *Check 2:* Nó thấy trường "Có Card rời không?" thiếu trong payload, nhưng trường này `IsRequired = false`. -> **Bỏ qua, gán giá trị mặc định.**

---

### 3. Cấu trúc Database (Cách lưu trữ tối ưu nhất)

Bài toán Custom Field thường làm sập hệ thống (chậm) nếu thiết kế Database sai (như dùng mô hình EAV - nhiều bảng join với nhau). Để tối ưu cả tốc độ đọc và sự linh hoạt, đây là cách thiết kế chuẩn cho .NET + PostgreSQL/SQL Server hiện nay:

**Bảng `FieldSets`**
| Id (PK) | Name | Description |
| :--- | :--- | :--- |
| FS_001 | Thông số Máy tính | Dùng cho Laptop, PC |

**Bảng `CustomFields` (Ràng buộc khóa ngoại với FieldSets)**
| Id (PK) | FieldSetId (FK)| Name | DataType | IsRequired | Options (JSON) |
| :--- | :--- | :--- | :--- | :--- | :--- |
| CF_01 | FS_001 | RAM | Number | True | null |
| CF_02 | FS_001 | Ổ cứng | Dropdown | True | ["256GB", "512GB"] |

**Bảng `AssetModels`**
| Id (PK) | Name | FieldSetId (FK) |
| :--- | :--- | :--- |
| AM_100 | MacBook Pro M3 | FS_001 |

**Bảng `Assets` (Đây là mấu chốt)**
| Id (PK) | AssetModelId (FK)| AssetTag | Status | **CustomData (Cột JSON / JSONB)** |
| :--- | :--- | :--- | :--- | :--- |
| AS_99 | AM_100 | IT-001 | InUse | `{"RAM": 16, "Ổ cứng": "512GB"}` |

#### Tại sao lại dùng cột JSON (`CustomData`) trong bảng `Assets`?
*   **Hiệu năng siêu tốc:** Không cần phải `JOIN` với bất kỳ bảng nào khác để lấy dữ liệu Custom Field của một chiếc máy. Truy vấn 1 phát ra luôn cả cục JSON.
*   **Query trực tiếp trên JSON:** Cả PostgreSQL và SQL Server hiện đại đều hỗ trợ query bên trong cột JSON.
    *   *Ví dụ (PostgreSQL):* Tìm tất cả tài sản có RAM lớn hơn 8GB:
        `SELECT * FROM Assets WHERE CustomData->>'RAM' > '8'`
*   Trong EF Core 8 (Entity Framework), cột JSON này có thể map trực tiếp thành một class hoặc `Dictionary<string, string>` rất dễ dàng bằng tính năng JSON Column Mapping.

Bạn đã thấy mô hình `FieldSet` kết hợp với cột JSON giải quyết trọn vẹn sự phức tạp này chưa? Nếu bạn thấy mô hình này ưng ý, bước tiếp theo tôi đề xuất là **viết thử mã nguồn C# thể hiện Aggregate `FieldSet` và `Asset`** để bạn hình dung rõ cách code trong Clean Architecture.
