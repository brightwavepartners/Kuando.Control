using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Input;

namespace Kuando.Control.Modules.VisualStudioTeamServices.Views
{
    [Export]
    public class VisualStudioTeamServicesSettingsViewModel : BindableBase
    {
        #region Constructors

        public VisualStudioTeamServicesSettingsViewModel()
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
