using System.Collections.Generic;

namespace FlirtyTools {
    public interface IDeviceLocationUpdatedEventArgs {

        IEnumerable<DeviceLocation> Locations { get; }
    }
}
