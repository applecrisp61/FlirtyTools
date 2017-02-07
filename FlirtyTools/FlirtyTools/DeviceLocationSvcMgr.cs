using System;

using Xamarin.Forms;

namespace FlirtyTools {
    public class DeviceLocationSvcMgr :ILocationServicesManager {
        private ILocationServicesManager mgr = DependencyService.Get<ILocationServicesManager>();

        public EnumLocationAuthorizationStates AuthorizationStatus {
            get { return mgr.AuthorizationStatus; }
        }

        public void RequestWhenInUseAuthorization() {
            mgr.RequestWhenInUseAuthorization();
        }

        public void RequestAlwaysAuthorization() {
            mgr.RequestAlwaysAuthorization();
        }

        public void StartUpdatingLocation() {
            mgr.StartUpdatingLocation();
        }

        public void StopUpdatingLocation() {
            mgr.StopUpdatingLocation();
        }

        public void RequestLocation() {
            mgr.RequestLocation();
        }

        public void AllowDeferredLocationUpdatesUntil(double distance, double timeout) {
            mgr.AllowDeferredLocationUpdatesUntil(distance, timeout);
        }

        public void DisallowDeferredLocationUpdates() {
            mgr.DisallowDeferredLocationUpdates();
        }

        public event EventHandler<IDeviceLocationUpdatedEventArgs> DeviceLocationUpdated {
            add { mgr.DeviceLocationUpdated += value; }
            remove { mgr.DeviceLocationUpdated -= value; }
        }
    }
}
