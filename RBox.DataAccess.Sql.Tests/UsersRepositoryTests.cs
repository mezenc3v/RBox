using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RBox.Model;

namespace RBox.DataAccess.Sql.Tests
{
    [TestClass]
    public class UsersRepositoryTests
    {
        private const string ConnectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RboxDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly IUsersRepository _usersRepository;

        private User TestUser { get; set; }

        public UsersRepositoryTests()
        {
            _usersRepository = new UsersRepository(ConnectionString);
        }

        [TestInitialize]
        public void Init()
        {
            string name = "testFile name";
            string userLogin = "testLogin@example.com";
            string password = "password";

            TestUser = _usersRepository.Add(name, userLogin, password);
        }

        [TestCleanup]
        public void Clean()
        {
            _usersRepository.Delete(TestUser.UserId);
        }

        [TestMethod]
        public void ShouldGetUser()
        {
            //arrange
            var user = TestUser;
            //act
            var newUser = _usersRepository.Get(user.UserId);
            //asserts
            Assert.AreEqual(user.Name, newUser.Name);
            Assert.AreEqual(user.Password, newUser.Password);
            Assert.AreEqual(user.UserLogin, newUser.UserLogin);
        }
    }
}
