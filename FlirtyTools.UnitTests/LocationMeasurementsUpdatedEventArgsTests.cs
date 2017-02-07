using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

using static FlirtyLocation.ConstantsRt;
using static FlirtyTools.TestConstants;

namespace FlirtyTools.UnitTests {

    [TestFixture]
    public class LocationMeasurementsUpdatedEventArgsTests {
        [Test]
        [SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        public void GenerateLocationMeasurement_ValidDevideLocation_ProperlyInitialized() {

            var tLat = BEACON_KITE_HILL.Latitude;
            var tLon = BEACON_KITE_HILL.Longitude;
            var tDts = new DateTime(2016, 03, 14, 15, 14, 15, 900);
            double? tElev = 42.42;
            int? tFloor = 13;
            double? tSpeed = 1.5;
            double? tCourse = 45.67;
            double? tHAcc = 5.5;
            double? tVAcc = 3.5;

            var testLoc = new DeviceLocation { 
                Latitude = tLat,
                Longitude = tLon,
                DateTimeStamp = tDts,
                Elevation = tElev,
                BuildingFloor = tFloor,
                Speed = tSpeed,
                Bearing = tCourse,
                HorizontalAccuracy = tHAcc,
                VerticalAccuracy = tVAcc};

            string id = Guid.NewGuid().ToString();
            int trackId = APPLE_TEST_PREFIX + 60;
            string trackName = "iOS track";
            int userId = APPLE_TEST_PREFIX + 61;
            string userName = "Lienad";
            string provider = "Method default";
            bool isFromFakeProvider = true;

            

            var converted = LocationMeasurementsUpdatedEventArgs.GenerateLocationMeasurement(testLoc,
                id,
                trackId,
                trackName,
                userId,
                userName,
                provider,
                isFromFakeProvider);

            Assert.AreEqual(converted.Latitude, tLat);
            Assert.AreEqual(converted.Longitude, tLon);
            Assert.IsTrue(converted.DateTimeStamp.Equals(tDts));
            Assert.AreEqual(converted.Elevation.Value, tElev);
            Assert.AreEqual(converted.Speed.Value, tSpeed);
            Assert.AreEqual(converted.Bearing.Value, tCourse);
            Assert.AreEqual(converted.HorizontalAccuracy.Value, tHAcc);
            Assert.AreEqual(converted.VerticalAccuracy.Value, tVAcc);
        }
    }
}
