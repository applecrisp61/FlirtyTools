using System;
using System.Collections.Generic;

using FlirtyLocation;


namespace FlirtyTools {

	// Purpose of this class: Make a deep copy of the current packet, which we can then pass along to more destinations
	// allowing the buffer holding the current measurements to reset and start filling again (without worrying about 
	// wiping out the data that we need)

	public class LocationMeasurementsPacketReadyEventArgs {

        public IEnumerable<LocationMeasurement> Packet => _packet;

		// Private / internal implementation
		// ReSharper disable once FieldCanBeMadeReadOnly.Local 
		// (don't like marking it as readonly: true, but since it is a list intended for modification, feel it misleads)
		private List<LocationMeasurement> _packet = new List<LocationMeasurement>();

		public LocationMeasurementsPacketReadyEventArgs(IEnumerable<LocationMeasurement> args) {
			foreach (var measurement in args) {
				var deepCopy = new LocationMeasurement(measurement);
				_packet.Add(deepCopy);
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

