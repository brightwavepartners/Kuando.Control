using System.Collections.Generic;
using Kuando.Control.Infrastructure.Models;

namespace Kuando.Control.Infrastructure.Repositories
{
    public interface IRepository
    {
        #region Methods

        void Add(BusyLight busyLight);

        IEnumerable<BusyLight> GetAll();

        void Remove(BusyLight busyLight);


        #endregion
    }
}
