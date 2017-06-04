using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using RBox.Model;

namespace RBox.WinForms
{
    public class ServiceClient
    {
        public Guid UserId => _currUserId;

        private const string ConnectionString = "http://localhost:52908/";
        private static Guid _currUserId;
        private static HttpClient _client;

        public ServiceClient()
        {
            _client = new HttpClient { BaseAddress = new Uri(ConnectionString) };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public User CreateUser(User user)
        {
            var response = _client.PostAsJsonAsync("api/users", user).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<User>().Result;
                ////////////////////////////
                _currUserId = result.UserId;
                /////////////////////////////
                return result;
            }
            throw new ServiceException("Error: {0}", response.StatusCode);
        }

        public User LoginUser(User user)
        {
            var response = _client.PostAsJsonAsync("api/users/login", user).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<User>().Result;
                ////////////////////////////
                _currUserId = result.UserId;
                /////////////////////////////
                return result;
            }
            throw new ServiceException("Error: {0}", response.StatusCode);
        }

        public void CloseUser()
        {
            _currUserId = new Guid();
            _client?.Dispose();
            _client = null;
        }

        public File[] GetUserFiles()
        {
            if (_client == null) return null;
            var response = _client.GetAsync($"api/users/{_currUserId}/files").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<File[]>().Result;
                return result;
            }
            throw new ServiceException("Error: {0}", response.StatusCode);
        }

        public Guid CreateFile(File file)
        {
            var response = _client.PostAsJsonAsync("api/files", file).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<File>().Result;
                return result.FileId;
            }
            throw new ServiceException("Error: {0}", response.StatusCode);
        }

        public void UploadFileContent(Guid fileId, byte[] content)
        {
            using (var byteArrayContent = new ByteArrayContent(content))
            {
                var response = _client.PutAsync($"api/files/{fileId}/content", byteArrayContent).Result;
                if (!response.IsSuccessStatusCode)
                    throw new ServiceException("Error: {0}", response.StatusCode);
            }
        }

        public void DeleteFile(Guid fileId)
        {
            var response = _client.DeleteAsync($"api/files/{fileId}").Result;
            if (!response.IsSuccessStatusCode)
                throw new ServiceException("Error: {0}", response.StatusCode);
        }

        public byte[] DownloadFile(Guid fileId)
        {
            var response = _client.GetAsync($"api/files/{fileId}/content").Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<byte[]>().Result;
            throw new ServiceException("Error: {0}", response.StatusCode);
        }

        public void CreateShare(Share share)
        {
            var response = _client.PostAsJsonAsync("api/users/share", share).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new ServiceException("Error: {0}", response.StatusCode);
            }
        }

        public File[] GetSharedFiles()
        {
            if (_client == null) return null;
            var response = _client.GetAsync($"api/users/{_currUserId}/shares").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<File[]>().Result;
                return result;
            }
            throw new ServiceException("Error: {0}", response.StatusCode);
        }

        public User FindUser(User user)
        {
            var response = _client.PostAsJsonAsync("api/users/user", user).Result;
            if (response.IsSuccessStatusCode)
                return response.Content.ReadAsAsync<User>().Result;
            throw new ServiceException("Error: {0}", response.StatusCode);
        }
    }
}
