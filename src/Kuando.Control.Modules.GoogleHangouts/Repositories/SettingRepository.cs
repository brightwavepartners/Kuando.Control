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

        private List<Setting> _settings;

        #endregion

        #region Constructors

        public SettingRepository()
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var googleHangoutsConfigurationSectionSettings =
                ((AppSettingsSection)configFile.GetSection(GoogleHangoutsConfigurationSectionName)).Settings;

            this._settings = new List<Setting>();

            foreach (KeyValueConfigurationElement googleHangoutsConfigurationSectionSetting in googleHangoutsConfigurationSectionSettings)
            {
                this._settings.Add(new Setting(googleHangoutsConfigurationSectionSetting.Key,
                    googleHangoutsConfigurationSectionSetting.Value));
            }
        }

        #endregion

        #region Properties

        public Setting this[object key]
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
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

        public string GetSetting(string key)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var googleHangoutsConfigurationSectionSettings =
                ((AppSettingsSection)configFile.GetSection(GoogleHangoutsConfigurationSectionName)).Settings;

            return googleHangoutsConfigurationSectionSettings[key].Value;
        }

        public void Remove(Setting item)
        {
            throw new System.NotImplementedException();
        }

        public void SaveSetting(string key, string value)
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
