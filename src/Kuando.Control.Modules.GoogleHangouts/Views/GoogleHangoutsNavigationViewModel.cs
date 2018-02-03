using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Prism.Regions;

namespace Kuando.Control.Modules.GoogleHangouts.Views
{
    [Export]
    public class GoogleHangoutsNavigationViewModel : BindableBase
    {
        #region Fields

        private readonly IRegionManager _regionManager;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public GoogleHangoutsNavigationViewModel(IRegionManager regionManager)
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
            this._regionManager.RequestNavigate("SettingsRegion", "GoogleHangoutsSettings");
        }

        #endregion
    }
}
