using System;

using FlirtyLocation;


namespace FlirtyTools {
    public class DeviceLocation {
        // Required
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime DateTimeStamp { get; set; }

        // Optional
        public double? Elevation { get; set; }
        public int? BuildingFloor { get; set; }
        public double? Speed { get; set; }
        public double? Bearing { get; set; }
        public double? HorizontalAccuracy { get; set; }
        public double? VerticalAccuracy { get; set; }

		public override string ToString() {
			return string.Format($"Device Location: {(new GeographicLocation(Latitude, Longitude)).ToString()} at {DateTimeStamp.ToLocalTime()}");
		}
    }
}
