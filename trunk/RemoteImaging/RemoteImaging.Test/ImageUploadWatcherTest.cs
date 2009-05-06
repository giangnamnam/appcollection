using RemoteImaging.RealtimeDisplay;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace RemoteImaging.Test
{
    
    
    /// <summary>
    ///This is a test class for ImageUploadWatcherTest and is intended
    ///to contain all ImageUploadWatcherTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ImageUploadWatcherTest
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
        ///A test for ShouldFireEvent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("RemoteImaging.exe")]
        public void ShouldFireEventTest()
        {
            ImageUploadWatcher_Accessor target = new ImageUploadWatcher_Accessor(); // TODO: Initialize to an appropriate value
            ImageDetail img = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ShouldFireEvent(img);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MoveImages
        ///</summary>
        [TestMethod()]
        [DeploymentItem("RemoteImaging.exe")]
        public void MoveImagesTest()
        {
            ImageUploadWatcher_Accessor target = new ImageUploadWatcher_Accessor(); // TODO: Initialize to an appropriate value
            int cameraID = 0; // TODO: Initialize to an appropriate value
            ImageDetail[] expected = null; // TODO: Initialize to an appropriate value
            ImageDetail[] actual;
            actual = target.MoveImages(cameraID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }


        /// <summary>
        ///A test for FireEvent
        ///</summary>
        [TestMethod()]
        [DeploymentItem("RemoteImaging.exe")]
        public void FireEventTest()
        {
            ImageUploadWatcher_Accessor target = new ImageUploadWatcher_Accessor(); // TODO: Initialize to an appropriate value
            ImageDetail img = null; // TODO: Initialize to an appropriate value
            target.FireEvent(img);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
       
        
    }
}
