using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using FlirtyLocation;

namespace FlirtyTools {
    public class LocationMeasurementsUpdatedEventArgs {

        public IEnumerable<LocationMeasurement> Measurements => _measurements;

        // Private / internal implementation
        // ReSharper disable once FieldCanBeMadeReadOnly.Local 
        // (don't like marking it as readonly: true, but since it is a list intended for modification, feel it misleads)
        private List<LocationMeasurement> _measurements = new List<LocationMeasurement>();

        public LocationMeasurementsUpdatedEventArgs(IDeviceLocationUpdatedEventArgs args, int trackid, string trackname,
            int userid, string username, string provider, bool isfake) {
            foreach (var location in args.Locations) {
                var newMeasurement = GenerateLocationMeasurement(location, Guid.NewGuid().ToString(), trackid, trackname, userid, username, provider, isfake);
                _measurements.Add(newMeasurement);
            }
        }

        public static LocationMeasurement GenerateLocationMeasurement(DeviceLocation loc, string id, int trackid,
            string trackname, int userid, string username, string provider, bool isfake) {
            return new LocationMeasurement {
                Id = id,
                TrackId = trackid,
                TrackName = trackname,
                UserId = userid,
                UserName = username,
                Latitude = loc.Latitude,
                Longitude = loc.Longitude,
                DateTimeStamp = loc.DateTimeStamp,
                Provider = provider,
                IsFromMockProvider = isfake,
                Elevation = loc.Elevation,
                BuildingFloor = loc.BuildingFloor,
                Speed = loc.Speed,
                Bearing = loc.Bearing, // positive degrees of movement vector relative to north
                HorizontalAccuracy = loc.HorizontalAccuracy,
                VerticalAccuracy = loc.VerticalAccuracy
            };
        }
    }
}
