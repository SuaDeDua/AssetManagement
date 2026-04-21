Theo chuẩn Domain-Driven Design (DDD) và kiến trúc của khóa học **Evently**, tầng `Domain` là trái tim của hệ thống. Nguyên tắc tối thượng ở đây là **Pure C#** (Tuyệt đối không tham chiếu đến Entity Framework Core, ASP.NET Core hay bất kỳ thư viện ngoại vi nào).

Với dự án lớn, cách tổ chức tốt nhất là **Chia theo Aggregate (Feature-based)** thay vì chia theo loại file (Type-based). Điều này giúp toàn bộ logic liên quan đến một thực thể nằm gọn trong một thư mục.

Dưới đây là cấu trúc thư mục tối ưu cho một module, ví dụ lấy module **`Organizations`** (chứa User, Company, Department) từ bản thiết kế ER của bạn làm mẫu:

### Cấu trúc thư mục tầng Domain (`AssetManagement.Modules.Organizations.Domain`)

```text
AssetManagement.Modules.Organizations.Domain/
├── Users/                              # Aggregate Root: User
│   ├── User.cs                         # Thực thể chính (kế thừa AggregateRoot<Guid>)
│   ├── UserErrors.cs                   # Định nghĩa các mã lỗi (ErrorType) cho User
│   ├── IUserRepository.cs              # Interface Repository (Chỉ định nghĩa, KHÔNG code logic DB ở đây)
│   ├── Events/                         # Các Domain Events xảy ra bên trong User
│   │   ├── UserCreatedDomainEvent.cs
│   │   └── UserDepartmentChangedDomainEvent.cs
│   └── ValueObjects/                   # Các đối tượng không có ID, phụ thuộc vào User
│       ├── Email.cs                    # Chứa logic validate email (phải có @...)
│       └── PhoneNumber.cs
│
├── Companies/                          # Aggregate Root: Company
│   ├── Company.cs
│   ├── CompanyErrors.cs
│   └── ICompanyRepository.cs
│
├── Departments/                        # Aggregate Root: Department
│   ├── Department.cs                   # Có quan hệ ParentId, CompanyId, UserManagerId
│   ├── DepartmentErrors.cs
│   └── IDepartmentRepository.cs
│
├── Locations/                          # Aggregate Root: Location
│   ├── Location.cs
│   └── ILocationRepository.cs
│
└── Shared/                             # (Tùy chọn) Các Value Object dùng chung trong nội bộ Module này
    └── Address.cs
```

### Giải thích chi tiết các thành phần:

1.  **Thực thể chính (`User.cs`, `Company.cs`...)**
    *   Các lớp này nên là `internal` hoặc `public sealed`.
    *   Thuộc tính dùng `private set` để ngăn bên ngoài sửa dữ liệu tùy tiện.
    *   Chứa các phương thức nghiệp vụ (Ví dụ: `user.ChangeDepartment(newDepartmentId)`). Phương thức này sẽ kiểm tra logic, đổi trạng thái và tự động gọi `AddDomainEvent(...)`.

2.  **`{Name}Errors.cs`**
    *   Sử dụng chung với pattern `Result` mà bạn đã tạo ở `Shared.Domain`.
    *   Thay vì ném Exception lung tung, bạn định nghĩa sẵn lỗi.
    *   Ví dụ: `public static readonly Error NotFound = Error.NotFound("Users.NotFound", "User not found");`

3.  **`I{Name}Repository.cs`**
    *   Tầng Domain định nghĩa *những gì nó cần* để lưu trữ dữ liệu thông qua các Interface này (Ví dụ: `Task<User?> GetByIdAsync(Guid id)`).
    *   Tầng `Infrastructure` (chứa EF Core) sẽ implement các Interface này. Tầng Domain không cần biết dữ liệu được lưu vào SQL Server hay PostgreSQL.

4.  **`Events/`**
    *   Chứa các Record đại diện cho những thay đổi trạng thái quan trọng.
    *   Ví dụ: Khi bạn gọi hàm `User.Create(...)`, nó sẽ tự động sinh ra một `UserCreatedDomainEvent`. Tầng `Application` sau khi lưu DB thành công sẽ publish Event này để gửi email chào mừng (mà không làm chậm quá trình tạo user).

### Tại sao không chia thư mục kiểu `Entities/`, `Interfaces/`, `Events/`?
Nếu bạn tạo thư mục `Entities` chứa 20 file, thư mục `Events` chứa 50 file, khi cần sửa đổi logic của `User`, bạn sẽ phải nhảy qua nhảy lại giữa 4-5 thư mục khác nhau. Gom tất cả vào thư mục `Users/` (theo chuẩn Vertical Slice trong Domain) giúp tính đóng gói (Encapsulation) cực kỳ chặt chẽ và dễ bảo trì.
