namespace TeaTime.Utility
{
    public static class SD
    {
        /// <summary>
        /// 系統管理者角色
        /// </summary>
        public const string Role_Admin = "Admin";

        /// <summary>
        /// 顧客角色
        /// </summary>
        public const string Role_Customer = "Customer";

        /// <summary>
        /// 員工角色
        /// </summary>
        public const string Role_Employee = "Employee";

        /// <summary>
        /// 經理人角色
        /// </summary>
        public const string Role_Manager = "Manager";

        // 等待店家確認訂單 -> 店家確認後改為訂單準備中 -> 店家準備完成後改
        // 為訂單完成、可取餐 -> 使用者取餐後改為訂單完成
        
        /// <summary>
        /// 等待店家確認訂單
        /// </summary>
        public const string StatusPending = "Pending";

        /// <summary>
        /// 店家確認接單後的訂單狀態，表示訂單已核准並可進入準備流程。
        /// </summary>
        public const string StatusApproved = "Approved";

        /// <summary>
        /// 店家確認後改為訂單準備中
        /// </summary>
        public const string StatusInProcess = "Processing";

        /// <summary>
        /// 店家或顧客取消訂單
        /// </summary>
        public const string StatusCancelled = "Cancelled";

        /// <summary>
        /// 店家準備完成，顧客可以取餐
        /// </summary>
        public const string StatusReady = "Ready";

        /// <summary>
        /// 顧客取餐及付款後，店家結束訂單 
        /// </summary>
        public const string StatusCompleted = "Completed";
    }
}


