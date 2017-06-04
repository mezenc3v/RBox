using System;
using System.Data.SqlClient;
using RBox.Model;
namespace RBox.Data.Sql
{
    public class UsersRepository : IUsersRepository
    {
        private readonly string _connectionString;

        public UsersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User AddUser(User user)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(_connectionString);
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    var userId = Guid.NewGuid();
                    command.CommandText = "insert into users values (@userId,@login,@password,@name)";
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@name", user.Name);
                    command.Parameters.AddWithValue("@login", user.UserLogin);
                    command.Parameters.AddWithValue("@password", user.Password);
                    command.ExecuteNonQuery();
                    return new User
                    {
                        UserId = userId,
                        Name = user.Name,
                        Password = user.Password,
                        UserLogin = user.UserLogin
                    };
                }
            }
            catch (Exception ex)
            {
                Log.Logger.ServiceLog.Fatal(ex.Message);
                throw;
            }
            finally
            {
                connection?.Dispose();
            }
        }

        public void DeleteUser(Guid userId)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(_connectionString);
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "delete from users where UserId = @userId";
                    command.Parameters.AddWithValue("@userId", userId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.Logger.ServiceLog.Fatal(ex.Message);
                throw;
            }
            finally
            {
                connection?.Dispose();
            }
        }

        public User GetUser(Guid userId)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(_connectionString);
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select UserId, Name, UserLogin, Password from users where UserId = @userId";
                    command.Parameters.AddWithValue("@userId", userId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new User
                            {
                                UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                UserLogin = reader.GetString(reader.GetOrdinal("UserLogin")),
                                Password = reader.GetString(reader.GetOrdinal("Password"))
                            };
                        }
                    }
                    throw new ArgumentException("user not found");
                }
            }
            catch (Exception ex)
            {
                Log.Logger.ServiceLog.Fatal(ex.Message);
                throw;
            }
            finally
            {
                connection?.Dispose();
            }
        }

        public User LoginUser(User user)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(_connectionString);
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select UserId, Name, UserLogin, Password from users where UserLogin = @login AND Password = @password";
                    command.Parameters.AddWithValue("@login", user.UserLogin);
                    command.Parameters.AddWithValue("@password", user.Password);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new User
                            {
                                UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                UserLogin = reader.GetString(reader.GetOrdinal("UserLogin")),
                                Password = reader.GetString(reader.GetOrdinal("Password"))
                            };
                        }
                    }
                    throw new ArgumentException("user not found");
                }
            }
            catch (Exception ex)
            {
                Log.Logger.ServiceLog.Fatal(ex.Message);
                throw;
            }
            finally
            {
                connection?.Dispose();
            }
        }

        public User FindUser(User user)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(_connectionString);
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select UserId, Name, UserLogin from users where UserLogin = @login";
                    command.Parameters.AddWithValue("@login", user.UserLogin);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new User
                            {
                                UserId = reader.GetGuid(reader.GetOrdinal("UserId")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                UserLogin = reader.GetString(reader.GetOrdinal("UserLogin")),
                            };
                        }
                    }
                    throw new ArgumentException("user not found");
                }
            }
            catch (Exception ex)
            {
                Log.Logger.ServiceLog.Fatal(ex.Message);
                throw;
            }
            finally
            {
                connection?.Dispose();
            }
        }

        public void UpdateUser(Guid userId, User user)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(_connectionString);
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "update users set Name = @name, Userlogin = @userlogin, Password = @password where userId = @userId";
                    command.Parameters.AddWithValue("@name", user.Name);
                    command.Parameters.AddWithValue("@Userlogin", user.UserLogin);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.ExecuteNonQuery();
                }
                throw new ArgumentException("user not found");
            }
            catch (Exception ex)
            {
                Log.Logger.ServiceLog.Fatal(ex.Message);
                throw;
            }
            finally
            {
                connection?.Dispose();
            }
        }
    }
}
