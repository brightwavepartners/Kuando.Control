using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows.Automation;

namespace Kuando.Control.Modules.GoogleHangouts.Models
{
    [Export(typeof(IHangout))]
    public class ChromeHangout : IHangout
    {
        #region Fields

        private const string BrowserNewTabText = "New Tab";
        private const string BrowserGoogleHangoutsTabText = "Google Hangouts";
        private const string ChromeProcessName = "chrome";

        #endregion

        #region Methods

        // BUG: there is a bug here where this routine only notices that a hangout is running if the hangouts window is on top.
        // BUG: if the hangouts window is not the topmost chrome tab, this routine will not notice hangouts is running.
        public bool IsActive()
        {
            var chromeProcesses = Process.GetProcessesByName(ChromeProcessName);

            if (!chromeProcesses.Any() || string.IsNullOrEmpty(BrowserGoogleHangoutsTabText))
            {
                return false;
            }

            foreach (var chromeProcess in chromeProcesses)
            {
                var mainWindowHandle = chromeProcess.MainWindowHandle;

                if (mainWindowHandle == IntPtr.Zero)
                {
                    continue;
                }

                // to find the tabs we first need to locate something reliable - the 'New Tab' button 
                var root = AutomationElement.FromHandle(chromeProcess.MainWindowHandle);
                var condNewTab = new PropertyCondition(AutomationElement.NameProperty, BrowserNewTabText);
                var elmNewTab = root.FindFirst(TreeScope.Descendants, condNewTab);

                // get the tabstrip by getting the parent of the 'new tab' button 
                var treewalker = TreeWalker.ControlViewWalker;

                if (elmNewTab != null)
                {
                    var elmTabStrip = treewalker.GetParent(elmNewTab);

                    // loop through all the tabs and get the names which is the page title 
                    Condition condTabItem =
                        new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);

                    return elmTabStrip.FindAll(TreeScope.Children, condTabItem)
                        .Cast<AutomationElement>()
                        .Where(tabitem => tabitem.Current.Name.Contains(BrowserGoogleHangoutsTabText)).ToList().Any();
                }
            }

            return false;
        }

        #endregion
    }
}
