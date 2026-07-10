using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeaTime.DataAccess.Data;
using TeaTime.Models;
using TeaTime.Utility;

namespace TeaTime.DataAccess.DBInitializer
{
    /// <summary>
    /// 負責在應用程式啟動時初始化資料庫與身分角色資料。
    /// </summary>
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// 初始化資料庫初始化器所需的使用者管理、角色管理與資料庫內容服務。
        /// </summary>
        /// <param name="userManager">用於建立預設管理者帳號與指派角色的使用者管理服務。</param>
        /// <param name="roleManager">用於檢查與建立系統角色的角色管理服務。</param>
        /// <param name="db">用於執行資料庫遷移與查詢預設管理者帳號的資料庫內容。</param>
        public DbInitializer(UserManager<IdentityUser> userManager,
                             RoleManager<IdentityRole> roleManager,
                             ApplicationDbContext db) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        /// <summary>
        /// 執行資料庫初始化流程，包含套用待處理的 migration、建立預設角色與預設管理者帳號。
        /// </summary>
        public void Initialize() 
        {
            try 
            {
                // 檢查是否有待處理的 migration，如果有就進行資料庫遷移 migration
                if (_db.Database.GetPendingMigrations().Count() > 0) 
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }
            // 檢查角色是否存在，如果不存在就建立所有角色以及我們的管理者帳號
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult()) 
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Manager)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Name = "Administrator",
                    PhoneNumber = "0911111111",
                    Address = "test address 123",
                    EmailConfirmed = true
                }, "Admin123*").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@gmail.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }

            return;
        }

    }
}
