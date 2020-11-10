using Microsoft.Extensions.Configuration;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SuperHero.Infrastructure.Repositories
{
    public class HeroRepository : IHeroRepository
    {
        private readonly IConfiguration _configuration;
        public HeroRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Hero> Get()
        {
            try
            {
                using(var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var heroList = new List<Hero>();
                    var sqlCmd = @"SELECT H.Id,
                                            H.Name,
                                            E.Id as IdEditor,
                                            E.Name as Editor,
                                            H.Age,
                                            H.Created
                                        FROM HERO H
                                    JOIN EDITOR E ON H.IdEditor = E.Id";

                    using (var cmd = new  SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();

                        var reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            var hero = new Hero(int.Parse(reader["id"].ToString()),
                                                reader["Name"].ToString(),
                                                new Editor(int.Parse(reader["idEditor"].ToString()),
                                                            reader["Editor"].ToString()),
                                                int.Parse(reader["Age"].ToString()),
                                                DateTime.Parse(reader["Created"].ToString()));

                            heroList.Add(hero);
                        }

                        return heroList;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Hero> GetByIdAsync(int id)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var heroList = new List<Hero>();
                    var sqlCmd = "dbo.SELECIONAR_HEROIS_POR_ID";

                    using (var cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);

                        con.Open();

                        var reader = await cmd
                                            .ExecuteReaderAsync()
                                            .ConfigureAwait(false);

                        while (reader.Read())
                        {
                            var hero = new Hero(int.Parse(reader["id"].ToString()),
                                                reader["Name"].ToString(),
                                                new Editor(int.Parse(reader["idEditor"].ToString()),
                                                            reader["Editor"].ToString()),
                                                int.Parse(reader["Age"].ToString()),
                                                DateTime.Parse(reader["Created"].ToString()));

                            return hero;
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

        public async Task<int> InsertAsync(Hero hero)
        {
            try
            {
                using (var con = new SqlConnection(_configuration["ConnectionString"]))
                {
                    var sqlCmd = @"INSERT INTO 
                                    HERO (Name, 
                                        IdEditor, 
                                        Age, 
                                        Created) 
                               VALUES (@name, 
                                        @editor,
                                        @age, 
                                        @created); SELECT scope_identity();";

                    using (SqlCommand cmd = new SqlCommand(sqlCmd, con))
                    {
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("name", hero.Name);
                        cmd.Parameters.AddWithValue("editor", hero.Editor.Id);
                        cmd.Parameters.AddWithValue("age", hero.Age);
                        cmd.Parameters.AddWithValue("created", hero.Created);

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
    }
}
