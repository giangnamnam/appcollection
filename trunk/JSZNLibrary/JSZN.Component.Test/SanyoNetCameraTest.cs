using JSZN.Component;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Net.Sockets;

namespace JSZN.Component.Test
{


    /// <summary>
    ///This is a test class for SanyoNetCameraTest and is intended
    ///to contain all SanyoNetCameraTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SanyoNetCameraTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for UserName
        ///</summary>
        [TestMethod()]
        public void UserNameTest()
        {
            SanyoNetCamera target = new SanyoNetCamera(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.UserName = expected;
            actual = target.UserName;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Password
        ///</summary>
        [TestMethod()]
        public void PasswordTest()
        {
            SanyoNetCamera target = new SanyoNetCamera(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Password = expected;
            actual = target.Password;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IPAddress
        ///</summary>
        [TestMethod()]
        public void IPAddressTest()
        {
            SanyoNetCamera target = new SanyoNetCamera(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.IPAddress = expected;
            actual = target.IPAddress;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }



        /// <summary>
        ///A test for SearchCamersAsync
        ///</summary>
        [TestMethod()]
        public void SearchCamersAsyncTest()
        {
            SanyoNetCamera.SearchCamersAsync();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for InitializeComponent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("JSZN.Component.dll")]
        public void InitializeComponentTest()
        {
            SanyoNetCamera_Accessor target = new SanyoNetCamera_Accessor(); // TODO: Initialize to an appropriate value
            target.InitializeComponent();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetImage
        ///</summary>
        [TestMethod()]
        public void GetImageTest()
        {
            SanyoNetCamera target = new SanyoNetCamera(); // TODO: Initialize to an appropriate value
            byte[] expected = null; // TODO: Initialize to an appropriate value
            byte[] actual;
            actual = target.GetImage();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Dispose
        ///</summary>
        [TestMethod()]
        [DeploymentItem("JSZN.Component.dll")]
        public void DisposeTest()
        {
            SanyoNetCamera_Accessor target = new SanyoNetCamera_Accessor(); // TODO: Initialize to an appropriate value
            bool disposing = false; // TODO: Initialize to an appropriate value
            target.Dispose(disposing);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Connect
        ///</summary>
        [TestMethod()]
        public void ConnectTest()
        {
            SanyoNetCamera target = new SanyoNetCamera(); // TODO: Initialize to an appropriate value
            target.Connect();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SanyoNetCamera Constructor
        ///</summary>
        [TestMethod()]
        public void SanyoNetCameraConstructorTest1()
        {
            IContainer container = null; // TODO: Initialize to an appropriate value
            SanyoNetCamera target = new SanyoNetCamera(container);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for SanyoNetCamera Constructor
        ///</summary>
        [TestMethod()]
        public void SanyoNetCameraConstructorTest()
        {
            SanyoNetCamera target = new SanyoNetCamera();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
