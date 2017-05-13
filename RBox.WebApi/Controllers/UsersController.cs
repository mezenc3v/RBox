using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RBox.Data;
using RBox.Data.Sql;
using RBox.Model;

namespace RBox.WebApi.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IFilesRepository _filesRepository;

        public UsersController(string connectionString)
        {
            _usersRepository = new UsersRepository(connectionString);
            _filesRepository = new FilesRepository(connectionString);
        }

        public UsersController() : this(ConfigurationManager.ConnectionStrings["RBoxDb"].ConnectionString)
        {

        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user">the user you want to create</param>
        /// <returns>created user</returns>
        [HttpPost]
        [Route("")]
        public User CreateUser([FromBody]User user)
        {
            return _usersRepository.AddUser(user);
        }

        /// <summary>
        /// get user
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public User GetUser(Guid id)
        {
            return _usersRepository.GetUser(id);
        }

        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="id">user id</param>
        [HttpDelete]
        [Route("{id}")]
        public void DeleteUser(Guid id)
        {
            _usersRepository.DeleteUser(id);
        }

        /// <summary>
        /// get user files
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>collection of files</returns>
        [HttpGet]
        [Route("{id}/files")]
        public IEnumerable<File> GetUserFiles(Guid id)
        {
            return _filesRepository.GetFilesByUserId(id);
        }

        /// <summary>
        /// update user info
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="user"></param>
        [HttpPut]
        [Route("{id}")]
        public void UpdateUser(Guid id, [FromBody]User user)
        {
            _usersRepository.UpdateUser(id, user);
        }
    }
}
