using System;

namespace JUtil
{
    internal sealed class NoneLog : ILog
    {
        #region ILog 成員

        public void T(string format, params object[] args){}
        public void D(string format, params object[] args){}
        public void I(string format, params object[] args){}
        public void W(string format, params object[] args){}
        public void E(string format, params object[] args){}
        
        #endregion

    } // end of ConsoleLog
}
