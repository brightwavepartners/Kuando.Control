using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Windows.Controls;

namespace Kuando.Control.Modules.SkypeForBusiness.Views
{
    /// <summary>
    /// Interaction logic for SkypeForBusiness.xaml
    /// </summary>
    [Export]
    public partial class SkypeForBusinessNavigation : UserControl
    {
        public SkypeForBusinessNavigation()
        {
            this.InitializeComponent();
        }
    }
}
