using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

public class Opis
{
    public static void citesteOpis(){
        var items = new Dictionary<object, Dictionary<string, object>>();

        try
        {
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
               configuration.GetSection("connection").Value
            );
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM opis;";

            using var reader = command.ExecuteReader();
            while (reader.Read()){
                var item = new Dictionary<string, object>(reader.FieldCount - 1);
                for (var i = 1; i < reader.FieldCount; i++)
                {
                    item[reader.GetName(i)] = reader.GetValue(i);
                }
                items[reader.GetValue(0)] = item;
            }
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            jso.IncludeFields=true;
            jso.WriteIndented=true;
            string json = JsonSerializer.Serialize(items, jso);
            File.WriteAllText("opis.json", json);
            Console.WriteLine(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
