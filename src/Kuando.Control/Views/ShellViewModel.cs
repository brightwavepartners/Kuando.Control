using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Application = System.Windows.Application;

namespace Kuando.Control.Views
{
    [Export]
    public class ShellViewModel : BindableBase
    {
        #region Fields

        private bool _isExit;
        private NotifyIcon _notifyIcon;

        #endregion

        #region Constructors

        public ShellViewModel()
        {
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Closing += this.OnShellClosing;
            }

            this._notifyIcon = new NotifyIcon();
            this._notifyIcon.DoubleClick += (s, args) => this.ShowShell();
            this._notifyIcon.Icon = Properties.Resources.kuando;
            this._notifyIcon.Visible = true;

            this.CreateContextMenu();

            this.WindowClosing = new DelegateCommand<Window>(this.OnWindowClosing);
        }

        #endregion

        #region Properties

        public ICommand WindowClosing { get; }

        #endregion

        #region Methods

        private void CreateContextMenu()
        {
            this._notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            this._notifyIcon.ContextMenuStrip.Items.Add("Open").Click += (sender, args) => this.ShowShell();
            this._notifyIcon.ContextMenuStrip.Items.Add("-");
            this._notifyIcon.ContextMenuStrip.Items.Add("Red");
            this._notifyIcon.ContextMenuStrip.Items.Add("Yellow");
            this._notifyIcon.ContextMenuStrip.Items.Add("Green");
            this._notifyIcon.ContextMenuStrip.Items.Add("-");
            this._notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (senders, args) => this.ExitApplication();
        }


        private void ExitApplication()
        {
            this._isExit = true;
            Application.Current.MainWindow?.Close();
            this._notifyIcon.Dispose();
            this._notifyIcon = null;
        }

        private void OnShellClosing(object sender, CancelEventArgs e)
        {
            if (this._isExit)
            {
                return;
            }

            e.Cancel = true;

            Application.Current.MainWindow?.Hide(); // a hidden window can be shown again, a closed one not
        }

        private void OnWindowClosing(Window e)
        {
            Debug.WriteLine("OnWindowClosing");
        }

        private void ShowShell()
        {
            var mainWindow = Application.Current.MainWindow;

            if (mainWindow == null)
            {
                return;
            }

            if (mainWindow.IsVisible)
            {
                if (mainWindow.WindowState == WindowState.Minimized)
                {
                    mainWindow.WindowState = WindowState.Normal;
                }

                mainWindow.Activate();
            }
            else
            {
                mainWindow.Show();
            }
        }

        #endregion
    }
}
