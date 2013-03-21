using Gurock.SmartInspect;

namespace JFramework
{
    class SILog : ILog
    {
        static SILog()
        {
            SiAuto.Si.Enabled = true;
        }

        public void D(string format, params object[] args)
        {
            SiAuto.Main.LogDebug(format, args);
        }

        public void I(string format, params object[] args)
        {
            SiAuto.Main.LogMessage(format, args);
        }

        public void W(string format, params object[] args)
        {
            SiAuto.Main.LogWarning(format, args);
        }

        public void E(string format, params object[] args)
        {
            SiAuto.Main.LogError(format, args);
        }
    }
}
