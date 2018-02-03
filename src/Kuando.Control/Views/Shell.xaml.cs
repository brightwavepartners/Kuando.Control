using MahApps.Metro.Controls;
using System.ComponentModel.Composition;

namespace Kuando.Control.Views
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    [Export]
    public partial class Shell : MetroWindow
    {
        #region Constructors

        public Shell()
        {
            this.InitializeComponent();
        }

        #endregion
    }
}
