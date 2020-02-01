using System.Data.SqlClient;
using DapperUtilities.Managers;
using DapperUtilities.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DapperUtilities.Tests
{
    [TestClass]
    public class TransactionManagerTests : TransactionManager
    {
        [TestMethod]
        [ExpectedException(typeof(SqlException),
             "Error has been encountered in the database.")]
        public void InsertUpdateTests()
        {
            AddParameter("ClientName", "BISTRO");
            AddParameter("Address", "Perea, Makati City");
            var result = InsertOrUpdateWithOutput("usp_InsertUpdateClient", "Message");

        }

        [TestMethod]
        [ExpectedException(typeof(SqlException), 
            "Error has been encountered in the database.")]
        public void GetListTests()
        {
            var result = GetList<Client>("SELECT * FROM Client Where ClientID = 4");
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        private TestContext testContextInstance;

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
    }
}
