using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using RBox.Data;
using RBox.Data.Sql;
using RBox.Model;

namespace RBox.WebApi.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly ISharesRepository _sharesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IFilesRepository _filesRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public UsersController(string connectionString)
        {
            _sharesRepository = new SharesRepository(connectionString);
            _usersRepository = new UsersRepository(connectionString);
            _filesRepository = new FilesRepository(connectionString);
        }
        /// <summary>
        /// Different constructor
        /// </summary>
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
                Log.Logger.ServiceLog.Error(ex.Message);
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
        /// find user
        /// </summary>
        /// <param name="user">user search</param>
        /// <returns>found user</returns>
        [HttpPost]
        [Route("user")]
        public User FindUser([FromBody] User user)
        {
            try
            {
                var foundUser = _usersRepository.FindUser(user);
                return foundUser;
            }
            catch (Exception ex)
            {
                Log.Logger.ServiceLog.Error(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// login user
        /// </summary>
        /// <param name="loginUser">user</param>
        /// <returns>logged user</returns>
        [HttpPost]
        [Route("login")]
        public User LoginUser([FromBody] User loginUser)
        {
            try
            {
                var user = new User
                {
                    UserLogin = loginUser.UserLogin,
                    Password = loginUser.Password
                };
                var foundUser = _usersRepository.LoginUser(user);
                return foundUser;
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
        /// get shared files
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>collection of files</returns>
        [HttpGet]
        [Route("{id}/shares")]
        public IEnumerable<File> GetUserSharedFiles(Guid id)
        {
            try
            {
                var shares = _sharesRepository.GetUserShares(id);
                return shares.Select(share => _filesRepository.GetInfo(share.FileId));
            }
            catch (Exception ex)
            {
                Log.Logger.ServiceLog.Error(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Add new share
        /// </summary>
        /// <param name="share">the share you want to create</param>
        /// <returns></returns>
        [HttpPost]
        [Route("share")]
        public Share AddShare([FromBody]Share share)
        {
            try
            {
                var newShare = _sharesRepository.AddShare(share);
                Log.Logger.ServiceLog.Info("Create share with id: {0}", share.UserId);
                return newShare;
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
