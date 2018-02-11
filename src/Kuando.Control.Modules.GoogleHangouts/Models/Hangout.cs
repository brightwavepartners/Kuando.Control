using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Automation;
using Kuando.Control.Infrastructure.Events;
using Kuando.Control.Infrastructure.Models;
using Kuando.Control.Modules.GoogleHangouts.Repositories;
using Prism.Events;

namespace Kuando.Control.Modules.GoogleHangouts.Models
{
    [Export]
    public class Hangout
    {
        #region Fields

        private const string BrowserNewTabText = "New Tab";
        private const string BrowserGoogleHangoutsTabText = "Google Hangouts";
        private const string ChromeProcessName = "chrome";

        private bool _isActive;

        public event EventHandler<ActiveChangedEventArgs> OnActiveChanged;

        #endregion

        #region Properties

        public bool IsActive
        {
            get => this._isActive;

            private set
            {
                if (value == this._isActive)
                {
                    return;
                }

                this._isActive = value;

                Debug.WriteLine(this._isActive
                    ? $"DEBUG: Google Hangouts is active"
                    : $"DEBUG: Google Hangouts is not active");

                this.RaiseActiveChanged();
            }
        }

        #endregion

        #region Methods

        public async void Monitor()
        {
            while (true)
            {
                var chromeProcesses = Process.GetProcessesByName(ChromeProcessName);

                if (!chromeProcesses.Any() || string.IsNullOrEmpty(BrowserGoogleHangoutsTabText))
                {
                    this.IsActive = false;
                }
                else
                {
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

                            this.IsActive = elmTabStrip.FindAll(TreeScope.Children, condTabItem)
                                .Cast<AutomationElement>()
                                .Where(tabitem => tabitem.Current.Name.Contains(BrowserGoogleHangoutsTabText)).ToList().Any();
                        }
                    }
                }

                await Task.Delay(5000);
            }

        }

        protected virtual void RaiseActiveChanged()
        {
            var handler = this.OnActiveChanged;

            handler?.Invoke(this, new ActiveChangedEventArgs(this.IsActive));
        }

        #endregion
    }
}
