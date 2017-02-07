using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.iOS;

namespace FlirtyTools.iOS.UI_IntegrationTests {
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class iOS_LocationSvcMgr_IntegrationTests {
        iOSApp app;

        [SetUp]
        public void BeforeEachTest() {
            // TODO: If the iOS app being tested is included in the solution then open
            // the Unit Tests window, right click Test Apps, select Add App Project
            // and select the app projects that should be tested.
            app = ConfigureApp
                .iOS
                // TODO: Update this path to point to your iOS app and uncomment the
                // code if the app is not included in the solution.
                //.AppBundle ("../../../iOS/bin/iPhoneSimulator/Debug/iOSUITest.iOS.app")
                .StartApp();
        }

        [Test]
        public void AppLaunches() {
            app.Screenshot("First screen.");
        }

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
            var mgr = new iOS_LocationSvcMgr();

            var status = mgr.AuthorizationStatus;
            Assert.AreEqual(EnumLocationAuthorizationStates.NotDetermined, status);
        }

        [Test]
        public async Task AuthorizationCanBeGranted_UserApprovesAlways_ReturnsAuthorizedAlways() {

            var mgr = new iOS_LocationSvcMgr();

            await Task.Run(() => {
                mgr.RequestAlwaysAuthorization(); // User must accept for this test to pass
            });

            var status = mgr.AuthorizationStatus;
            Assert.AreEqual(EnumLocationAuthorizationStates.AuthorizedAlways, status);
        }

        [Test]
        public void AuthorizationIsMaintained_AlwaysAlreadyApproved_ReturnsAuthorizedAlways() {

            var mgr = new iOS_LocationSvcMgr();

            var status = mgr.AuthorizationStatus;
            Assert.AreEqual(EnumLocationAuthorizationStates.AuthorizedAlways, status);
        }

        [Test]
        public async Task AuthorizationCanBeChanged_UserApprovesWhenInUse_ReturnsAuthorizedWhenInUse() {

            var mgr = new iOS_LocationSvcMgr();

            await Task.Run(() => {
                mgr.RequestWhenInUseAuthorization(); // User must accept for this test to pass
            });

            var status = mgr.AuthorizationStatus;
            Assert.AreEqual(EnumLocationAuthorizationStates.AuthorizedWhenInUse, status);
        }
    }
}

