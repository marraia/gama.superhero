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
        public HeroRepository()
        {

        }

        public IEnumerable<Hero> Get()
        {
            try
            {
                using(var con = new SqlConnection(""))
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

        public Task<Hero> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAsync(Hero hero)
        {
            throw new NotImplementedException();
        }
    }
}
