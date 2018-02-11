using System;
using System.ComponentModel.Composition;
using Kuando.Control.Modules.GoogleHangouts.Events;
using Kuando.Control.Modules.GoogleHangouts.Models;
using Kuando.Control.Modules.GoogleHangouts.Repositories;
using Prism.Events;
using Prism.Mvvm;

namespace Kuando.Control.Modules.GoogleHangouts.Views
{
    [Export]
    public class GoogleHangoutsSettingsViewModel : BindableBase
    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;
        private readonly SettingRepository _settingRepository;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public GoogleHangoutsSettingsViewModel(IEventAggregator eventAggregator, SettingRepository settingRepository)
        {
            this._eventAggregator = eventAggregator;
            this._settingRepository = settingRepository;
        }

        #endregion

        #region Properties

        public bool IsEnabled
        {
            get => Convert.ToBoolean(this._settingRepository[SettingRepository.Enabled].Value);

            set
            {
                var newSetting = new Setting(SettingRepository.Enabled, value.ToString());

                this._settingRepository[SettingRepository.Enabled] = newSetting;

                this._eventAggregator.GetEvent<GoogleHangoutSettingEvent>().Publish(newSetting);
            }
        }

        #endregion
    }
}
