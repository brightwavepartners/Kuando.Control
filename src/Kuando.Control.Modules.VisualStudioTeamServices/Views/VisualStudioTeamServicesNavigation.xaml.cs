using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Kuando.Control.Modules.VisualStudioTeamServices.Views
{
    /// <summary>
    /// Interaction logic for VisualStudioTeamServicesNavigationView.xaml
    /// </summary>
    [Export]
    public partial class VisualStudioTeamServicesNavigation : UserControl
    {
        #region Constructors

        public VisualStudioTeamServicesNavigation()
        {
            this.InitializeComponent();
        }

        #endregion
    }
}
