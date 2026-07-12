namespace TeaTime.DataAccess.DBInitializer
{
    /// <summary>
    /// 定義應用程式啟動時需要執行的資料庫初始化流程。
    /// </summary>
    public interface IDbInitializer
    {
        /// <summary>
        /// 執行資料庫遷移、角色建立與預設管理者帳號建立流程。
        /// </summary>
        void Initialize();
    }
}