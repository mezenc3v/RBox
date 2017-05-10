using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RBox.Model;

namespace RBox.DataAccess.Sql.Tests
{
    [TestClass]
    public class CommentRepositoryTests
    {
        private const string ConnectionString =
    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RboxDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly IUsersRepository _usersRepository = new UsersRepository(ConnectionString);
        private readonly IFilesRepository _filesRepository;
        private readonly ICommentsRepository _commentsRepository;

        private User TestUser { get; set; }
        private File TestFile { get; set; }

        public CommentRepositoryTests()
        {
            _filesRepository = new FilesRepository(ConnectionString, _usersRepository);
            _commentsRepository = new CommentsRepository(ConnectionString);
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
                foreach (var comment in _commentsRepository.GetComments(TestFile.FileId))
                {
                    _commentsRepository.Delete(comment.CommentId);
                }
                foreach (var file in _filesRepository.GetUserFiles(TestUser.UserId))
                {
                    _filesRepository.Delete(file.FileId);
                    _usersRepository.Delete(TestUser.UserId);
                }
            }
        }

        [TestMethod]
        public void ShouldCreateAndGetComment()
        {
            //arrange
            var comment = new Comment
            {
                FileId = TestFile.FileId,
                UserId = TestUser.UserId,
                Text = "comment text"
            };
            //act
            var testComment = _commentsRepository.Add(comment);
            //asserts
            Assert.AreEqual(testComment.UserId, TestUser.UserId);
            Assert.AreEqual(testComment.FileId, TestFile.FileId);
            Assert.AreEqual(testComment.Text, comment.Text);
        }
    }
}
