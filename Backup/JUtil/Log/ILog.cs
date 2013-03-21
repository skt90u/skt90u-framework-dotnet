namespace JUtil
{
    internal interface ILog
    {
        void T(string format, params object[] args);
        void D(string format, params object[] args);
        void I(string format, params object[] args);
        void W(string format, params object[] args);
        void E(string format, params object[] args);
    }
}
