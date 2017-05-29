using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBox.Model;

namespace RBox.Data.Sql
{
    public class FilesRepository : IFilesRepository
    {
        private readonly string _connectionString;

        public FilesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public File Add(File file)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "insert into Files (FileId, Name, UserId, Description) values (@fileId, @name, @userId, @description)";
                    var fileId = Guid.NewGuid();
                    command.Parameters.AddWithValue("@fileId", fileId);
                    command.Parameters.AddWithValue("@name", file.Name);
                    command.Parameters.AddWithValue("@userId", file.UserId);
                    command.Parameters.AddWithValue("@description", file.Description);
                    command.ExecuteNonQuery();
                    file.FileId = fileId;
                    return file;
                }
            }
        }

        public byte[] GetContent(Guid fileId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select Content from files where FileId = @fileId";
                    command.Parameters.AddWithValue("@fileId", fileId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader.GetSqlBinary(reader.GetOrdinal("content")).Value;
                        }
                        throw new ArgumentException($"file {fileId} not found");
                    }
                }
            }
        }

        public File GetInfo(Guid fileId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select FileId, UserId, Name, Description from Files where FileId = @fileId";
                    command.Parameters.AddWithValue("@fileId", fileId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new File
                            {
                                FileId = reader.GetGuid(reader.GetOrdinal("fileId")),
                                UserId = reader.GetGuid(reader.GetOrdinal("userId")),
                                Description = reader.GetString(reader.GetOrdinal("description")),
                                Name = reader.GetString(reader.GetOrdinal("name"))
                            };
                        }
                        throw new ArgumentException($"file {fileId} not found");
                    }
                }
            }
        }

        public void UpdateContent(Guid fileId, byte[] content)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "update files set content = @content where FileId = @fileId";
                    command.Parameters.AddWithValue("@fileId", fileId);
                    command.Parameters.AddWithValue("@content", content);
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<File> GetFilesByUserId(Guid userId)
        {
            var result = new List<File>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select FileId from files where userId = @userId";
                    command.Parameters.AddWithValue("@userId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(GetInfo(reader.GetGuid(reader.GetOrdinal("FileId"))));
                        }
                        return result;
                    }
                }
            }
        }

        public void Delete(Guid fileId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "delete from files where FileId = @fileId";
                    command.Parameters.AddWithValue("@fileId", fileId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
