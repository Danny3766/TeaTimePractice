using Microsoft.EntityFrameworkCore;
using TeaTime.DataAccess.Data;
using TeaTime.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using TeaTime.Utility;
using TeaTime.DataAccess.DBInitializer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Keep Development on appsettings.Development.json. For non-Development local
// runs, load User Secrets before reading configuration values, then re-apply
// environment and command-line providers so Azure App Service settings win.
if (!builder.Environment.IsDevelopment())
{
    builder.Configuration
        .AddUserSecrets<Program>(optional: true)
        .AddEnvironmentVariables()
        .AddCommandLine(args);
}

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// 註冊 DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 註冊 Identity 服務
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

// 註冊 Cookie 設定
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

// 註冊資料庫初始化器，供啟動流程套用 migration 並建立預設身分資料。
builder.Services.AddScoped<IDbInitializer, DbInitializer>();

// 註冊使用 Razor 服務
builder.Services.AddRazorPages();

// 註冊 IUnitOfWork,UnitOfWork DI 服務
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

// 註冊 EmailSender 服務
builder.Services.AddScoped<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 在啟用驗證與授權前初始化資料庫、角色與預設管理者帳號。
SeedDatabase();

// 增加身分驗證
app.UseAuthentication();
// 授權
app.UseAuthorization();
//將 Razor Page 加入路由對應
app.MapRazorPages();

// 調整專案的路由設定
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();

/// <summary>
/// 建立服務範圍並執行資料庫初始化流程，確保 migration、預設角色與管理者帳號在應用程式啟動時完成設定。
/// </summary>
void SeedDatabase() 
{
    using (var scope = app.Services.CreateScope()) 
    {
        // 透過獨立 scope 解析 scoped service，避免直接從 root provider 取得資料庫相關服務。
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}
