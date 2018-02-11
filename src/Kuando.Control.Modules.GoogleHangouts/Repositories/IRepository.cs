using System.Collections.Generic;

namespace Kuando.Control.Modules.GoogleHangouts.Repositories
{
    public interface IRepository<T>
    {
        #region Properties

        T this[object key] { get; set; }

        #endregion

        #region Methods

        void Add(T item);

        IList<T> FindAll();

        void Remove(T item);

        #endregion
    }
}
