using System;
using System.Data.SqlClient;

namespace ORM
{

    class Program
    {
        const string ConnectionString = "Server=.\\sqlexpress; Database= CoolDB; Integrated Security=True;";

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

        private static void Read(SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "select Id, SPY, Clor, [Power] from Cars";
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
                var spz = reader.GetString(1);
                var color = reader.GetString(2);
                var power = reader.GetString(3);

                /*
                null check
                1.

                reader.IsDbNull("SPZ");

                2.
                var spzObject = reader["SPZ"];
                is obj DBNull ? 
                 */

                //Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                Console.WriteLine("Connected...");

                //stuff
                //Insert(connection);
                Update(connection);

                connection.Close();
                Console.WriteLine("Closed...");
            }
        }
    }
}
