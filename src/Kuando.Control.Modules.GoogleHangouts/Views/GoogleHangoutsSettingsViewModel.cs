using Prism.Mvvm;
using System;
using System.ComponentModel.Composition;
using System.Configuration;

namespace Kuando.Control.Modules.GoogleHangouts.Views
{
    [Export]
    public class GoogleHangoutsSettingsViewModel : BindableBase
    {
        #region Fields

        private const string EnabledConfigurationKey = "GoogleHangouts";

        private bool _isEnabled;

        #endregion

        #region Properties

        public bool IsEnabled
        {
            get
            {
                this._isEnabled = Convert.ToBoolean(GetConfigValue(EnabledConfigurationKey));

                return this._isEnabled;
            }

            set
            {
                if (value == this._isEnabled)
                {
                    return;
                }

                this._isEnabled = value;

                SetConfigValue(EnabledConfigurationKey, this._isEnabled.ToString());
            }
        }

        #endregion

        #region Methods

        private static string GetConfigValue(string key)
        {
            var appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            return appConfig.AppSettings.Settings[key].Value;
        }

        private static void SetConfigValue(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
        }

        #endregion

    }
}
