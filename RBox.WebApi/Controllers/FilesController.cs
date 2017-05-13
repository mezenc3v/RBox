using RBox.Data;
using RBox.Data.Sql;
using RBox.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;

namespace RBox.WebApi.Controllers
{
    [RoutePrefix("api/files")]
    public class FilesController : ApiController
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IFilesRepository _filesRepository;
        private readonly ISharesRepository _sharesRepository;
        private readonly ICommentsRepository _commentsRepository;

        public FilesController(string connectionString)
        {
            _usersRepository = new UsersRepository(connectionString);
            _filesRepository = new FilesRepository(connectionString);
            _sharesRepository = new SharesRepository(connectionString);
            _commentsRepository = new CommentsRepository(connectionString);
        }

        public FilesController() : this(ConfigurationManager.ConnectionStrings["RBoxDb"].ConnectionString)
        {

        }
        /// <summary>
        /// get file info
        /// </summary>
        /// <param name="id">file id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public File GetFileInfo(Guid id)
        {
            return _filesRepository.GetInfo(id);
        }

        /// <summary>
        /// get file content
        /// </summary>
        /// <param name="id">file id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/content")]
        public byte[] GetFileContent(Guid id)
        {
            return _filesRepository.GetContent(id);
        }

        /// <summary>
        /// create file
        /// </summary>
        /// <param name="file">the file you want to create </param>
        /// <returns>created file</returns>
        [HttpPost]
        [Route("")]
        public File CreateFile(File file)
        {
            return _filesRepository.Add(file);
        }

        /// <summary>
        /// update file content
        /// </summary>
        /// <param name="id">file id</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/content")]
        public async Task UpdateFileContent(Guid id)
        {
            var bytes = await Request.Content.ReadAsByteArrayAsync();
            _filesRepository.UpdateContent(id, bytes);
        }

        /// <summary>
        /// get file comment
        /// </summary>
        /// <param name="id">file id</param>
        /// <returns>collection of comments</returns>
        [HttpGet]
        [Route("{id}/comments")]
        public IEnumerable<Comment> GetComments([FromUri] Guid id)
        {
            return _commentsRepository.GetComments(id);
        }

        /// <summary>
        /// create comment
        /// </summary>
        /// <param name="comment">the comment you want to create</param>
        /// <returns>created comment</returns>
        [HttpPost]
        [Route("{id}/comments")]
        public Comment CreateComment(Comment comment)
        {
            return _commentsRepository.AddComment(comment);
        }

        /// <summary>
        /// update comment
        /// </summary>
        /// <param name="commentId">comment id</param>
        /// <param name="comment"></param>
        [HttpPut]
        [Route("{id}/comments/{commentId}")]
        public void UpdateComment(Guid commentId, Comment comment)
        {
            _commentsRepository.UpdateComment(commentId, comment);
        }
    }
}
