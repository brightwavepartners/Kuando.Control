using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Kuando.Control.Modules.VisualStudioTeamServices.Views
{
    /// <summary>
    /// Interaction logic for VisualStudioTeamServicesSettingsView.xaml
    /// </summary>
    [Export]
    public partial class VisualStudioTeamServicesSettings : UserControl
    {
        #region Constructors

        public VisualStudioTeamServicesSettings()
        {
            this.InitializeComponent();
        }


        #endregion
    }
}
