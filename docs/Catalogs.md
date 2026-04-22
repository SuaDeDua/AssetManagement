Tầng **Catalogs** (Danh mục) là một module đặc thù. Dựa trên bản thiết kế ER Diagram của bạn, module này thiên về quản lý **Meta-data (Siêu dữ liệu)** cho hệ thống tài sản, bao gồm các cấu hình động (`FieldSet`, `CustomField`) và các danh mục phân loại (`Category`, `Manufacturer`, `AssetModel`).

Vì đặc tính của module này là **"Ít thay đổi (Low Churn), Nhiều người đọc (High Read)"** và **"Quan hệ cấu trúc chặt chẽ"**, cách phân chia thư mục trong tầng Domain (`Assetly.Modules.Catalogs.Domain`) cần phản ánh rõ các Aggregate Root (Thực thể gốc).

Dưới đây là cấu trúc thư mục tối ưu cho tầng Domain của module **Catalogs**, tuân thủ nguyên tắc Feature-based / Aggregate-based của Evently:

### Cấu trúc thư mục `Assetly.Modules.Catalogs.Domain`

```text
Assetly.Modules.Catalogs.Domain/
├── AssetModels/                        # Aggregate Root chính: Mẫu tài sản (VD: Dell XPS 15)
│   ├── AssetModel.cs                   # Thực thể trung tâm, liên kết tới Manufacturer, Category, FieldSet
│   ├── AssetModelErrors.cs
│   ├── IAssetModelRepository.cs
│   └── Events/
│       └── AssetModelCreatedDomainEvent.cs
│
├── Categories/                         # Aggregate Root: Danh mục tài sản (VD: Laptop, Bàn ghế)
│   ├── Category.cs
│   ├── CategoryErrors.cs
│   └── ICategoryRepository.cs
│
├── Manufacturers/                      # Aggregate Root: Nhà sản xuất (VD: Dell, Apple)
│   ├── Manufacturer.cs
│   ├── ManufacturerErrors.cs
│   └── IManufacturerRepository.cs
│
├── CustomFields/                       # Khối kiến trúc phức tạp nhất: Quản lý trường động
│   ├── FieldSets/                      # Aggregate Root: Tập hợp các trường (VD: Cấu hình Laptop)
│   │   ├── FieldSet.cs                 # Chứa danh sách các FieldItem (One-to-Many)
│   │   ├── FieldItem.cs                # Entity con (Child Entity) của FieldSet
│   │   ├── FieldSetErrors.cs
│   │   └── IFieldSetRepository.cs
│   │
│   └── Fields/                         # Aggregate Root: Định nghĩa 1 trường (VD: RAM, CPU)
│       ├── CustomField.cs              # Định nghĩa kiểu dữ liệu (Text, Number, Date...)
│       ├── CustomFieldErrors.cs
│       └── ICustomFieldRepository.cs
│
└── Shared/                             # (Tùy chọn) Các Value Object dùng chung trong Module Catalogs
    ├── Url.cs                          # Value Object cho Manufacturer.urlHomePage (Validate URL hợp lệ)
    └── ImagePath.cs                    # Value Object cho đường dẫn ảnh (AssetModel.image)
```

### Phân tích các quyết định thiết kế (Tại sao lại chia như vậy?)

#### 1. Tại sao `AssetModel` là một Aggregate Root riêng?
Theo ER của bạn, `AssetModel` chứa các khóa ngoại (`manufacturerId`, `categoryId`, `fieldSetId`).
*   **Nguyên tắc DDD:** Một Aggregate Root không được chứa toàn bộ Object của Aggregate Root khác, nó chỉ nên giữ **ID (Guid)**.
*   **Do đó:** Trong file `AssetModel.cs`, bạn sẽ khai báo các thuộc tính như `public Guid ManufacturerId { get; private set; }`, thay vì `public Manufacturer Manufacturer { get; private set; }`. Việc load dữ liệu liên kết sẽ do tầng Application (CQRS) đảm nhiệm khi cần query.

#### 2. Tại sao `FieldItem` lại nằm chung thư mục với `FieldSet`?
Nhìn vào ER Diagram: `fieldItem` sinh ra chỉ để tạo mối quan hệ giữa `fieldSet` và `customField`, đồng thời lưu thứ tự (`Order`).
*   **Quyết định DDD:** `FieldItem` **không phải** là một Aggregate Root độc lập. Nó không có ý nghĩa nếu đứng một mình. Nó là một **Child Entity** thuộc quyền quản lý của `FieldSet`.
*   **Thiết kế Code:** Bạn sẽ không có `IFieldItemRepository`. Mọi thao tác thêm/sửa/xóa `FieldItem` đều phải đi qua `FieldSet`.
    *   Ví dụ trong `FieldSet.cs`: `public void AddField(Guid customFieldId, int order) { ... }`

#### 3. Tại sao `CustomField` lại là Aggregate Root riêng?
*   Một `CustomField` (ví dụ: "Dung lượng RAM") có thể được sử dụng lại ở nhiều `FieldSet` khác nhau (vừa dùng cho Máy tính bàn, vừa dùng cho Laptop).
*   Vì nó tồn tại độc lập và được chia sẻ, nó cần có Repository riêng (`ICustomFieldRepository`) để quản trị viên có thể tạo trước danh sách các trường dữ liệu tĩnh.

#### 4. Sử dụng Value Objects (`Shared/`)
Bản thiết kế ER của bạn có các trường như `urlHomePage` hay `image`. Thay vì dùng chuỗi (string) thông thường dễ bị lỗi, bạn nên tạo Value Object trong thư mục `Shared`.
*   Ví dụ `Url.cs`: Sẽ chứa logic kiểm tra xem chuỗi nhập vào có phải là HTTP/HTTPS hợp lệ hay không. Nếu không hợp lệ, nó sẽ quăng lỗi ngay từ tầng Domain, bảo vệ hệ thống khỏi rác dữ liệu.

Cấu trúc này giữ cho module Catalogs cực kỳ **gọn gàng, dễ mở rộng** và đảm bảo **tính toàn vẹn dữ liệu** cao nhất khi bạn thiết lập các mối quan hệ (Relational) động! Bạn có muốn tôi viết thử class `FieldSet.cs` để xem cách nó quản lý `FieldItem` không?
