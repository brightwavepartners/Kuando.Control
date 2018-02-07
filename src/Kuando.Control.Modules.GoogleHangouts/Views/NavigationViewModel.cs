using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Input;

namespace Kuando.Control.Modules.GoogleHangouts.Views
{
    [Export]
    public class NavigationViewModel : BindableBase
    {
        #region Constructors

        public NavigationViewModel()
        {
            this.SubmitCommand = new DelegateCommand<object>(this.OnSubmit, this.CanSubmit);
        }


        #endregion

        #region Properties

        public ICommand SubmitCommand { get; }

        #endregion

        #region Methods

        private bool CanSubmit(object arg)
        {
            return true;
        }

        private void OnSubmit(object arg)
        {
            Debug.WriteLine("OnSubmit");
        }

        #endregion
    }
}
