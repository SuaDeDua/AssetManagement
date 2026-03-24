 1. Module Users (Nhân sự & Tổ chức)
   * Trách nhiệm: Quản lý cơ cấu công ty, sơ đồ phòng ban và thông tin tài khoản nhân viên. (Nền tảng để phân quyền và xác định ai đang cầm thiết bị).
   * Các Aggregate/Entities chính:
       * User (Nhân viên, IT Admin)
       * Company (Công ty/Chi nhánh cấp cao nhất)
       * Department (Phòng ban/Nhóm - sử dụng ParentId để phân cấp)

  2. Module Catalogs (Danh mục & Định nghĩa)
   * Trách nhiệm: Quản lý cấu hình tĩnh, thư viện danh mục, mẫu mã thiết bị. Dữ liệu ở đây ít thay đổi, đóng vai trò như "Từ điển" cho hệ thống.
   * Các Aggregate/Entities chính:
       * AssetModel (Mẫu máy - VD: MacBook Pro M2)
       * Category (Chủng loại - VD: Laptop)
       * Manufacturer (Hãng sản xuất - VD: Apple)
       * Bộ cấu hình động (EAV): FieldSet, CustomField, FieldItem

  3. Module Assets (Quản lý Kho & Tài sản vật lý)
   * Trách nhiệm: Theo dõi vòng đời vật lý của thiết bị. Trả lời câu hỏi "Cái máy này số serial bao nhiêu, đang nằm ở đâu?".
   * Các Aggregate/Entities chính:
       * Asset (Thực thể trung tâm mang AssetTag)
       * Location (Kho bãi, văn phòng - sử dụng ParentId để phân cấp Thành phố -> Địa chỉ)

  4. Module Transactions (Giao dịch & Quy trình)
   * Trách nhiệm: Quản lý dòng chảy công việc, giấy tờ pháp lý, yêu cầu cấp phát và hóa đơn. Đây là trái tim quy trình (Workflow) của hệ thống.
   * Các Aggregate/Entities chính:
       * HandoverTicket (Phiếu bàn giao giữa Sender và Receiver)
       * AssetRequest (Phiếu xin mượn/cấp thiết bị - Dự kiến phát triển thêm)
       * OrderInformation (Thông tin đơn mua sắm thiết bị)
       * Supplier (Nhà cung cấp)

  ---

  Phần Phụ trợ (Không phải Module nghiệp vụ)
   * SharedKernel (Lõi dùng chung): Chứa các Base Classes (Entity, Result, IDomainEvent) và các Value Objects phổ quát (EmailAddress, Money, CityInfo).
     Các Module phía trên đều được phép Reference (tham chiếu) đến phần này.
