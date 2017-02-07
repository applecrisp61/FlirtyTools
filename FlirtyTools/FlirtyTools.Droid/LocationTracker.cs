using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using FlirtyTools;
using FlirtyLocation;

namespace FlirtyTools.Droid {
    class LocationTracker : ILocationTracker {
        public event EventHandler<ILocationMeasurementsUpdatedEventArgs> LocationsChanged;
        public event EventHandler<IEnumerable<LocationMeasurement>> LocationMeasurementPacketReady;

        public void StartTracking() {
            throw new NotImplementedException();
        }

        public void StopTracking() {
            throw new NotImplementedException();
        }
    }
}