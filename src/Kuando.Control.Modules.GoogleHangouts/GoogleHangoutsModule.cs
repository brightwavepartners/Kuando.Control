using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Kuando.Control.Infrastructure.Events;
using Kuando.Control.Infrastructure.Models;
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

        private const string HangoutsMonitorEnabledConfigKey = "Enabled";

        private readonly IEventAggregator _eventAggregator;
        private readonly Hangout _hangout;
        private readonly Task _hangoutMonitorTask;
        private readonly IRegionManager _regionManager;
        private readonly SettingRepository _settingRepository;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public GoogleHangoutsModule(IRegionManager regionManager, IEventAggregator eventAggregator, SettingRepository settingRepository, Hangout hangout)
        {
            this._eventAggregator = eventAggregator;
            this._regionManager = regionManager;
            this._settingRepository = settingRepository;

            this._regionManager.RegisterViewWithRegion("NavigationRegion", () => ServiceLocator.Current.GetInstance<Views.GoogleHangoutsNavigation>());
            this._regionManager.RegisterViewWithRegion("SettingsRegion", typeof(Views.GoogleHangoutsSettings));

            this._hangout = hangout;
            hangout.OnActiveChanged += this.OnHangoutActiveChanged;

            this._hangoutMonitorTask = new Task(hangout.Monitor, TaskCreationOptions.LongRunning);

            if (Convert.ToBoolean(this._settingRepository.GetSetting(HangoutsMonitorEnabledConfigKey)))
            {
                this._hangoutMonitorTask.Start();
            }
        }

        #endregion

        #region Methods

        public void Initialize()
        {
        }


        private void OnHangoutActiveChanged(object sender, ActiveChangedEventArgs eventArgs)
        {
            var hangoutsEnabled = Convert.ToBoolean(this._settingRepository.GetSetting(HangoutsMonitorEnabledConfigKey));
            var hangoutsActive = eventArgs.Active;

            if (!hangoutsEnabled)
            {
                return;
            }

            this._eventAggregator.GetEvent<BusyLightColorEvent>().Publish(hangoutsActive ? Color.Red : Color.Green);
        }

        #endregion
    }
}
