using System;

namespace Neptuo.AspNet.Navigation
{
    public static class VersionInfo
    {
        internal const string Version = "1.0.0";
        internal const string Preview = "-beta2";

        public static Version GetVersion()
        {
            return new Version(Version);
        }
    }
}
