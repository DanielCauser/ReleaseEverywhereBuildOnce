using System;
namespace SampleApp.Config
{
    public static class Constants
    {
        public static string EndPoint =>
        AppSettingsManager.Settings["EndPoint"];

    }
}
