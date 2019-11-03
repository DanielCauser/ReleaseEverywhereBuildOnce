using System;
using CauserException.Config;

namespace SampleApp.Config
{
    public static class Constants
    {
        public const string DefaultConfigPath = "appsettings.json";

        public static AppEnvironment CurrentEnvironment
        {
            get
            {
#if (DEBUG)
                return AppEnvironment.dev;
#else
                return AppEnvironment.Unspecified;
#endif
            }
        }

        public static string EndPoint =>
        AppSettingsManager.Settings["EndPoint"];
    }
}
