using System;
using System.ComponentModel;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RBox.Model;

namespace RBox.Data.Sql.Tests
{
    [TestClass]
    public class UsersRepositoryTests
    {
        private readonly string ConnectionString;
        private readonly IUsersRepository _usersRepository;

        private User TestUser { get; set; }

        public UsersRepositoryTests(string connectionString)
        {
            ConnectionString = connectionString;
            _usersRepository = new UsersRepository(ConnectionString);
        }

        public UsersRepositoryTests() : this(ConfigurationManager.ConnectionStrings["RBoxDb"].ConnectionString)
        {

        }

        [TestInitialize]
        public void Init()
        {
            var name = "testFile name";
            var userLogin = "testLogin@example.com";
            var password = "password";
            var user = new User
            {
                Name = name,
                UserLogin = userLogin,
                Password = password
            };

            TestUser = _usersRepository.AddUser(user);
        }

        [TestCleanup]
        public void Clean()
        {
            _usersRepository.DeleteUser(TestUser.UserId);
        }

        [TestMethod]
        public void ShouldGetUser()
        {
            //arrange
            var user = TestUser;
            //act
            var newUser = _usersRepository.GetUser(user.UserId);
            //asserts
            Assert.AreEqual(user.Name, newUser.Name);
            Assert.AreEqual(user.Password, newUser.Password);
            Assert.AreEqual(user.UserLogin, newUser.UserLogin);
        }

        [TestMethod]
        public void ShouldUpdateUser()
        {
            //arrange
            var originalUser = _usersRepository.GetUser(TestUser.UserId);
            var modifiedUser = _usersRepository.GetUser(TestUser.UserId);
            //act
            modifiedUser.Password = "NewPassword";
            modifiedUser.Name = "NewName";
            _usersRepository.UpdateUser(originalUser.UserId, modifiedUser);
            modifiedUser = _usersRepository.GetUser(originalUser.UserId);
            //asserts
            Assert.AreEqual(modifiedUser.UserId, originalUser.UserId);
            Assert.AreEqual(modifiedUser.UserLogin, originalUser.UserLogin);
            Assert.AreNotEqual(modifiedUser.Name, originalUser.Name);
            Assert.AreNotEqual(modifiedUser.Password, originalUser.Password);
        }
    }
}
