using Kuando.Control.Modules.GoogleHangouts.Models;

namespace Kuando.Control.Modules.GoogleHangouts.Repositories
{
    public interface IRepository
    {
        #region Methods

        void Get(string key);

        void Update(Setting setting);

        #endregion
    }
}
