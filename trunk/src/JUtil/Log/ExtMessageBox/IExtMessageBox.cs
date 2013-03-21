namespace JUtil
{
    /// <summary>
    /// interface IExtMessageBox
    /// </summary>
    public interface IExtMessageBox
    {
        /// <summary>
        /// interface of Error MessageBox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        void Error(string text, string caption);

        /// <summary>
        /// interface of Info MessageBox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        void Info(string text, string caption);
        

    } // end of IMessageBox
}
