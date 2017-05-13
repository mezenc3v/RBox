using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RBox.Model;
using System.Configuration;

namespace RBox.Data.Sql.Tests
{
    [TestClass]
    public class CommentsRepositoryTests
    {
        private readonly string ConnectionString;

        private readonly IUsersRepository _usersRepository;
        private readonly IFilesRepository _filesRepository;
        private readonly ICommentsRepository _commentsRepository;

        private User TestUser { get; set; }
        private File TestFile { get; set; }

        public CommentsRepositoryTests(string connectionString)
        {
            ConnectionString = connectionString;
            _usersRepository = new UsersRepository(ConnectionString);
            _filesRepository = new FilesRepository(ConnectionString);
            _commentsRepository = new CommentsRepository(ConnectionString);
        }

        public CommentsRepositoryTests() : this(ConfigurationManager.ConnectionStrings["RBoxDb"].ConnectionString)
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
                foreach (var comment in _commentsRepository.GetComments(TestFile.FileId))
                {
                    _commentsRepository.DeleteComment(comment.CommentId);
                }
                foreach (var file in _filesRepository.GetFilesByUserId(TestUser.UserId))
                {
                    _filesRepository.Delete(file.FileId);
                    _usersRepository.DeleteUser(TestUser.UserId);
                }
            }
        }

        [TestMethod]
        public void ShouldCreateComment()
        {
            //arrange
            var comment = new Comment
            {
                FileId = TestFile.FileId,
                UserId = TestUser.UserId,
                Text = "comment text"
            };
            //act
            var testComment = _commentsRepository.AddComment(comment);
            //asserts
            Assert.AreEqual(testComment.UserId, TestUser.UserId);
            Assert.AreEqual(testComment.FileId, TestFile.FileId);
            Assert.AreEqual(testComment.Text, comment.Text);
        }
    }
}
