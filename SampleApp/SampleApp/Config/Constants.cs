using System;
namespace SampleApp.Config
{
    public static class Constants
    {
        public static string EndPoint =>
#if DEBUG
        AppSettingsManager.Settings["appsettings.dev.json"];
#else
        AppSettingsManager.Settings["appsettings.json"];
#endif

    }
}
