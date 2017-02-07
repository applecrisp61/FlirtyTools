using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FlirtyLocation;

namespace FlirtyTools {
    public class LocationTracker {

        public event EventHandler<LocationMeasurementsUpdatedEventArgs> MeasurementsUpdated;
        public event EventHandler<IEnumerable<LocationMeasurement>> LocationMeasurementPacketReady;

        public void StartTracking() {
            _mgr.StartUpdatingLocation();
        }

        public void StopTracking() {
            _mgr.StopUpdatingLocation();
        }

        public LocationTracker(ILocationServicesManager mgr,
            int trackid, string trackname, int userid, string username, string provider, bool isfake) {

            _mgr = mgr;

            _trackid = trackid;
            _trackname = trackname;
            _userid = userid;
            _username = username;
            _provider = provider;
            _isfake = isfake;

            // Note: The last items on the list are the most recent locations
            mgr.DeviceLocationUpdated += (sender, args) => {
                var margs = DeviceLocationToMeasurementEventArgs(args);
                MeasurementsUpdated?.Invoke(this, margs);

                foreach (var measurement in margs.Measurements) {
                    _locationMeasurementsPacket.Add(measurement);
                }

                if (_locationMeasurementsPacket.Count > PacketReadySize) {
                    LocationMeasurementPacketReady?.Invoke(this, _locationMeasurementsPacket); // Send if someone is listening
                    _locationMeasurementsPacket.Clear(); // Clear irrespective of having a listener
                }
            };
        }


        // INTERNAL IMPLEMENTATION: Eventhing here and below should be private or internal (TOOD :)
        private readonly ILocationServicesManager _mgr;
        [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
        private List<LocationMeasurement> _locationMeasurementsPacket = new List<LocationMeasurement>();

        private readonly int _trackid;
        private readonly string _trackname;
        private readonly int _userid;
        private readonly string _username;
        private readonly string _provider;
        private readonly bool _isfake;

        private static readonly int PacketReadySize = 10;

        internal LocationMeasurementsUpdatedEventArgs DeviceLocationToMeasurementEventArgs(IDeviceLocationUpdatedEventArgs args) {
            return new LocationMeasurementsUpdatedEventArgs(args, _trackid, _trackname, _userid, _username, _provider, _isfake);
        }
    }
}
