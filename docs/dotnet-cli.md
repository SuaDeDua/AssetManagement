**CHÍNH XÁC 100%!**

.NET CLI (`dotnet`) là một công cụ cực kỳ mạnh mẽ. Bạn hoàn toàn có thể xây dựng toàn bộ cấu trúc dự án từ con số 0 (Zero) chỉ bằng các dòng lệnh trong Terminal/Console mà không cần đụng đến chuột hay giao diện của Visual Studio.

Dưới đây là một "Bản đồ Cheat Sheet" các lệnh CLI quyền lực nhất để bạn thao tác mọi thứ:

---

### 1. Quản lý thư mục (Dùng lệnh OS kết hợp .NET)
*Lưu ý: Tạo thư mục (`mkdir`) không phải là lệnh của .NET, mà là lệnh của hệ điều hành (Mac/Linux/Windows).*

```bash
# Tạo một lèo các thư mục lồng nhau
mkdir -p src/API/AssetManagement.Api

# Chuyển vào thư mục đó
cd src/API/AssetManagement.Api
```

### 2. Quản lý Solution (.sln / .slnx)

```bash
# Tạo file Solution mới tinh
dotnet new sln -n AssetManagement

# Thêm 1 project vào Solution
dotnet sln AssetManagement.sln add src/API/AssetManagement.Api/AssetManagement.Api.csproj

# Thêm nhiều project cùng lúc (Dùng Wildcard - Rất bá đạo!)
dotnet sln AssetManagement.sln add **/*.csproj

# Xóa 1 project khỏi Solution
dotnet sln AssetManagement.sln remove src/AssetManagement.Domain/AssetManagement.Domain.csproj
```

### 3. Tạo Project (.csproj)
Bạn có thể tạo ra hầu hết mọi loại ứng dụng.

```bash
# Tạo dự án Web API (Backend)
dotnet new webapi -n AssetManagement.Api -o src/API/AssetManagement.Api

# Tạo dự án Web API
dotnet new webapi -n AssetManagement.Api -o src/API/AssetManagement.Api
     --no-https false --enable-openapi true --use-controllers false

# Tạo thư viện Class Library (Chỉ chứa code, không chạy được độc lập)
dotnet new classlib -n AssetManagement.Domain -o src/AssetManagement.Domain

# Tạo dự án Console (Tool nhỏ, chạy nền)
dotnet new console -n DataSeeder -o tools/DataSeeder

# Tạo dự án Unit Test (Dùng xUnit)
dotnet new xunit -n AssetManagement.Domain.UnitTests -o tests/AssetManagement.Domain.UnitTests
```

### 4. Quản lý Reference (Gắn kết các Project)
Thay vì click chuột phải "Add Reference", bạn dùng CLI:

```bash
# Đứng từ API, gọi sang Application
dotnet add src/API/AssetManagement.Api/AssetManagement.Api.csproj reference src/AssetManagement.Application/AssetManagement.Application.csproj
```

### 5. Quản lý Thư viện ngoài (NuGet Packages)

```bash
# Cài thư viện MediatR vào Application
dotnet add src/AssetManagement.Application/AssetManagement.Application.csproj package MediatR

# Cài thư viện EF Core với phiên bản cụ thể
dotnet add src/AssetManagement.Infrastructure/AssetManagement.Infrastructure.csproj package Microsoft.EntityFrameworkCore.SqlServer --version 10.0.0

# Xóa thư viện
dotnet remove src/AssetManagement.Api/AssetManagement.Api.csproj package Newtonsoft.Json
```

### 6. Code Generation (Sinh file code)
Mặc dù bạn hay tự tạo file `.cs` bằng tay, .NET CLI cũng hỗ trợ sinh một số file cấu hình:

```bash
# Tạo file gitignore chuẩn cho .NET (Cực kỳ quan trọng)
dotnet new gitignore

# Tạo file editorconfig chuẩn
dotnet new editorconfig

# Tạo file global.json (Dùng để khóa phiên bản SDK)
dotnet new globaljson --sdk-version 10.0.201
```

---

### Một tuyệt chiêu "Bấm 1 Enter ra cả dự án":
Rất nhiều Senior Dev viết sẵn một file script `.sh` (trên Mac/Linux) hoặc `.bat` (trên Windows) chứa 20 dòng lệnh `dotnet new`, `dotnet add` như ở trên. 

Mỗi khi có dự án mới, họ chỉ việc chạy file script đó. 3 giây sau, toàn bộ cây thư mục Clean Architecture với đầy đủ Solution, Projects, và References được dựng lên hoàn hảo!

Bạn có đang định xây dựng cả hệ thống bằng CLI luôn không? Nếu vướng lệnh nào, cứ nhắn mình nhé.

------
# Custom Rules - configure these as required

# .NET Code Analyzers rules

# CA1000: Do not declare static members on generic types
dotnet_diagnostic.CA1000.severity = none

# CA1002: Do not expose generic lists
dotnet_diagnostic.CA1002.severity = none

# CA1008: Enums should have zero value
dotnet_diagnostic.CA1008.severity = none

