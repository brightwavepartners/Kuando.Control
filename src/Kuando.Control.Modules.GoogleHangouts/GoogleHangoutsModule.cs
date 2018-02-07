using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using Kuando.Control.Infrastructure.Models;
using Kuando.Control.Infrastructure.Repositories;
using Kuando.Control.Modules.GoogleHangouts.Models;
using Microsoft.Practices.ServiceLocation;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

namespace Kuando.Control.Modules.GoogleHangouts
{
    [ModuleExport(typeof(GoogleHangoutsModule))]
    public class GoogleHangoutsModule : IModule
    {
        #region Fields

        private readonly BusyLightRepository _busyLightRepository;
        private readonly IRegionManager _regionManager;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public GoogleHangoutsModule(IRegionManager regionManager, BusyLightRepository busyLightRepository, Hangout hangout)
        {
            this._busyLightRepository = busyLightRepository;
            this._regionManager = regionManager;

            this._regionManager.RegisterViewWithRegion("NavigationRegion", () => ServiceLocator.Current.GetInstance<Views.GoogleHangoutsNavigation>());
            this._regionManager.RegisterViewWithRegion("SettingsRegion", typeof(Views.GoogleHangoutsSettings));

            hangout.OnActiveChanged += this.OnHangoutActiveChanged;

            var hangoutMonitorTask = new Task(hangout.Monitor, TaskCreationOptions.LongRunning);

            hangoutMonitorTask.Start();
        }

        private void OnHangoutActiveChanged(object sender, ActiveChangedEventArgs eventArgs)
        {
            var busyLight = this._busyLightRepository.GetAll().FirstOrDefault();

            if (busyLight != null)
            {
                busyLight.Color = eventArgs.Active ? Color.Red : Color.Green;
            }
        }

        #endregion

        #region Methods

        public void Initialize()
        {
        }


        #endregion
    }
}
