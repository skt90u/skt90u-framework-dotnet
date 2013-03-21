namespace JUtil.Service
{
    /// <summary>
    /// 要在Service中定時執行的物件，必須實作的介面定義
    /// </summary>
    public interface IServiceObject
    {
        /// <summary>
        /// 初始化此物件的參數
        /// </summary>
        void Initialize(object args);

        /// <summary>
        /// 定義定時執行的動作
        /// </summary>
        void RunOnce();
    }
}
