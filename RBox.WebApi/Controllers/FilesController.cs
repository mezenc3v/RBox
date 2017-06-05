using RBox.Data;
using RBox.Data.Sql;
using RBox.Model;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;

namespace RBox.WebApi.Controllers
{
    /// <summary>
    /// Files Controller
    /// </summary>
    [RoutePrefix("api/files")]
    public class FilesController : ApiController
    {
        private readonly IFilesRepository _filesRepository;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="connectionString"></param>
        public FilesController(string connectionString)
        {
            _filesRepository = new FilesRepository(connectionString);
        }
        /// <summary>
        /// different constructor
        /// </summary>
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
            try
            {
                return _filesRepository.GetInfo(id);
            }
            catch (Exception ex)
            {
                Log.Logger.ServiceLog.Error(ex.Message);
                throw;
            }

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
            try
            {
                return _filesRepository.GetContent(id);
            }
            catch (Exception ex)
            {
                Log.Logger.ServiceLog.Error(ex.Message);
                throw;
            }
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
            try
            {
                return _filesRepository.Add(file);
            }
            catch (Exception ex)
            {
                Log.Logger.ServiceLog.Error(ex.Message);
                throw;
            }
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
            try
            {
                var bytes = await Request.Content.ReadAsByteArrayAsync();
                _filesRepository.UpdateContent(id, bytes);
            }
            catch (Exception ex)
            {
                Log.Logger.ServiceLog.Error(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// delete file
        /// </summary>
        /// <param name="id">file id</param>
        [HttpDelete]
        [Route("{id}")]
        public void Delete(Guid id)
        {
            _filesRepository.Delete(id);
        }
    }
}
