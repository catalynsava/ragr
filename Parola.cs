using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

public class Parola
{
    public static bool verificaParola(string passwordParam){
        try
        {
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            string userParam = configuration.GetSection("user").Value ?? "root";

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT u.`password` FROM cfg_utilizatori u WHERE u.username = @userParam;";
            command.Parameters.AddWithValue("@userParam", userParam);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader.GetString("password") == passwordParam)
                {
                    Console.WriteLine("conectare reusita");
                    reader.Close();
                    connection.Close();
                    connection.Dispose();
                    return true;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
