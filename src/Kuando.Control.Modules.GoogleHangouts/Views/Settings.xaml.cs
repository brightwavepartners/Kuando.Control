using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Kuando.Control.Modules.GoogleHangouts.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    [Export]
    public partial class Settings : UserControl
    {
        #region Constructors

        public Settings()
        {
            this.InitializeComponent();
        }


        #endregion
    }
}
