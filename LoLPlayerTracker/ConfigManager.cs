using System.Configuration;
using System.Windows.Forms;

namespace LoLPlayerTracker {
    public static class ConfigManager {

        public static void Set(string key, string value) {
            Configuration Config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            Config.AppSettings.Settings.Remove(key);
            Config.AppSettings.Settings.Add(key, value);
            Config.Save(ConfigurationSaveMode.Modified);
        }

        public static string Get(string key) {
            string value = ConfigurationManager.AppSettings[key];
            if (value == null) {
                value = "";
            }
            return value;
        }

        public static bool Exists(string key) {
            return (ConfigurationManager.AppSettings[key] != null);
        }

    }
}
