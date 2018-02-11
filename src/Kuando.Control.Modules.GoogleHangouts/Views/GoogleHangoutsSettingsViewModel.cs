using System;
using System.ComponentModel.Composition;
using Kuando.Control.Infrastructure.Events;
using Kuando.Control.Infrastructure.Models;
using Kuando.Control.Modules.GoogleHangouts.Repositories;
using Prism.Events;
using Prism.Mvvm;

namespace Kuando.Control.Modules.GoogleHangouts.Views
{
    [Export]
    public class GoogleHangoutsSettingsViewModel : BindableBase
    {
        #region Fields

        private const string EnabledSettingKey = "Enabled";

        private readonly IEventAggregator _eventAggregator;
        private readonly SettingRepository _settingRepository;

        private bool _isEnabled;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public GoogleHangoutsSettingsViewModel(IEventAggregator eventAggregator, SettingRepository settingRepository)
        {
            this._eventAggregator = eventAggregator;
            this._settingRepository = settingRepository;

            this._isEnabled = !string.IsNullOrEmpty(this._settingRepository.GetSetting(EnabledSettingKey)) &&
                              Convert.ToBoolean(this._settingRepository.GetSetting(EnabledSettingKey));
        }

        #endregion

        #region Properties

        public bool IsEnabled
        {
            get => this._isEnabled;

            set
            {
                if (value == this._isEnabled)
                {
                    return;
                }

                this._isEnabled = value;

                this._settingRepository.SaveSetting(EnabledSettingKey, this._isEnabled.ToString());

                if (!this._isEnabled)
                {
                    this._eventAggregator.GetEvent<BusyLightColorEvent>().Publish(Color.Green);
                }
            }
        }

        #endregion
    }
}
