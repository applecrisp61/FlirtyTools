using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocationMeasurementClassLibrary;

namespace FlirtyTools.UWP {
    class LocationTracker : ILocationTracker {
        public event EventHandler<LocationMeasurement> LocationChanged;
        public event EventHandler<IEnumerable<LocationMeasurement>> LocationMeasurementPacketReady;

        public void StartTracking() {
            throw new NotImplementedException();
        }

        public void StopTracking() {
            throw new NotImplementedException();
        }
    }
}
