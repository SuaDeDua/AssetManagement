sau khi tạo xong project sẽ dùng lệnh dotnet sln AssetManagement.slnx add **/*.csproj để tự động thêm vào project tổng

tree-file mới đầu vào sẽ có chọn slnx hoặc sln

chia ra tạo solution folder và tạo real folder
    solution folder: chỉ ghi folder ảo trong file .slnx
    real folder: lệnh mkdir -p ... sau đó ghi file ảo vào file .slnx
    add exist folder/file
    delete project
