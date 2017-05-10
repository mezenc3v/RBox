using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBox.Model;

namespace RBox.DataAccess.Sql
{
    public class SharesRepository : ISharesRepository
    {
        private readonly string _connectionString;

        public SharesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Share GetShare(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select ShareId, FileId, UserId from Shares where ShareId = @id";
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Share
                            {
                                ShareId = reader.GetGuid(reader.GetOrdinal("ShareId")),
                                FileId = reader.GetGuid(reader.GetOrdinal("FileId")),
                                UserId = reader.GetGuid(reader.GetOrdinal("UserId"))
                            };
                        }
                        throw new ArgumentException($"share {id} not found");
                    }
                }
            }
        }

        public Share Add(Share share)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "insert into Shares (ShareId, UserId, FileId) values (@ShareId, @UserId, @FileId)";
                    var shareId = Guid.NewGuid();
                    command.Parameters.AddWithValue("@ShareId", shareId);
                    command.Parameters.AddWithValue("@UserId", share.UserId);
                    command.Parameters.AddWithValue("@FileId", share.FileId);
                    command.ExecuteNonQuery();
                    share.ShareId = shareId;
                    return share;
                }
            }
        }

        public void Delete(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "delete from shares where ShareId = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Share> GetUserShares(Guid userId)
        {
            var result = new List<Share>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select ShareId from Shares where userId = @userId";
                    command.Parameters.AddWithValue("@userId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(GetShare(reader.GetGuid(reader.GetOrdinal("ShareId"))));
                        }
                        return result;
                    }
                }
            }
        }
    }
}
