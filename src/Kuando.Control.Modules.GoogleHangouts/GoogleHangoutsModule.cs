using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Kuando.Control.Modules.GoogleHangouts.Models;

namespace Kuando.Control.Modules.GoogleHangouts
{
    [ModuleExport(typeof(GoogleHangoutsModule))]
    public class GoogleHangoutsModule : IModule
    {
        #region Fields

        private readonly IRegionManager _regionManager;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public GoogleHangoutsModule(IRegionManager regionManager, Hangout hangout)
        {
            this._regionManager = regionManager;

            this._regionManager.RegisterViewWithRegion("NavigationRegion", typeof(Views.GoogleHangoutsNavigation));
            this._regionManager.RegisterViewWithRegion("SettingsRegion", typeof(Views.GoogleHangoutsSettings));

            var hangoutMonitorTask = new Task(hangout.Monitor, TaskCreationOptions.LongRunning);

            hangoutMonitorTask.Start();
        }

        #endregion

        #region Methods

        public void Initialize()
        {
        }


        #endregion
    }
}
