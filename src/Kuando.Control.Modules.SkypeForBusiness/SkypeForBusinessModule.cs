using System.ComponentModel.Composition;
using Kuando.Control.Infrastructure;
using Microsoft.Practices.ServiceLocation;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

namespace Kuando.Control.Modules.SkypeForBusiness
{
    [ModuleExport(typeof(SkypeForBusinessModule))]
    public class SkypeForBusinessModule : IModule
    {
        #region Fields

        private IRegionManager _regionManager;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public SkypeForBusinessModule(IRegionManager regionManager)
        {
            this._regionManager = regionManager;

            this._regionManager.RegisterViewWithRegion(Constants.NavigationRegion, () => ServiceLocator.Current.GetInstance<Views.SkypeForBusinessNavigation>());
        }

        #endregion

        #region Methods

        public void Initialize()
        {
        }

        #endregion
    }
}
