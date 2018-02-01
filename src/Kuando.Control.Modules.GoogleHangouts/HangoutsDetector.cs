using Busylight;
using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Automation;
using Condition = System.Windows.Automation.Condition;

namespace Kuando.Control.Modules.GoogleHangouts
{
    [Export]
    public class HangoutsDetector
    {
        #region Methods

        public void Run()
        {
            var busyLight = new SDK();

            var busyLights = busyLight.GetAttachedBusylightDeviceList();

            busyLight.Light(BusylightColor.Green);

            while (true)
            {
                var chromeProcesses = Process.GetProcessesByName("chrome");

                if (!busyLights.Any() || !chromeProcesses.Any())
                {
                    busyLight.Light(BusylightColor.Green);
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
                        var condNewTab = new PropertyCondition(AutomationElement.NameProperty, "New Tab");
                        var elmNewTab = root.FindFirst(TreeScope.Descendants, condNewTab);

                        // get the tabstrip by getting the parent of the 'new tab' button 
                        var treewalker = TreeWalker.ControlViewWalker;

                        if (elmNewTab != null)
                        {
                            var elmTabStrip = treewalker.GetParent(elmNewTab);

                            // loop through all the tabs and get the names which is the page title 
                            Condition condTabItem =
                                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);

                            busyLight.Light(
                                elmTabStrip.FindAll(TreeScope.Children, condTabItem).Cast<AutomationElement>()
                                    .Where(tabitem => tabitem.Current.Name.Contains("Google Hangouts")).ToList().Any()
                                    ? BusylightColor.Red
                                    : BusylightColor.Green);
                        }
                    }
                }

                Thread.Sleep(5000);
            }

        }

        #endregion
    }
}
