using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RBox.DataAccess;
using RBox.Model;

namespace RBox.DataAccess.Sql.Tests
{
    [TestClass]
    public class FilesRepositoryTests
    {
        private const string ConnectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RboxDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly IUsersRepository _usersRepository = new UsersRepository(ConnectionString);
        private readonly IFilesRepository _filesRepository;

        private User TestUser { get; set; }

        public FilesRepositoryTests()
        {
            _filesRepository = new FilesRepository(ConnectionString, _usersRepository);
        }

        [TestInitialize]
        public void Init()
        {
            TestUser = _usersRepository.Add("testUser", "testLogin", "TestPass");
        }

        [TestCleanup]
        public void Clean()
        {
            if (TestUser != null)
            {
                foreach (var file in _filesRepository.GetUserFiles(TestUser.UserId))
                {
                    _filesRepository.Delete(file.FileId);
                    _usersRepository.Delete(TestUser.UserId);
                }
            }
        }

        [TestMethod]
        public void ShouldCreateAndGetFile()
        {
            //arrange
            var file = new File
            {
                Description = "testFile description",
                Name = "testFile name",
                UserId = TestUser.UserId
            };
            //act
            var newFile = _filesRepository.Add(file);
            var result = _filesRepository.GetInfo(newFile.FileId);
            //asserts
            Assert.AreEqual(file.UserId, result.UserId);
            Assert.AreEqual(file.Name, result.Name);
        }

        [TestMethod]
        public void ShouldUpdateFileContent()
        {
            //arrange
            var file = new File
            {
                Description = "testFile description",
                Name = "testFile name",
                UserId = TestUser.UserId
            };
            var content = Encoding.UTF8.GetBytes("file content");
            var newFile = _filesRepository.Add(file);
            //act
            _filesRepository.UpdateContent(newFile.FileId, content);
            var resultContent = _filesRepository.GetContent(newFile.FileId);
            //asserts
            Assert.IsTrue(content.SequenceEqual(resultContent));
        }
    }
}
