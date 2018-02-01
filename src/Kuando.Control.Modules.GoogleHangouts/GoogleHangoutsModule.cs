using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

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
        public GoogleHangoutsModule(IRegionManager regionManager, HangoutsDetector hangoutsDetector)
        {
            this._regionManager = regionManager;

            this._regionManager.RegisterViewWithRegion("NavigationRegion", typeof(Views.Navigation));
            this._regionManager.RegisterViewWithRegion("SettingsRegion", typeof(Views.Settings));

            var hangoutsDetectorTask = new Task(() => hangoutsDetector.Run(), TaskCreationOptions.LongRunning);

            hangoutsDetectorTask.Start();
        }

        #endregion

        #region Methods

        public void Initialize()
        {
        }


        #endregion
    }
}
