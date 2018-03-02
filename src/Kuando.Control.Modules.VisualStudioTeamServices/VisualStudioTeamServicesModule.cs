using System.ComponentModel.Composition;
using System.Diagnostics;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

namespace Kuando.Control.Modules.VisualStudioTeamServices
{
    [ModuleExport(typeof(VisualStudioTeamServicesModule))]

    public class VisualStudioTeamServicesModule : IModule
    {
        #region Fields

        private readonly IRegionManager _regionManager;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public VisualStudioTeamServicesModule(IRegionManager regionManager)
        {
            this._regionManager = regionManager;

            //this._regionManager.RegisterViewWithRegion(Constants.NavigationRegion, () => ServiceLocator.Current.GetInstance<Views.VisualStudioTeamServicesNavigation>());
            //this._regionManager.RegisterViewWithRegion(Constants.SettingsRegion, () => ServiceLocator.Current.GetInstance<Views.VisualStudioTeamServicesSettings>());
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            Debug.WriteLine("VisualStudioTeamServices Module Initialize");
        }


        #endregion
    }
}
