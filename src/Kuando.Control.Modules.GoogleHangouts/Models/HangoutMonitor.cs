using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace Kuando.Control.Modules.GoogleHangouts.Models
{
    [Export]
    public class HangoutMonitor
    {
        #region Fields

        private CancellationToken _hangoutMonitorCancellationToken;
        private CancellationTokenSource _hangoutMonitorCancellationTokenSource;
        private Task _hangoutMonitorTask;
        private readonly IEnumerable<IHangout> _hangouts;
        private bool _isActive;

        public event EventHandler<ActiveChangedEventArgs> OnActiveChanged;

        #endregion

        #region Constructor

        [ImportingConstructor]
        public HangoutMonitor([ImportMany]IEnumerable<IHangout> hangouts)
        {
            this._hangouts = hangouts;
        }

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

        public void StartMonitor()
        {
            this._hangoutMonitorCancellationTokenSource = new CancellationTokenSource();
            this._hangoutMonitorCancellationToken = this._hangoutMonitorCancellationTokenSource.Token;

            this._hangoutMonitorTask = new Task(this.Monitor, this._hangoutMonitorCancellationToken, TaskCreationOptions.LongRunning);

            this._hangoutMonitorTask.Start();
        }

        public void StopMonitor()
        {
            this._hangoutMonitorCancellationTokenSource.Cancel();
        }

        private async void Monitor()
        {
            while (!this._hangoutMonitorCancellationTokenSource.IsCancellationRequested)
            {
                Debug.WriteLine("DEBUG: Checking for hangouts");

                this.IsActive = this._hangouts.Any(hangout => hangout.IsHangoutActive());
                
                try
                {
                    await Task.Delay(5000, this._hangoutMonitorCancellationToken);
                }
                catch (TaskCanceledException)
                {
                    this._hangoutMonitorCancellationTokenSource.Dispose();
                }
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
