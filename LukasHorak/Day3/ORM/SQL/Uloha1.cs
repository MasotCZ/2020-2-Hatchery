using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UlohyUtility;

namespace ORM.SQL
{
    class Uloha1 : IUloha
    {
        private static void Delete(SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "delete from Car where color = @red";
            CreateSqlParam(command, "red", "red");

            var rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected != 1)
            {
                throw new Exception("ERRR");
            }
        }

        private static void Update(SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "update Car set color=@clr where power = 38";
            CreateSqlParam(command, "clr", "purple");

            var rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected != 1)
            {
                throw new Exception("ERRR");
            }
        }

        private static void Insert(SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "insert into Car (Id, SPz, Color, [Power]) values (@id, @spz, @color, @power)";
            CreateSqlParam(command, "id", Guid.NewGuid().ToString());
            CreateSqlParam(command, "spz", DBNull.Value);
            CreateSqlParam(command, "color", "red");
            CreateSqlParam(command, "power", 58);

            var rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected != 1)
            {
                throw new Exception("ERRR");
            }
        }

        private static void CreateSqlParam(SqlCommand command, string paramName, object paramValue)
        {
            var param = command.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue;
            command.Parameters.Add(param);
        }

        private static void Read(SqlConnection connection, SqlTransaction tr)
        {
            SqlCommand command = connection.CreateCommand();
            command.Transaction = tr;
            command.CommandText = "select Id, spz, color, [Power] from Car";
            var reader = command.ExecuteReader();

            //radky zpetny vazby, pocet radku
            while (reader.Read())
            {
                //bud index jako u pole a nebo getSTring but s indexem a nebo s jmenem
                //vypada to ze potrebuju novejsi .net na string

                //var id = reader.GetString("ID");
                //var spz = reader.GetString("SPZ");
                //var color = reader.GetString("Color");
                //var power = reader.GetString("Power");

                var id = reader.GetString(0);
                var spz = reader[1];
                string spzVal = "";
                if (spz is null || spz is DBNull)
                {
                    spzVal = "null";
                }
                else
                {
                    spzVal = spz.ToString();
                }

                var color = reader.GetString(2);
                var power = reader.GetDecimal(3);

                /*
                null check
                1.

                reader.IsDbNull("SPZ");

                2.
                var spzObject = reader["SPZ"];
                is obj DBNull ? 
                 */

                Console.WriteLine($"{id}, {spz}, {color}, {power}");
            }

            reader.Close();
        }

        public void Execute()
        {
            using (SqlConnection connection = new SqlConnection(Config.ConnectionString))
            {
                connection.Open();
                Console.WriteLine("Connected...");

                using (var tr = connection.BeginTransaction())
                {

                    //stuff
                    Read(connection, tr);
                    //Insert(connection);
                    //Update(connection);
                    //Delete(connection);

                    tr.Commit();

                }

                connection.Close();
                Console.WriteLine("Closed...");
            }
        }
    }
}
