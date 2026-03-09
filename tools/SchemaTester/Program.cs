using System;
using AssetManagement.Domain.Assets;
using Microsoft.EntityFrameworkCore;

namespace SchemaTester;

public class TempDbContext : DbContext
{
    public DbSet<AssetModel> AssetModels { get; set; }
    public DbSet<FieldSet> FieldSets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Sử dụng database trên RAM siêu nhanh, không cần cài SQL Server
        optionsBuilder.UseInMemoryDatabase("AssetTestDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Hướng dẫn EF Core cách đọc các Value Object (Rất quan trọng)
        // modelBuilder.Entity<AssetModel>().OwnsOne(x => x.Name); // Tạm tắt vì AssetModel chưa có Name
        modelBuilder.Entity<FieldSet>().OwnsOne(x => x.Name);
        
        // Bạn có thể thêm các mapping khác ở đây
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Đang kiểm tra sơ đồ Entity (Schema Mapping)...");

        try
        {
            using var db = new TempDbContext();
            
            // Lệnh này ép EF Core dịch toàn bộ code Entity sang cấu trúc bảng
            db.Database.EnsureCreated();

            Console.WriteLine("✅ CHÚC MỪNG! Sơ đồ Database của bạn KHÔNG CÓ LỖI.");
            
            Console.WriteLine("\n--- BẢN ĐỒ QUAN HỆ CÁC BẢNG (DEBUG VIEW) ---");
            Console.WriteLine(db.Model.ToDebugString(Microsoft.EntityFrameworkCore.Infrastructure.MetadataDebugStringOptions.LongDefault));
            Console.WriteLine("--------------------------------------------");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ CÓ LỖI KHI MAP ENTITY:");
            Console.WriteLine(ex.Message);
        }
    }
}
