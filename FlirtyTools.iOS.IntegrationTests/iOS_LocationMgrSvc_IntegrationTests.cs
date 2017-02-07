using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace FlirtyTools.iOS.IntegrationTests {
    [TestFixture]
    public class iOS_LocationMgrSvc_IntegrationTests {
        [Test]
        public void Pass() {
            Assert.True(true);
        }

        [Test]
        public void Fail() {
            Assert.False(true);
        }

        [Test]
        [Ignore("another time")]
        public void Ignore() {
            Assert.True(false);
        }

        [Test]
        public void AuthorizationStatusCheck_NoneYetSet_ReturnsNotDetermined() {
            var mgr = new DeviceLocationSvcMgr();

            var status = mgr.AuthorizationStatus;
            Assert.AreEqual(EnumLocationAuthorizationStates.NotDetermined, status);
        }

        [Test]
        public async Task AuthorizationCanBeGranted_UserApprovesAlways_ReturnsAuthorizedAlways() {
            
            var mgr = new DeviceLocationSvcMgr();

            await Task.Run(() => {
                    mgr.RequestAlwaysAuthorization(); // User must accept for this test to pass
            }); 

            var status = mgr.AuthorizationStatus;
            Assert.AreEqual(EnumLocationAuthorizationStates.AuthorizedAlways, status);
        }

        [Test]
        public void AuthorizationIsMaintained_AlwaysAlreadyApproved_ReturnsAuthorizedAlways() {

            var mgr = new DeviceLocationSvcMgr();

            var status = mgr.AuthorizationStatus;
            Assert.AreEqual(EnumLocationAuthorizationStates.AuthorizedAlways, status);
        }

        [Test]
        public async Task AuthorizationCanBeChanged_UserApprovesWhenInUse_ReturnsAuthorizedWhenInUse() {

            var mgr = new DeviceLocationSvcMgr();

            await Task.Run(() => {
                mgr.RequestWhenInUseAuthorization(); // User must accept for this test to pass
            });

            var status = mgr.AuthorizationStatus;
            Assert.AreEqual(EnumLocationAuthorizationStates.AuthorizedWhenInUse, status);
        }
    }
}