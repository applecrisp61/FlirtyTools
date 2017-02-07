using System;
using System.Collections.Generic;

using CoreLocation;
using Foundation;

using NUnit.Framework;

using static FlirtyLocation.ConstantsRt;


namespace FlirtyTools.iOS.UnitTests {

    [TestFixture]
    public class iOS_LocationSvcMgr_UnitTests {
        [Test]
        public void Pass() {
            Assert.True(true);
        }

        [Test]
        public void NsDateToDateTime_ValidNSDate_ReturnsSameDateTime() {
            var testDateTime = new DateTime(1979, 11, 12, 11, 12, 11, 121);

            var components = new NSDateComponents {
                Year = testDateTime.Year,
                Month = testDateTime.Month,
                Day = testDateTime.Day,
                Hour = testDateTime.Hour,
                Minute = testDateTime.Minute,
                Second = testDateTime.Second,
                Nanosecond = testDateTime.Millisecond * 1000000
            };

            NSCalendar calendar = new NSCalendar(NSCalendarType.Gregorian);
            var nsDate = calendar.DateFromComponents(components);

            var calculatedDateTime = DeviceLocationSvcMgr.NsDateToDateTime(nsDate);
            Assert.IsTrue(testDateTime.Equals(calculatedDateTime));
        }

        [Test]
        public void NsDateToDateTime_ValidNSDateToNanoSecond_RoundToMillisecondDown() {
            var testDateTime = new DateTime(1979, 11, 12, 11, 12, 11, 121);

            var components = new NSDateComponents {
                Year = testDateTime.Year,
                Month = testDateTime.Month,
                Day = testDateTime.Day,
                Hour = testDateTime.Hour,
                Minute = testDateTime.Minute,
                Second = testDateTime.Second,
                Nanosecond = testDateTime.Millisecond * 1000000 + 499999
            };

            NSCalendar calendar = new NSCalendar(NSCalendarType.Gregorian);
            var nsDate = calendar.DateFromComponents(components);

            var calculatedDateTime = DeviceLocationSvcMgr.NsDateToDateTime(nsDate);
            Assert.IsTrue(testDateTime.Equals(calculatedDateTime), $"msec: {calculatedDateTime.Millisecond}");
        }

        [Test]
        public void NsDateToDateTime_ValidNSDateToNanoSecond_RoundToMillisecondUp() {
            var testDateTime = new DateTime(1979, 11, 12, 11, 12, 11, 121);

            var components = new NSDateComponents {
                Year = testDateTime.Year,
                Month = testDateTime.Month,
                Day = testDateTime.Day,
                Hour = testDateTime.Hour,
                Minute = testDateTime.Minute,
                Second = testDateTime.Second,
                Nanosecond = testDateTime.Millisecond * 1000000 + 500000
            };

            NSCalendar calendar = new NSCalendar(NSCalendarType.Gregorian);
            var nsDate = calendar.DateFromComponents(components);

            var calculatedDateTime = DeviceLocationSvcMgr.NsDateToDateTime(nsDate);
            Assert.IsTrue(testDateTime.Equals(calculatedDateTime), $"msec: {calculatedDateTime.Millisecond}");
        }


    }




    
    class TCLLocationManager : CLLocationManager {

        public EventHandler<CLLocationsUpdatedEventArgs> FakeLocationsUpdated;

        public virtual void RaiseFakeLocationsUpdated(CLLocation[] updates) {
            CLLocationsUpdatedEventArgs args = new CLLocationsUpdatedEventArgs(updates);

            FakeLocationsUpdated?.Invoke(this, args);
        }


    }
    

    class TCLLocation : CLLocation {

        internal TCLLocation(CLLocationCoordinate2D loc, NSDate dts, double elev, CLFloor floor,
            double speed, double course, double horizAccuracy, double vertAccuracy) {
            Coordinate = loc;
            Timestamp = dts;
            Altitude = elev;
            Floor = floor;
            Speed = speed;
            Course = course;
            HorizontalAccuracy = horizAccuracy;
            VerticalAccuracy = vertAccuracy;
        }

        public override CLLocationCoordinate2D Coordinate { get; }
        public override NSDate Timestamp { get; }
        public override double Altitude { get; }
        public override CLFloor Floor { get; }
        public override double HorizontalAccuracy { get; }
        public override double VerticalAccuracy { get; }
        public override double Speed { get; }
        public override double Course { get; }

        // STATIC DEFAULT values to ease testing
        static double _tLat = BEACON_KITE_HILL.Latitude;
        static double _tLon = BEACON_KITE_HILL.Longitude;
        static DateTime _tDts = new DateTime(2016, 03, 14, 15, 14, 15, 900);
        static double _tElev = 42.42;
        static CLFloor _tFloor = new CLFloor();
        static double _tSpeed = 1.5;
        static double _tCourse = 45.67;
        static double _tHAcc = 5.5;
        static double _tVAcc = 3.5;

        public static CLLocation DefaultTCLocationFactory() {
            return new TCLLocation (
                new CLLocationCoordinate2D(_tLat, _tLon),
                DeviceLocationSvcMgr.DateTimeToNsDate(_tDts),
                _tElev,
                _tFloor,
                _tSpeed,
                _tCourse,
                _tHAcc,
                _tVAcc
            );
        }
        
    }



}


/*
 * private CLLocationCoordinate2D _loc = new CLLocationCoordinate2D(BEACON_KITE_HILL.Latitude, BEACON_KITE_HILL.Longitude);
        private NSDate _dts = LocationTracker.DateTimeToNsDate(new DateTime(2016, 03, 14, 15, 14, 15, 900));
        private double _elev = 42.42;
        private CLFloor _floor = new CLFloor();
        private double _speed = new nfloat(1.2);
        private double _direction = new nfloat(45.5);
        private double _horizAccuracy = new nfloat(20.5);
        private double _vertAccuracy = new nfloat(3.14);
*/


/*
Id = new Guid().ToString(),
                TrackId = apple_test_prefix + 61,
                TrackName = "iOS track",
                UserId = apple_test_prefix + 1,
                UserName = "Lienad",
                Latitude = loc.Coordinate.Latitude,
                Longitude = loc.Coordinate.Longitude,
                DateTimeStamp = NsDateToDateTime(loc.Timestamp),
                Provider = "iPhone",
                IsFromMockProvider = false,
                Elevation = loc.Altitude,
                BuildingFloor = (int)loc.Floor.Level,
                Speed = loc.Speed,
                Bearing = loc.Course, // positive degrees of movement vector relative to north
                HorizontalAccuracy = loc.HorizontalAccuracy,
                VerticalAccuracy = loc.VerticalAccuracy
                */
