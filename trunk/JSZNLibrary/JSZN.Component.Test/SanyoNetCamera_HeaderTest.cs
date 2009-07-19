using JSZN.Component;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JSZN.Net;

namespace JSZN.Component.Test
{


    /// <summary>
    ///This is a test class for SanyoNetCamera_HeaderTest and is intended
    ///to contain all SanyoNetCamera_HeaderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SanyoNetCamera_HeaderTest
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
        ///A test for Ver
        ///</summary>
        [TestMethod()]
        [DeploymentItem("JSZN.Component.dll")]
        public void VerTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            SanyoNetCamera_Accessor.Header target = new SanyoNetCamera_Accessor.Header(param0); // TODO: Initialize to an appropriate value
            ushort expected = 0; // TODO: Initialize to an appropriate value
            ushort actual;
            target.Ver = expected;
            actual = target.Ver;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for TotalNumOfPackets
        ///</summary>
        [TestMethod()]
        [DeploymentItem("JSZN.Component.dll")]
        public void TotalNumOfPacketsTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            SanyoNetCamera_Accessor.Header target = new SanyoNetCamera_Accessor.Header(param0); // TODO: Initialize to an appropriate value
            ushort expected = 0; // TODO: Initialize to an appropriate value
            ushort actual;
            target.TotalNumOfPackets = expected;
            actual = target.TotalNumOfPackets;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SeqNo
        ///</summary>
        [TestMethod()]
        [DeploymentItem("JSZN.Component.dll")]
        public void SeqNoTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            SanyoNetCamera_Accessor.Header target = new SanyoNetCamera_Accessor.Header(param0); // TODO: Initialize to an appropriate value
            ushort expected = 0; // TODO: Initialize to an appropriate value
            ushort actual;
            target.SeqNo = expected;
            actual = target.SeqNo;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PackNo
        ///</summary>
        [TestMethod()]
        [DeploymentItem("JSZN.Component.dll")]
        public void PackNoTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            SanyoNetCamera_Accessor.Header target = new SanyoNetCamera_Accessor.Header(param0); // TODO: Initialize to an appropriate value
            ushort expected = 0; // TODO: Initialize to an appropriate value
            ushort actual;
            target.PackNo = expected;
            actual = target.PackNo;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Mac
        ///</summary>
        [TestMethod()]
        [DeploymentItem("JSZN.Component.dll")]
        public void MacTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            SanyoNetCamera_Accessor.Header target = new SanyoNetCamera_Accessor.Header(param0); // TODO: Initialize to an appropriate value
            MAC expected = null; // TODO: Initialize to an appropriate value
            MAC actual;
            target.Mac = expected;
            actual = target.Mac;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Cmd
        ///</summary>
        [TestMethod()]
        [DeploymentItem("JSZN.Component.dll")]
        public void CmdTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            SanyoNetCamera_Accessor.Header target = new SanyoNetCamera_Accessor.Header(param0); // TODO: Initialize to an appropriate value
            SanyoNetCamera_Accessor.Command expected = null; // TODO: Initialize to an appropriate value
            SanyoNetCamera_Accessor.Command actual;
            target.Cmd = expected;
            actual = target.Cmd;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SetShort
        ///</summary>
        [TestMethod()]
        [DeploymentItem("JSZN.Component.dll")]
        public void SetShortTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            SanyoNetCamera_Accessor.Header target = new SanyoNetCamera_Accessor.Header(param0); // TODO: Initialize to an appropriate value
            ushort value = 0; // TODO: Initialize to an appropriate value
            int index = 0; // TODO: Initialize to an appropriate value
            target.SetShort(value, index);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod()]
        [DeploymentItem("JSZN.Component.dll")]
        public void ParseTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            SanyoNetCamera_Accessor.Header target = new SanyoNetCamera_Accessor.Header(param0); // TODO: Initialize to an appropriate value
            byte[] buffer = null; // TODO: Initialize to an appropriate value
            int startIndex = 0; // TODO: Initialize to an appropriate value
            target.Parse(buffer, startIndex);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetUshort
        ///</summary>
        [TestMethod()]
        [DeploymentItem("JSZN.Component.dll")]
        public void GetUshortTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            SanyoNetCamera_Accessor.Header target = new SanyoNetCamera_Accessor.Header(param0); // TODO: Initialize to an appropriate value
            int index = 0; // TODO: Initialize to an appropriate value
            ushort expected = 0; // TODO: Initialize to an appropriate value
            ushort actual;
            actual = target.GetUshort(index);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetBytes
        ///</summary>
        [TestMethod()]
        [DeploymentItem("JSZN.Component.dll")]
        public void GetBytesTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            SanyoNetCamera_Accessor.Header target = new SanyoNetCamera_Accessor.Header(param0); // TODO: Initialize to an appropriate value
            byte[] expected = null; // TODO: Initialize to an appropriate value
            byte[] actual;
            actual = target.GetBytes();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Header Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("JSZN.Component.dll")]
        public void SanyoNetCamera_HeaderConstructorTest()
        {
            SanyoNetCamera_Accessor.Command cmd = null; // TODO: Initialize to an appropriate value
            MAC destMAC = null; // TODO: Initialize to an appropriate value
            SanyoNetCamera_Accessor.Header target = new SanyoNetCamera_Accessor.Header(cmd, destMAC);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