# CA1019: Define accessors for attribute arguments
dotnet_diagnostic.CA1019.severity = none

# CA1024: Use properties where appropriate
dotnet_diagnostic.CA1024.severity = none

# CA1030: Use events where appropriate
dotnet_diagnostic.CA1030.severity = none

# CA1031: Do not catch general exception types
dotnet_diagnostic.CA1031.severity = none

# CA1032: Implement standard exception constructors
dotnet_diagnostic.CA1032.severity = none

# CA1034: Nested types should not be visible
dotnet_diagnostic.CA1034.severity = none

# CA1040: Avoid empty interfaces
dotnet_diagnostic.CA1040.severity = none

# CA1051: Do not declare visible instance fields
dotnet_diagnostic.CA1051.severity = none

# CA1056: URI-like properties should not be strings
dotnet_diagnostic.CA1056.severity = none

# CA1062: Validate arguments of public methods
dotnet_diagnostic.CA1062.severity = none

# CA1063: Implement IDisposable Correctly
dotnet_diagnostic.CA1063.severity = none

# CA1307: Specify StringComparison for clarity
dotnet_diagnostic.CA1307.severity = none

# CA1308: Normalize strings to uppercase
dotnet_diagnostic.CA1308.severity = none

# CA1700: Do not name enum values 'Reserved'
dotnet_diagnostic.CA1700.severity = none

# CA1707: Identifiers should not contain underscores
dotnet_diagnostic.CA1707.severity = none

# CA1711: Identifiers should not have incorrect suffix
dotnet_diagnostic.CA1711.severity = none

# CA1716: Identifiers should not match keywords
dotnet_diagnostic.CA1716.severity = none

# CA1724: Type names should not match namespaces
dotnet_diagnostic.CA1724.severity = none

# CA1812: Avoid uninstantiated internal classes
dotnet_diagnostic.CA1812.severity = none

# CA1816: Dispose methods should call SuppressFinalize
dotnet_diagnostic.CA1816.severity = none

# CA1819: Properties should not return arrays
dotnet_diagnostic.CA1819.severity = none

# CA1822: Mark members as static
dotnet_diagnostic.CA1822.severity = none

# CA1848: Use the LoggerMessage delegates
dotnet_diagnostic.CA1848.severity = none

# CA1860: Avoid using 'Enumerable.Any()' extension method
dotnet_diagnostic.CA1860.severity = none

# CA2007: Consider calling ConfigureAwait on the awaited task
dotnet_diagnostic.CA2007.severity = none

# CA2201: Do not raise reserved exception types
dotnet_diagnostic.CA2201.severity = none

# CA2211: Non-constant fields should not be visible
dotnet_diagnostic.CA2211.severity = none

# CA2213: Disposable fields should be disposed
dotnet_diagnostic.CA2213.severity = none

# CA2225: Operator overloads have named alternates
dotnet_diagnostic.CA2225.severity = none

# CA2227: Collection properties should be read only
dotnet_diagnostic.CA2227.severity = none

# CA2234: Pass system uri objects instead of strings
dotnet_diagnostic.CA2234.severity = none

# CA2326: Do not use TypeNameHandling values other than None
dotnet_diagnostic.CA2326.severity = none

# CA2326: Do not use insecure JsonSerializerSettings
dotnet_diagnostic.CA2327.severity = none

# CS8600: Converting null literal or possible null value to non-nullable type.
dotnet_diagnostic.CS8600.severity = none

# CS8603: Possible null reference return.
dotnet_diagnostic.CS8603.severity = none

# CS8618: Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
dotnet_diagnostic.CS8618.severity = none

# IDE Code Analyzers rules

# IDE0046: Convert to conditional expression
dotnet_diagnostic.IDE0046.severity = none

# IDE0053: Use expression body for lambda expression
dotnet_diagnostic.IDE0053.severity = none

# IDE0270: Use coalesce expression
dotnet_diagnostic.IDE0270.severity = none

# IDE0290: Use primary constructor
dotnet_diagnostic.IDE0290.severity = none

# SonarAnalyzer.CSharp rules

# S112: General or reserved exceptions should never be thrown
dotnet_diagnostic.S112.severity = none

# S1075: URIs should not be hardcoded
dotnet_diagnostic.S1075.severity = none

# S2094: Utility classes should not have public constructors
dotnet_diagnostic.S1118.severity = none

# S1144: Unused private types or members should be removed
dotnet_diagnostic.S1144.severity = none

# S2094: Classes should not be empty
dotnet_diagnostic.S2094.severity = none

# S2365: Properties should not make collection or array copies
dotnet_diagnostic.S2365.severity = none

# S3267: Loops should be simplified with "LINQ" expressions
dotnet_diagnostic.S3267.severity = none

# S3881: "IDisposable" should be implemented correctly
dotnet_diagnostic.S3881.severity = none

# S4136: Method overloads should be grouped together
dotnet_diagnostic.S4136.severity = none

# S6605: Collection-specific "Exists" method should be used instead of the "Any" extension
dotnet_diagnostic.S6605.severity = none

