namespace JUtil
{
    /// <summary>
    /// 指定用於連線資料庫的Privider
    /// </summary>
    public enum DbProvider
    {
        /// <summary>
        /// MySql native provider
        /// </summary>
        MySql = 0,

        /// <summary>
        /// SqlServer native provider
        /// </summary>
        SqlServer = 1,

        /// <summary>
        /// Oracle native provider
        /// </summary>
        Oracle = 2,

        /// <summary>
        /// Teradata native provider
        /// </summary>
        Teradata = 3,

        /// <summary>
        /// Sqlite native provider
        /// </summary>
        Sqlite = 4,

        /// <summary>
        /// OleDb provider
        /// </summary>
        OleDb = 5


    } // end of DatabaseMode
}
