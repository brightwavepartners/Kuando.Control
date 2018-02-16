using System.ComponentModel.Composition;
using SHDocVw;

namespace Kuando.Control.Modules.GoogleHangouts.Models
{
    [Export(typeof(IHangout))]
    public class InternetExplorerHangout : IHangout
    {
        #region Fields

        private const string BrowserGoogleHangoutsUrl = "hangouts.google.com";

        #endregion

        #region Methods

        public bool IsHangoutActive()
        {
            foreach (InternetExplorer ieInst in new ShellWindows())
            {
                var url = ieInst.LocationURL;
                return url.Contains(BrowserGoogleHangoutsUrl);
            }

            return false;
        }

        #endregion
    }
}
