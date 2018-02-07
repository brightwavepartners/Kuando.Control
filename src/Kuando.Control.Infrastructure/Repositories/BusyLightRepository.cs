using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Busylight;
using Kuando.Control.Infrastructure.Models;

namespace Kuando.Control.Infrastructure.Repositories
{
    [Export]
    public class BusyLightRepository : IRepository
    {
        #region Fields

        // this mapping allows us to associate a business object busylight with an actual kuando busylight instance
        private readonly Dictionary<BusyLight, BusylightDevice> _busyLights = new Dictionary<BusyLight, BusylightDevice>();

        #endregion

        #region Constructors

        public BusyLightRepository()
        {
            var busyLightSdk = new SDK();

            var busyLights = busyLightSdk.GetAttachedBusylightDeviceList();

            foreach (var busyLight in busyLights)
            {
                this._busyLights.Add(new BusyLight {Color = Color.Off, Id = busyLight.USBID}, busyLight);
            }
        }

        #endregion

        #region Methods

        public void Add(BusyLight busyLight)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BusyLight> GetAll()
        {
            return this._busyLights.Select(dictionaryItem => dictionaryItem.Key);
        }

        public void Remove(BusyLight busyLight)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
