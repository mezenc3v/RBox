using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBox.Model;

namespace RBox.Data.Sql
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly string _connectionString;

        public CommentsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Comment GetComment(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select CommentId, UserId, FileId, Text, Date from Comments where CommentId = @id";
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Comment
                            {
                                CommentId = reader.GetGuid(reader.GetOrdinal("CommentId")),
                                FileId = reader.GetGuid(reader.GetOrdinal("FileId")),
                                UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                                Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                                Text = reader.GetString(reader.GetOrdinal("Text"))
                            };
                        }
                        throw new ArgumentException($"comment {id} not found");
                    }
                }
            }
        }

        public Comment AddComment(Comment comment)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "insert into Comments (CommentId, UserId, FileId, Text, Date) values (@CommentId, @UserId, @FileId, @Text, @Date)";
                    var commentId = Guid.NewGuid();
                    var currentDate = DateTime.Now;
                    command.Parameters.AddWithValue("@CommentId", commentId);
                    command.Parameters.AddWithValue("@UserId", comment.UserId);
                    command.Parameters.AddWithValue("@FileId", comment.FileId);
                    command.Parameters.AddWithValue("@Text", comment.Text);
                    command.Parameters.AddWithValue("@Date", currentDate);
                    command.ExecuteNonQuery();
                    comment.CommentId = commentId;
                    return comment;
                }
            }
        }

        public IEnumerable<Comment> GetComments(Guid fileId)
        {
            var result = new List<Comment>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select CommentId from Comments where FileId = @id";
                    command.Parameters.AddWithValue("@id", fileId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(GetComment(reader.GetGuid(reader.GetOrdinal("CommentId"))));
                        }
                        return result;
                    }
                }
            }
        }

        public void UpdateComment(Guid commentId, Comment comment)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "update Comments set Text = @text, Date = @date where CommentId = @id";
                    command.Parameters.AddWithValue("@id", commentId);
                    command.Parameters.AddWithValue("@text", comment.Text);
                    command.Parameters.AddWithValue("@date", DateTime.Now);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteComment(Guid commentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "delete from Comments where CommentId = @id";
                    command.Parameters.AddWithValue("@id", commentId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
