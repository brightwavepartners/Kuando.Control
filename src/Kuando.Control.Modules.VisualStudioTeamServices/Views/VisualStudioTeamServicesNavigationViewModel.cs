using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Input;
using Kuando.Control.Infrastructure;
using Prism.Regions;

namespace Kuando.Control.Modules.VisualStudioTeamServices.Views
{
    [Export]
    public class VisualStudioTeamServicesNavigationViewModel : BindableBase
    {
        #region Fields

        private readonly IRegionManager _regionManager;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public VisualStudioTeamServicesNavigationViewModel(IRegionManager regionManager)
        {
            this._regionManager = regionManager;

            this.NavigateCommand = new DelegateCommand(this.OnNavigate, this.CanNavigate);
        }


        #endregion

        #region Properties

        public ICommand NavigateCommand { get; }

        #endregion

        #region Methods

        private bool CanNavigate()
        {
            return true;
        }

        private void OnNavigate()
        {
            this._regionManager.RequestNavigate(Constants.SettingsRegion, "VisualStudioTeamServicesSettings");
        }

        #endregion
    }
}
