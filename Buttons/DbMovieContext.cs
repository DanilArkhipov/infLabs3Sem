using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Prac4.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Prac4
{
    public class DbMovieContext
    {
        private readonly IConfiguration _configuration;

        private string connectionString;

        public DbMovieContext(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = configuration["ConnectionStrings:SqlServer"];
        }

        public async Task<List<Movies>> GetAllAsync()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var cmd = "Select * From movies";
                SqlCommand command = new SqlCommand(cmd, connection);
                var resReader = await command.ExecuteReaderAsync();
                var resList = new List<Movies>();
                if (resReader.HasRows)
                {
                    while (await resReader.ReadAsync())
                    {
                        resList.Add(new Movies(
                            resReader.GetValue(1).ToString(),
                            resReader.GetValue(2).ToString(),
                            resReader.GetValue(3).ToString(),
                            resReader.GetValue(4).ToString(),
                            int.Parse(resReader.GetValue(0).ToString())
                        ));
                    }
                }

                return resList;
            }

        }

        public async Task<List<Movies>> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlParameter param = new SqlParameter("@id", id);
                var cmd = "Select * From movies where movies.id = @id";
                SqlCommand command = new SqlCommand(cmd, connection);
                command.Parameters.Add(param);
                var resReader = await command.ExecuteReaderAsync();
                var resList = new List<Movies>();
                if (resReader.HasRows)
                {
                    while (await resReader.ReadAsync())
                    {
                        resList.Add(new Movies(
                            resReader.GetValue(1).ToString(),
                            resReader.GetValue(2).ToString(),
                            resReader.GetValue(3).ToString(),
                            resReader.GetValue(4).ToString(),
                            int.Parse(resReader.GetValue(0).ToString())
                        ));
                    }
                }

                return resList;
            }

        }

        public async Task InsertAsync(Movies movie)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var cmd =
                    "Insert Into movies (name,genre,author,relise_date) VALUES (@name,@genre,@author,@relise_date);";
                var paramName = new SqlParameter("@name",movie.Name);
                var paramGenre = new SqlParameter("@genre",movie.Genre);
                var paramAuthor = new SqlParameter("@author",movie.Author);
                var paramReleaseDate = new SqlParameter("@relise_date",movie.Relise_Date);
                var command = new SqlCommand(cmd,connection);
                command.Parameters.Add(paramName);
                command.Parameters.Add(paramGenre);
                command.Parameters.Add(paramAuthor);
                command.Parameters.Add(paramReleaseDate);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateAsync(int id, Movies movie)
        {
            var type = movie.GetType();
            var props = type.GetProperties().Where(p=>p.GetValue(movie)!=null && p.Name!="Id").ToArray();
            if (props.Length != 0)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    var cmdUpdatePart = "";
                    for (int i = 0;i<props.Length-1;i++)
                    {
                        cmdUpdatePart += string.Format("{0} = @{0},", props[i].Name);
                    }
                    cmdUpdatePart += string.Format("{0} = @{0}", props[props.Length-1].Name);

                    var cmd = string.Format("Update movies set {0} where movies.id = @id",cmdUpdatePart);
                    var command = new SqlCommand(cmd,connection);
                    foreach (var p in props)
                    {
                        command.Parameters.Add(new SqlParameter("@"+p.Name,p.GetValue(movie)));
                    }
                    command.Parameters.Add(new SqlParameter("@id",id));
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlParameter param = new SqlParameter("@id", id);
                var cmd = "Delete From movies where id = @id";
                SqlCommand command = new SqlCommand(cmd, connection);
                command.Parameters.Add(param);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}