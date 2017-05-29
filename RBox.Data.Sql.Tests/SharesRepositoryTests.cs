using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RBox.Model;

namespace RBox.Data.Sql.Tests
{
    [TestClass]
    public class SharesRepositoryTests
    {
        private readonly string ConnectionString;
        private readonly IUsersRepository _usersRepository;
        private readonly IFilesRepository _filesRepository;
        private readonly ISharesRepository _sharesRepository;

        private User TestUser { get; set; }
        private File TestFile { get; set; }

        public SharesRepositoryTests(string connectionString)
        {
            ConnectionString = connectionString;
            _usersRepository = new UsersRepository(ConnectionString);
            _filesRepository = new FilesRepository(ConnectionString);
            _sharesRepository = new SharesRepository(ConnectionString);
        }

        public SharesRepositoryTests() : this(ConfigurationManager.ConnectionStrings["RBoxDb"].ConnectionString)
        {

        }
        [TestInitialize]
        public void Init()
        {
            var user = new User
            {
                Name = "testUser",
                UserLogin = "testLogin",
                Password = "TestPass"
            };

            TestUser = _usersRepository.AddUser(user);

            TestFile = new File
            {
                Description = "testFile description",
                Name = "testFile name",
                UserId = TestUser.UserId
            };

            TestFile = _filesRepository.Add(TestFile);
        }

        [TestCleanup]
        public void Clean()
        {
            if (TestUser != null)
            {
                foreach (var share in _sharesRepository.GetUserShares(TestUser.UserId))
                {
                    _sharesRepository.DeleteShare(share.ShareId);
                }
                foreach (var file in _filesRepository.GetFilesByUserId(TestUser.UserId))
                {
                    _filesRepository.Delete(file.FileId);
                    _usersRepository.DeleteUser(TestUser.UserId);
                }
            }
        }

        [TestMethod]
        public void ShouldCreateShare()
        {
            //arrange
            var share = new Share
            {
                FileId = TestFile.FileId,
                UserId = TestUser.UserId
            };
            //act
            var testShare = _sharesRepository.AddShare(share);
            //asserts
            Assert.AreEqual(testShare.UserId, TestUser.UserId);
            Assert.AreEqual(testShare.FileId, TestFile.FileId);
        }
    }
}
