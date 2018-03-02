using System;
using System.ComponentModel.Composition;
using Kuando.Control.Infrastructure;
using Kuando.Control.Infrastructure.Events;
using Kuando.Control.Infrastructure.Models;
using Kuando.Control.Modules.GoogleHangouts.Events;
using Kuando.Control.Modules.GoogleHangouts.Models;
using Kuando.Control.Modules.GoogleHangouts.Repositories;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

namespace Kuando.Control.Modules.GoogleHangouts
{
    [ModuleExport(typeof(GoogleHangoutsModule))]
    public class GoogleHangoutsModule : IModule
    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;
        private readonly HangoutMonitor _hangout;
        private readonly IRegionManager _regionManager;
        private readonly SettingRepository _settingRepository;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public GoogleHangoutsModule(IRegionManager regionManager, IEventAggregator eventAggregator, SettingRepository settingRepository, HangoutMonitor hangout)
        {
            this._eventAggregator = eventAggregator;
            this._eventAggregator.GetEvent<GoogleHangoutSettingEvent>().Subscribe(this.OnGoogleHangoutSettingChanged, ThreadOption.BackgroundThread);

            this._regionManager = regionManager;
            this._regionManager.RegisterViewWithRegion(Constants.NavigationRegion, () => ServiceLocator.Current.GetInstance<Views.GoogleHangoutsNavigation>());
            this._regionManager.RegisterViewWithRegion(Constants.SettingsRegion, () => ServiceLocator.Current.GetInstance<Views.GoogleHangoutsSettings>());

            this._hangout = hangout;
            hangout.OnActiveChanged += this.OnHangoutActiveChanged;

            this._settingRepository = settingRepository;

            if (Convert.ToBoolean(this._settingRepository[SettingRepository.Enabled].Value))
            {
                this._hangout.StartMonitor();
            }
        }

        #endregion

        #region Methods

        public void Initialize()
        {
        }


        private void OnHangoutActiveChanged(object sender, ActiveChangedEventArgs eventArgs)
        {
            var hangoutsEnabled = Convert.ToBoolean(this._settingRepository[SettingRepository.Enabled].Value);
            var hangoutsActive = eventArgs.Active;

            if (!hangoutsEnabled)
            {
                return;
            }

            this._eventAggregator.GetEvent<BusyLightColorEvent>().Publish(hangoutsActive ? Color.Red : Color.Green);
        }

        private void OnGoogleHangoutSettingChanged(Setting setting)
        {
            if (setting.Key != SettingRepository.Enabled)
            {
                return;
            }

            if (Convert.ToBoolean(setting.Value))
            {
                if (this._hangout.IsActive)
                {
                    this._eventAggregator.GetEvent<BusyLightColorEvent>().Publish(Color.Red);
                }

                this._hangout.StartMonitor();
            }
            else
            {
                this._hangout.StopMonitor();
                this._eventAggregator.GetEvent<BusyLightColorEvent>().Publish(Color.Green);
            }
        }

        #endregion
    }
}
