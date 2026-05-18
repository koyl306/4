using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace pract9
{
    public class DbService
    {
        private readonly string _connectionString = "Data Source=M:\\programing\\uni\\pract9\\file.sqli";

        public List<Experiment> GetAll()
        {
            var result = new List<Experiment>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText =
                @"SELECT id, name, date, usedmaterial, result FROM Experiments";

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                result.Add(new Experiment
                {
                    Id = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Date = reader.GetString(2),
                    UsedMaterial = reader.GetString(3),
                    Result = reader.GetString(4)
                });
            }

            return result;
        }

        public void Insert(Experiment e)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText =
                @"INSERT INTO Experiments (Name, Date, UsedMaterial, Result)
              VALUES ($name, $date, $usedmaterial, $result)";

            cmd.Parameters.AddWithValue("$name", e.Name);
            cmd.Parameters.AddWithValue("$date", DateTime.Now.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("$usedmaterial", e.UsedMaterial);
            cmd.Parameters.AddWithValue("$result", e.Result);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText =
                "DELETE FROM Experiments WHERE Id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            cmd.ExecuteNonQuery();
        }
    }
}
