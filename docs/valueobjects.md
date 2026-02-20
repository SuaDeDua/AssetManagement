✦ Đây là một câu hỏi cốt lõi trong thiết kế Domain-Driven Design (DDD). Ranh giới giữa Value Object (VO) và Primitive Type (kiểu nguyên thủy: string, int,
  decimal...) đôi khi rất mong manh.

  Dưới đây là "Bộ lọc 3 câu hỏi" giúp bạn quyết định chính xác 100%:

  Câu hỏi 1: Nó có Logic đi kèm không? (Validation/Format)
   * Có: -> Value Object.
       * Ví dụ: Email (phải có @), PhoneNumber (chỉ số), Money (không âm, làm tròn), ZipCode (5-7 ký tự).
       * Tại sao? Vì bạn không muốn code validation if (email.Contains("@")) lặp lại rải rác ở 50 chỗ khác nhau. VO gói gọn nó vào 1 chỗ.
   * Không (Chứa gì cũng được): -> Primitive Type.
       * Ví dụ: Comment (người dùng thích chửi bậy hay gõ teencode gì kệ họ), Note.

  Câu hỏi 2: Nó có cấu trúc phức hợp không? (Nhiều field nhỏ)
   * Có: -> Value Object.
       * Ví dụ: Address (Đường, Phường, Quận), FullName (Họ, Tên, Đệm), Money (Số tiền + Loại tiền), DateRange (Từ ngày + Đến ngày).
       * Tại sao? VO giúp bạn truyền 1 cục Address thay vì truyền 4-5 tham số rời rạc vào hàm.
   * Không (Chỉ 1 field đơn): -> Xem tiếp câu 3.

  Câu hỏi 3: Nó có định danh riêng (Identity) không?
   * Có (Quan trọng là AI, không phải là CÁI GÌ): -> Entity.
       * Ví dụ: User (Đổi tên vẫn là user đó), Asset (Sơn lại màu vẫn là máy đó).
   * Không (Quan trọng là GIÁ TRỊ): -> Value Object.
       * Ví dụ: Tờ tiền 500k. Bạn đổi tờ này lấy tờ khác, miễn là vẫn 500k là được (VO). Hai địa chỉ giống hệt nhau thì coi là một (VO).

  ---

  Bảng Cheatsheet cho dự án Asset Management của bạn:


  ┌───────────────────────┬───────────────────┬───────────────────────────────────────────────────────────────────┐
  │ Dữ liệu               │ Quyết định        │ Lý do                                                             │
  ├───────────────────────┼───────────────────┼───────────────────────────────────────────────────────────────────┤
  │ Tên tài sản (Name)    │ VO (`Name`)         │ Cần validate độ dài, ký tự đặc biệt, trim space.                  │
  │ Mô tả (Description)   │ VO (`Description`)  │ Cần giới hạn max length (ví dụ 500 ký tự).                        │
  │ Số serial (Serial No) │ VO (`SerialNumber`) │ Logic phức tạp (format, unique format).                           │
  │ Giá tiền (Cost)       │ VO (`Money`)        │ Đa tiền tệ (VND, USD), không âm.                                  │
  │ Email nhân viên       │ VO (`Email`)        │ Validate định dạng.                                               │
  │ Asset Tag (Mã TS)     │ VO (`AssetTag`)     │ Format riêng của công ty (VD: AST-001).                           │
  │ Ghi chú (Note)        │ String            │ Tùy ý nhập, không quan trọng format.                              │
  │ Số lượng (Quantity)   │ Int               │ Nếu chỉ là số đếm đơn giản. (Dùng VO Quantity nếu cần logic > 0). │
  │ Ngày tháng (Date)     │ DateTime/DateOnly │ Đủ dùng rồi. (Dùng VO Period nếu có Start/End Date).              │
  └───────────────────────┴───────────────────┴───────────────────────────────────────────────────────────────────┘


  Quy tắc vàng:
  "Khi nghi ngờ, hãy bắt đầu bằng Primitive Type (String/Int). Khi thấy logic validation lặp lại lần thứ 2 -> Refactor thành Value Object."
