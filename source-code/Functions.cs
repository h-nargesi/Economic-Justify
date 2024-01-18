using System.Reflection;
using System.Diagnostics;

namespace Photon.Economy
{
    public class Functions
    {
        public static string GetVersion()
        {
            return
                Assembly.GetExecutingAssembly().GetName().Name + ": " +
                Assembly.GetExecutingAssembly().GetName().Version + "\r\nFile Version: " +
                FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
        }
    }
}
