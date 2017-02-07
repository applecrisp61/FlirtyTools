/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using FlirtyLocation;

namespace FlirtyTools {
    class FakeLocationServicesManager : ILocationServicesManager {

        private ILocationServicesManager _svcmgr = DependencyService.Get<ILocationServicesManager>();

        public void RequestWhenInUseAuthorization() {
            throw new NotImplementedException("RequestWhenInUseAuthorization()");
        }

        public void RequestAlwaysAuthorization() {
            throw new NotImplementedException("RequestAlwaysAuthorization()");
        }

        public void StartUpdatingLocation() {
            throw new NotImplementedException("StartUpdatingLocation()");
        }

        public void StopUpdatingLocation() {
            throw new NotImplementedException("StopUpdatingLocation()");
        }

        public void RequestLocation() {
            throw new NotImplementedException("RequestLocation()");
        }

        public void AllowDeferredLocationUpdatesUntil(double distance, double timeout) {
            throw new NotImplementedException("AllowDeferredLocationUpdatesUntil()");
        }

        public void DisallowDeferredLocationUpdates() {
            throw new NotImplementedException("DisallowDeferredLocationUpdates()");
        }

        public event EventHandler<ILocationMeasurementsUpdatedEventArgs> MeasurementsUpdated {
            add { _svcmgr.DeviceLocationUpdated += value; }
            remove { _svcmgr.DeviceLocationUpdated -= value; }
        }
    }
}
*/