using System.ComponentModel.Composition;
using System.Windows;

namespace Kuando.Control.Views
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    [Export]
    public partial class Shell : Window
    {
        #region Constructors

        public Shell()
        {
            this.InitializeComponent();
        }

        #endregion
    }
}
