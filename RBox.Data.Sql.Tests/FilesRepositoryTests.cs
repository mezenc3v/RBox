using System;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RBox.Data;
using RBox.Model;

namespace RBox.Data.Sql.Tests
{
    [TestClass]
    public class FilesRepositoryTests
    {
        private readonly string ConnectionString;
        private readonly IUsersRepository _usersRepository;
        private readonly IFilesRepository _filesRepository;

        private User TestUser { get; set; }

        public FilesRepositoryTests(string connectionString)
        {
            ConnectionString = connectionString;
            _usersRepository = new UsersRepository(ConnectionString);
            _filesRepository = new FilesRepository(ConnectionString);
        }

        public FilesRepositoryTests() : this(ConfigurationManager.ConnectionStrings["RBoxDb"].ConnectionString)
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
        }

        [TestCleanup]
        public void Clean()
        {
            if (TestUser != null)
            {
                foreach (var file in _filesRepository.GetFilesByUserId(TestUser.UserId))
                {
                    _filesRepository.Delete(file.FileId);
                    _usersRepository.DeleteUser(TestUser.UserId);
                }
            }
        }

        [TestMethod]
        public void ShouldCreateFile()
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
