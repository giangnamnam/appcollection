using RemoteImaging.RealtimeDisplay;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace RemoteImaging.Test
{
    
    
    /// <summary>
    ///This is a test class for ImageClassifierTest and is intended
    ///to contain all ImageClassifierTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ImageClassifierTest
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
        ///A test for ClassifyImages
        ///</summary>
        [TestMethod()]
        public void ClassifyImagesTest()
        {
            ImageClassifier target = new ImageClassifier(); // TODO: Initialize to an appropriate value
            ImageUploadEventArgs[] images = null; // TODO: Initialize to an appropriate value
            target.ClassifyImages(images);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ImageClassifier Constructor
        ///</summary>
        [TestMethod()]
        public void ImageClassifierConstructorTest()
        {
            ImageClassifier target = new ImageClassifier();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
