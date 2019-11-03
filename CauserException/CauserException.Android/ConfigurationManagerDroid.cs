using System;
using System.IO;
using Android.Content;
using SampleApp.Config;

namespace CauserException.Droid
{
    public class ConfigurationManagerDroid : AppSettingsManager
    {
        public static void Init(Context context, string configPath = Constants.DefaultConfigPath)
        {
            Stream stream;
            try
            {
                stream = context.Assets.Open(configPath);
            }
            catch (Java.IO.FileNotFoundException)
            {
                if (configPath == Constants.DefaultConfigPath)
                    throw;

                configPath = Constants.DefaultConfigPath;
                stream = context.Assets.Open(configPath);
            }

            Init(stream);
        }
    }
}
