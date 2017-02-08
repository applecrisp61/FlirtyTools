using System;
using System.Collections.Generic;

using CoreLocation;
using Foundation;

using Xamarin.Forms;

[assembly: Dependency(typeof(FlirtyTools.DeviceLocationSvcMgr))]
namespace FlirtyTools.iOS {
    public class DeviceLocationSvcMgr : ILocationServicesManager {

        public EnumLocationAuthorizationStates AuthorizationStatus {
            get {
                switch (CLLocationManager.Status) {
                case CLAuthorizationStatus.NotDetermined:
                        return EnumLocationAuthorizationStates.NotDetermined;
                case CLAuthorizationStatus.Restricted:
                        return EnumLocationAuthorizationStates.Restricted;
                case CLAuthorizationStatus.Denied:
                        return EnumLocationAuthorizationStates.Denied;
                case CLAuthorizationStatus.AuthorizedAlways:
                        return EnumLocationAuthorizationStates.AuthorizedAlways;
                case CLAuthorizationStatus.AuthorizedWhenInUse:
                        return EnumLocationAuthorizationStates.AuthorizedWhenInUse;
                default:
                        return EnumLocationAuthorizationStates.OtherError;
                // Note that prior to iOS 8, there was no distinction between AuthorizedAlways
                // and AuthorizedWhenInUse. The CLLocationManager would return Authorized,
                // which is mapped to the same value as the current AuthorizedAlways (3)
                }
            }
        }

        public void RequestWhenInUseAuthorization() {
            _mgr.RequestWhenInUseAuthorization();
        }

        public void RequestAlwaysAuthorization() {
            _mgr.RequestAlwaysAuthorization();
        }

        public void StartUpdatingLocation() {
            _mgr.StartUpdatingLocation();
        }

        public void StopUpdatingLocation() {
            _mgr.StopUpdatingLocation();
        }

        public void RequestLocation() {
            _mgr.RequestLocation();
        }

        public void AllowDeferredLocationUpdatesUntil(double distance, double timeout) {
            _mgr.AllowDeferredLocationUpdatesUntil(distance, timeout);
        }

        public void DisallowDeferredLocationUpdates() {
            _mgr.DisallowDeferredLocationUpdates();
        }

        public event EventHandler<IDeviceLocationUpdatedEventArgs> DeviceLocationUpdated;

		public event EventHandler<CLAuthorizationChangedEventArgs> DeviceAuthorizationChanged;

        private readonly CLLocationManager _mgr = new CLLocationManager();

        // DOCUMENTATION: Measurements[] array
        // An array of CLLocation objects containing the location data. This array always contains 
        // at least one object representing the current location. If updates were deferred or if multiple 
        // locations arrived before they could be delivered, the array may contain additional entries. 
        // The objects in the array are organized in the order in which they occurred. Therefore, the most 
        // recent location update is at the end of the array.

        // Constructor
        public DeviceLocationSvcMgr() {

			/* DCL: This should be handled by my calling code 
            // Request authorization for iOS 8 and above.
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0)) {
                _mgr.RequestAlwaysAuthorization();
            }
			*/

            _mgr.LocationsUpdated += (sender, args) => {
                var deviceLocations = new List<DeviceLocation>();

                foreach (var location in args.Locations) {
                    var dl = new DeviceLocation {
                        Latitude = location.Coordinate.Latitude,
                        Longitude = location.Coordinate.Longitude,
                        DateTimeStamp = NsDateToDateTime(location.Timestamp),
                        Elevation = location.Altitude,
                        BuildingFloor = -1, // Need to figure out how to work with the Apple Floor variable
                        Speed = location.Speed,
                        Bearing = location.Course,
                        HorizontalAccuracy = location.HorizontalAccuracy,
                        VerticalAccuracy = location.VerticalAccuracy
                    };

                    deviceLocations.Add(dl);
                }

                // We have now transformed the iOS to our local cross-platform form... throw the local form
                var deviceArgs = new DeviceLocationUpdatedEventArgs(deviceLocations);
                DeviceLocationUpdated?.Invoke(this, deviceArgs);

            };

			// Wrapper to rethrow the Native iOS AuthorizationChanged event (though I am handling it
			// via C# style events rather than Apple's delegate-object pattern
			_mgr.AuthorizationChanged += (sender, e) => {
				DeviceAuthorizationChanged?.Invoke(this, e);
			};

        }

        // Utility helpers
        internal static DateTime NsDateToDateTime(NSDate date) {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
                new DateTime(2001, 1, 1, 0, 0, 0));
            return reference.AddSeconds(date.SecondsSinceReferenceDate);
        }

        internal static NSDate DateTimeToNsDate(DateTime date) {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(
                new DateTime(2001, 1, 1, 0, 0, 0));
            return NSDate.FromTimeIntervalSinceReferenceDate(
                (date - reference).TotalSeconds);
        }
    }
}