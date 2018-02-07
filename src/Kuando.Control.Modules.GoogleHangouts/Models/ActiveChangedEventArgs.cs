using System;

namespace Kuando.Control.Modules.GoogleHangouts.Models
{
    public class ActiveChangedEventArgs : EventArgs
    {
        #region Constructor

        public ActiveChangedEventArgs(bool active)
        {
            this.Active = active;
        }

        #endregion

        #region Properties

        public bool Active { get; }

        #endregion
    }
}
