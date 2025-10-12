using DevExpress.ExpressApp.EFCore.Updating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using Castle.Core.Resource;
using DevExpress.Persistent.BaseImpl.EF.StateMachine;
using DXApplication3.Module.BusinessObjects.DanhMuc;
using DXApplication3.Module.BusinessObjects.DonHangs;

namespace DXApplication3.Module.BusinessObjects;

// This code allows our Model Editor to get relevant EF Core metadata at design time.
// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
//Dung sqlserver
//public class DXApplication3ContextInitializer : DbContextTypesInfoInitializerBase {
//	protected override DbContext CreateDbContext() {
//		var optionsBuilder = new DbContextOptionsBuilder<DXApplication3EFCoreDbContext>()
//            .UseSqlServer(";")
//            .UseChangeTrackingProxies()
//            .UseObjectSpaceLinkProxies();
//        return new DXApplication3EFCoreDbContext(optionsBuilder.Options);
//	}
//}
//Dung postgres
public class DXApplication3ContextInitializer : DbContextTypesInfoInitializerBase
{
    protected override DbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DXApplication3EFCoreDbContext>()
            .UseNpgsql(";")
            .UseChangeTrackingProxies()
            .UseObjectSpaceLinkProxies();

        return new DXApplication3EFCoreDbContext(optionsBuilder.Options);
    }
}
//This factory creates DbContext for design-time services. For example, it is required for database migration.
//Sql server
//public class DXApplication3DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DXApplication3EFCoreDbContext> {
//	public DXApplication3EFCoreDbContext CreateDbContext(string[] args) {
//		//throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
//		var optionsBuilder = new DbContextOptionsBuilder<DXApplication3EFCoreDbContext>();
//		//optionsBuilder.UseSqlServer("Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DXApplication3");
//        optionsBuilder.UseSqlServer("Integrated Security=SSPI;Pooling=false;Data Source=MAYDH01\\SQLEXPRESS;Initial Catalog=DXApplication3");
//        optionsBuilder.UseChangeTrackingProxies();
//		optionsBuilder.UseObjectSpaceLinkProxies();
//		return new DXApplication3EFCoreDbContext(optionsBuilder.Options);
//	}
//}

//postgess
public class DXApplication3DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DXApplication3EFCoreDbContext>
{
    public DXApplication3EFCoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DXApplication3EFCoreDbContext>();

        // 🟢 PostgreSQL connection string
        //optionsBuilder.UseNpgsql("Host=db.yejizukiljbbwdvipyvh.supabase.co;Database=postgres;Username=postgres;Password=Demama@2023;SSL Mode=Require;Trust Server Certificate=true");
        //neon.tech
        optionsBuilder.UseNpgsql("Host=ep-plain-hall-a1ko9d1j-pooler.ap-southeast-1.aws.neon.tech;Database=database-01;Username=neondb_owner;Password=npg_0ly1PDeEAMIK;SSL Mode=Require;Trust Server Certificate=true");

        // XAF proxies (vẫn giữ nguyên)
        optionsBuilder.UseChangeTrackingProxies();
        optionsBuilder.UseObjectSpaceLinkProxies();

        return new DXApplication3EFCoreDbContext(optionsBuilder.Options);
    }
}
[TypesInfoInitializer(typeof(DXApplication3ContextInitializer))]
public class DXApplication3EFCoreDbContext : DbContext {
	public DXApplication3EFCoreDbContext(DbContextOptions<DXApplication3EFCoreDbContext> options) : base(options) {
	}
	//public DbSet<ModuleInfo> ModulesInfo { get; set; }
	public DbSet<ModelDifference> ModelDifferences { get; set; }
	public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
	public DbSet<PermissionPolicyRole> Roles { get; set; }
	public DbSet<DXApplication3.Module.BusinessObjects.ApplicationUser> Users { get; set; }
    public DbSet<DXApplication3.Module.BusinessObjects.ApplicationUserLoginInfo> UserLoginInfos { get; set; }
	public DbSet<FileData> FileData { get; set; }
	public DbSet<ReportDataV2> ReportDataV2 { get; set; }
	public DbSet<DashboardData> DashboardData { get; set; }

    public DbSet<Employee> Employee { get; set; }
    //public DbSet<TaskAssignment> TaskAssignment { get; set; }
    //Dang ky bai viet
    public DbSet<BaiViet> BaiViets { get; set; }
    public DbSet<HistoryLog> HistoryLogs { get; set; }

    public DbSet<Task> Task { get; set; }

    public DbSet<DonHang> DonHang { get; set; }
    public DbSet<DanhMucKhoanThu> DanhMucKhoanThu { get; set; }

    public DbSet<DanhMucKhoanChi> DanhMucKhoanChi { get; set; }

    public DbSet<DonHangDoanhThu> DonHangDoanhThu { get; set; }

    //public DbSet<ChiPhiDonHang> ChiPhiDonHang { get; set; }


    //Dang ký StateMachine
    //public DbSet<StateMachine> StateMachines { get; set; }
    //public DbSet<StateMachineState> StateMachineStates { get; set; }
    //public DbSet<StateMachineTransition> StateMachineTransitions { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        //ep kieu postgres
        modelBuilder.Entity<DashboardData>(entity =>
        {
            entity.Property(e => e.ID)
                .HasColumnType("uuid"); // ép kiểu PostgreSQL
        });
        //fix time dung postgres
        // map tất cả DateTime sang timestamp without time zone
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetColumnType("timestamp without time zone");
                }
            }
        }

        // Đăng ký thêm các entity StateMachine
        // Chỉ ignore property có thật
        //modelBuilder.Entity<StateMachine>()
        //    .Ignore(sm => sm.StartState);

        ////Xoa danh muc cha la xoa het cac con co link den
        //modelBuilder.Entity<DonHangDoanhThu>()
        //.HasOne(c => c.KhoanThus)
        //.WithMany(d => d.ID)
        //.HasForeignKey(c => c.LoaiKhoanThuID)
        //.OnDelete(DeleteBehavior.Cascade);


        //modelBuilder.Entity<StateMachine>();
        //modelBuilder.Entity<StateMachineState>();
        //modelBuilder.Entity<StateMachineTransition>();
        // Cấu hình quan hệ giữa StateMachine và StateMachineState cho StartState
        //modelBuilder.Entity<StateMachine>()
        //    .HasOne(sm => sm.StartState) // navigation property
        //    .WithMany() // không có navigation ngược
        //    .HasForeignKey("StartStateId") // tên cột khóa ngoại
        //    .OnDelete(DeleteBehavior.Restrict); // tránh delete cascade vòng lặp

        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
        modelBuilder.Entity<DXApplication3.Module.BusinessObjects.ApplicationUserLoginInfo>(b => {
            b.HasIndex(nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.LoginProviderName), nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.ProviderUserKey)).IsUnique();
        });
        modelBuilder.Entity<ModelDifference>()
            .HasMany(t => t.Aspects)
            .WithOne(t => t.Owner)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
