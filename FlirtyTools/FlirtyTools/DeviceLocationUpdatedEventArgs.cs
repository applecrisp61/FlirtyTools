using System.Collections.Generic;

namespace FlirtyTools {
    public class DeviceLocationUpdatedEventArgs : IDeviceLocationUpdatedEventArgs {

        public IEnumerable<DeviceLocation> Locations { get; }

        public DeviceLocationUpdatedEventArgs(IEnumerable<DeviceLocation> locs) {
            Locations = locs;
        }
    }
}