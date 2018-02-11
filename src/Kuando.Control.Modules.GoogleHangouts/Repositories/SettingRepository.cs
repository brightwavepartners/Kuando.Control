using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics;
using Kuando.Control.Modules.GoogleHangouts.Models;

namespace Kuando.Control.Modules.GoogleHangouts.Repositories
{
    [Export]
    public class SettingRepository : IRepository<Setting>
    {
        #region Fields

        private const string GoogleHangoutsConfigurationSectionName = "googleHangouts";

        #endregion

        #region Properties

        public static string Enabled => "Enabled";

        public Setting this[object key]
        {
            get => new Setting(key.ToString(), GetSetting(key.ToString()));

            set
            {
                SaveSetting(key.ToString(), value.Value);

                Debug.WriteLine($"{key}:{value.Value}");
            }
        }

        #endregion

        #region Methods

        public void Add(Setting item)
        {
            throw new System.NotImplementedException();
        }

        public IList<Setting> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public void Remove(Setting item)
        {
            throw new System.NotImplementedException();
        }

        private static string GetSetting(string key)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var googleHangoutsConfigurationSectionSettings =
                ((AppSettingsSection)configFile.GetSection(GoogleHangoutsConfigurationSectionName)).Settings;

            return googleHangoutsConfigurationSectionSettings[key].Value;
        }

        private static void SaveSetting(string key, string value)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var googleHangoutsConfigurationSectionSettings =
                ((AppSettingsSection)configFile.GetSection(GoogleHangoutsConfigurationSectionName)).Settings;

            googleHangoutsConfigurationSectionSettings[key].Value = value;

            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }

        #endregion
    }
}
