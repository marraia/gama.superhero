using Microsoft.Extensions.Configuration;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<User> GetByLoginAsync(string login)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @$"SELECT U.Id, 
                                           U.Name,
                                           U.Login,
                                           U.Password,
                                           P.Id as idProfile,
                                           P.Description 
                                        FROM USERS U
                                    JOIN PROFILE P ON U.idProfile = P.Id
                                    WHERE U.login='{login}'";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = await cmd
                                            .ExecuteReaderAsync()
                                            .ConfigureAwait(false);

                        while (reader.Read())
                        {
                            var user = new User(int.Parse(reader["id"].ToString()),
                                                reader["Name"].ToString(),
                                                new Profile(int.Parse(reader["idProfile"].ToString()),
                                                            reader["Description"].ToString()));

                            user.InformationLoginUser(reader["Login"].ToString(), reader["Password"].ToString());
                            return user;
                        }

                        return default;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @$"SELECT U.Id, 
                                           U.Name,
                                           U.Login,
                                           U.Password,
                                           P.Id as idProfile,
                                           P.Description 
                                        FROM USERS U
                                    JOIN PROFILE P ON U.idProfile = P.Id
                                    WHERE U.Id='{id}'";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = await cmd
                                            .ExecuteReaderAsync()
                                            .ConfigureAwait(false);

                        while (reader.Read())
                        {
                            var user = new User(int.Parse(reader["id"].ToString()),
                                                reader["Name"].ToString(),
                                                new Profile(int.Parse(reader["idProfile"].ToString()),
                                                            reader["Description"].ToString()));

                            user.InformationLoginUser(reader["Name"].ToString(), reader["Name"].ToString());
                            return user;
                        }

                        return default;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> InsertAsync(User user)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @"INSERT INTO 
                                    USERS (IdProfile,
                                            Name, 
                                            Login, 
                                            Password, 
                                            Created) 
                               VALUES (@profile, 
                                        @name,
                                        @login, 
                                        @password,
                                        @created); SELECT scope_identity();";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("profile", user.Profile.Id);
                        cmd.Parameters.AddWithValue("name", user.Name);
                        cmd.Parameters.AddWithValue("login", user.Login);
                        cmd.Parameters.AddWithValue("password", user.Password);
                        cmd.Parameters.AddWithValue("created", user.Created);

                        con.Open();
                        var id = await cmd
                                       .ExecuteScalarAsync()
                                       .ConfigureAwait(false);

                        return int.Parse(id.ToString());
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(User user)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @"UPDATE USERS 
                                    SET (IdProfile,
                                            Name, 
                                            Login, 
                                            Password) 
                                   VALUES (@profile, 
                                            @name,
                                            @login, 
                                            @password)";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("profile", user.Profile.Id);
                        cmd.Parameters.AddWithValue("name", user.Name);
                        cmd.Parameters.AddWithValue("login", user.Login);
                        cmd.Parameters.AddWithValue("password", user.Password);

                        con.Open();
                        await cmd
                                .ExecuteScalarAsync()
                                .ConfigureAwait(false);

                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
