using System;

namespace FlirtyTools {
    public interface ILocationServicesManager {

        EnumLocationAuthorizationStates AuthorizationStatus { get; }
        void RequestWhenInUseAuthorization();
        void RequestAlwaysAuthorization();

        void StartUpdatingLocation();
        void StopUpdatingLocation();
        void RequestLocation();

        void AllowDeferredLocationUpdatesUntil(double distance, double timeout);
        void DisallowDeferredLocationUpdates();

        event EventHandler<IDeviceLocationUpdatedEventArgs> DeviceLocationUpdated;

    }

}
