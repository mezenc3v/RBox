using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RBox.Model;

namespace RBox.DataAccess.Sql.Tests
{
    [TestClass]
    public class SharesRepositoryTests
    {
        private const string ConnectionString =
    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RboxDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly IUsersRepository _usersRepository = new UsersRepository(ConnectionString);
        private readonly IFilesRepository _filesRepository;
        private readonly ISharesRepository _sharesRepository;

        private User TestUser { get; set; }
        private File TestFile { get; set; }

        public SharesRepositoryTests()
        {
            _filesRepository = new FilesRepository(ConnectionString, _usersRepository);
            _sharesRepository = new SharesRepository(ConnectionString);
        }
        [TestInitialize]
        public void Init()
        {
            TestUser = _usersRepository.Add("testUser", "testLogin", "TestPass");

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
                    _sharesRepository.Delete(share.ShareId);
                }
                foreach (var file in _filesRepository.GetUserFiles(TestUser.UserId))
                {
                    _filesRepository.Delete(file.FileId);
                    _usersRepository.Delete(TestUser.UserId);
                }
            }
        }

        [TestMethod]
        public void ShouldCreateAndGetShare()
        {
            //arrange
            var share = new Share
            {
                FileId = TestFile.FileId,
                UserId = TestUser.UserId
            };
            //act
            var testShare = _sharesRepository.Add(share);
            //asserts
            Assert.AreEqual(testShare.UserId, TestUser.UserId);
            Assert.AreEqual(testShare.FileId, TestFile.FileId);
        }
    }
}
