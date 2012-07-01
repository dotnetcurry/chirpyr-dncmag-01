using ChirpyR.Data.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ChirpyR.Domain.Repository;
using ChirpyR.Domain.Model;
using System.Collections.Generic;
using ChirpyR.Data.Repository;

namespace ChirpR.Data.Test
{
    
    
    /// <summary>
    ///This is a test class for ChirpyRDataServiceTest and is intended
    ///to contain all ChirpyRDataServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ChirpyRDataServiceTest
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
        
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()

        {
            
        }
        
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            Console.WriteLine("Cleanup" + TestContext.TestName);
        }
        
        #endregion


        /// <summary>
        ///A test for GetAllChirps
        ///</summary>
        [TestMethod()]
        public void GetAllChirpsTest()
        {
            IChirpyRRepository repository = new ChirpyRSqlRepository("ChirpyRConnection", "dbo"); 
            ChirpyRDataService target = new ChirpyRDataService(repository); 
            IList<Chirp> expected = null; // TODO: Initialize to an appropriate value
            IList<Chirp> actual;
            actual = target.GetAllChirps();
            Assert.AreNotEqual(expected, actual);
            
        }

        /// <summary>
        ///A test for AddChirp
        ///</summary>
        [TestMethod()]
        public void AddChirpTest()
        {
            IChirpyRRepository repository = new ChirpyRSqlRepository("ChirpyRConnection", "dbo"); 
            ChirpyRDataService target = new ChirpyRDataService(repository);
            Chirp newChirp = new Chirp
            {
                Text = "Test Chirp",
                ChirpTime = DateTime.Now,
            };
            long notExpected = -1; 
            long actual;
            actual = target.AddChirp(newChirp);
            Assert.AreNotEqual(notExpected, actual);
        }

        /// <summary>
        ///A test for RegisterUser
        ///</summary>
        [TestMethod()]
        public void RegisterUserTest()
        {
            IChirpyRRepository repository = new ChirpyRSqlRepository("ChirpyRConnection", "dbo"); 
            ChirpyRDataService target = new ChirpyRDataService(repository);
            ChirpyRUser chirpUser = new ChirpyRUser
            {
                UserId = "Test",
                FullName = "Test User",
                Email = "sumitkm@gmail.com"
            };
            long notExpected = 0; 
            long actual;
            actual = target.RegisterUser(chirpUser);
            Assert.AreNotEqual(notExpected, actual);
        }

        [TestMethod()]
        public void FollowUserTest()
        {
            IChirpyRRepository repoistory = new ChirpyRSqlRepository("ChirpyRConnection", "dbo");
            ChirpyRDataService target = new ChirpyRDataService(repoistory);
            ChirpyRUser user = repoistory.GetUserById("Test");
            ChirpyRUser follow = repoistory.GetUserById("Test1");
            target.FollowUser(user.UserId, follow.UserId);
        }

        [TestMethod]
        public void GetFollowingTest()
        {

        }

        [TestMethod()]
        public void GetFollowersTest()
        {

        }

        [TestMethod()]
        public void Register10Users()
        {
            IChirpyRRepository repository = new ChirpyRSqlRepository("ChirpyRConnection", "dbo");
            ChirpyRDataService target = new ChirpyRDataService(repository);
            int created = 0;
            int failed = 0;
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    ChirpyRUser chirpUser = new ChirpyRUser
                    {
                        UserId = "Test" + i.ToString(),
                        FullName = "Test User",
                        Email = "Test"+i.ToString() + "@gmail.com"
                    };
                    long actual;
                    actual = target.RegisterUser(chirpUser);
                    created++;
                }
                catch (ApplicationException ae)
                {
                    failed++;
                }
            }
            Assert.AreEqual(10, created);
        }
    }
}
