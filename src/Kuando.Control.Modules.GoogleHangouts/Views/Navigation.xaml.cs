using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Kuando.Control.Modules.GoogleHangouts.Views
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
