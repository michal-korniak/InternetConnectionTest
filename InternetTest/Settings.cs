using System.IO;
using Newtonsoft.Json;

namespace InternetTest
{
    public static class Settings
    {
        private const string AppSettingsPath = "appsettings.json";

        public static string GoogleDnsAddress { get; private set; }
        public static int PingIntervalInSeconds { get; private set; }
        public static int PingTimeoutInSeconds { get; private set; }
        public static int AttemptsNumber { get; private set; }


        static Settings()
        {
            var appSettingsJson = File.ReadAllText(AppSettingsPath);
            var settings = JsonConvert.DeserializeObject(appSettingsJson) as dynamic;

            GoogleDnsAddress = settings?.GoogleDnsAddress;
            PingIntervalInSeconds = settings?.PingIntervalInSeconds;
            PingTimeoutInSeconds = settings?.PingTimeoutInSeconds;
            AttemptsNumber = settings?.AttemptsNumber;
        }
    }
}