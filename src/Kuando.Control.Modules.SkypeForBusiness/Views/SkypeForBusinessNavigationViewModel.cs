using System.ComponentModel.Composition;
using System.Windows.Input;
using Kuando.Control.Infrastructure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Kuando.Control.Modules.SkypeForBusiness.Views
{
    [Export]
    public class SkypeForBusinessNavigationViewModel : BindableBase
    {
        #region Fields

        private readonly IRegionManager _regionManager;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public SkypeForBusinessNavigationViewModel(IRegionManager regionManager)
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
            this._regionManager.RequestNavigate(Constants.SettingsRegion, Constants.SkypeForBusinessSettingsViewName);
        }

        #endregion
    }
}
