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
            try
            {
                var newUser = _usersRepository.AddUser(user);
                Log.Logger.ServiceLog.Info("Create user with id: {0}", newUser.UserId);
                return newUser;
            }
            catch (Exception ex)
            {
                Log.Logger.ServiceLog.Fatal(ex.Message);
                throw;
            }
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
            try
            {
                var user = _usersRepository.GetUser(id);
                return user;
            }
            catch (Exception ex)
            {
                Log.Logger.ServiceLog.Error(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="id">user id</param>
        [HttpDelete]
        [Route("{id}")]
        public void DeleteUser(Guid id)
        {
            try
            {
                Log.Logger.ServiceLog.Info("Delete user with id: {0}", id);
                _usersRepository.DeleteUser(id);
            }
            catch (Exception ex)
            {
                Log.Logger.ServiceLog.Error(ex.Message);
                throw;
            }
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
            try
            {
                return _filesRepository.GetFilesByUserId(id);
            }
            catch (Exception ex)
            {
                Log.Logger.ServiceLog.Error(ex.Message);
                throw;
            }
            
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
            try
            {
                _usersRepository.UpdateUser(id, user);
            }
            catch (Exception ex)
            {
                Log.Logger.ServiceLog.Error(ex.Message);
                throw;
            }
        }
    }
}
