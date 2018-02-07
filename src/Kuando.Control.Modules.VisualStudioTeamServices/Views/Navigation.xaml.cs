using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Kuando.Control.Modules.VisualStudioTeamServices.Views
{
    /// <summary>
    /// Interaction logic for Navigation.xaml
    /// </summary>
    [Export]
    public partial class Navigation : UserControl
    {
        #region Constructors

        public Navigation()
        {
            this.InitializeComponent();
        }

        #endregion
    }
}
