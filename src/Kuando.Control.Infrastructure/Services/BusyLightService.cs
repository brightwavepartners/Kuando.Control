using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using Kuando.Control.Infrastructure.Events;
using Kuando.Control.Infrastructure.Models;
using Kuando.Control.Infrastructure.Repositories;
using Prism.Events;

namespace Kuando.Control.Infrastructure.Services
{
    [Export]
    public class BusyLightService
    {
        #region Fields

        private readonly BusyLightRepository _busyLightRepository;
        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Constructors

        [ImportingConstructor]
        public BusyLightService(IEventAggregator eventAggregator, BusyLightRepository busyLightRepository)
        {
            this._busyLightRepository = busyLightRepository;
            this._eventAggregator = eventAggregator;

            this._eventAggregator.GetEvent<BusyLightColorEvent>().Subscribe(this.SetColor, ThreadOption.PublisherThread);

        }

        #endregion

        #region Methods

        private void SetColor(Color color)
        {
            Debug.WriteLine($"DEBUG: Setting color for any attached BusyLights to {color}");

            foreach (var busyLight in this._busyLightRepository.GetAll().ToList())
            {
                busyLight.Color = color;
            }
        }

        #endregion
    }
}
