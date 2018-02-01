using System.Windows;
using Application = System.Windows.Application;

namespace Kuando.Control
{
    /// <inheritdoc />
    public partial class App : Application
    {
        #region Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

        #endregion
    }
}
