using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

class CfgExploatatii
{
    public List<Exploatatie>? exploatatii {get; set;}
    public CfgExploatatii(){
        try
        {
            this.exploatatii = new List<Exploatatie>();
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT e.cod, e.descriere FROM cfg_exploatatii e ORDER BY e.cod;";

            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
               this.exploatatii.Add(new Exploatatie(){
                cod = reader.GetInt32("cod"),
                descriere = reader.GetString("descriere")
               });
            }
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            jso.IncludeFields=true;
            jso.WriteIndented=true;
            string json = JsonSerializer.Serialize( exploatatii, jso );
            File.WriteAllText("CfgExploatatii.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
class Exploatatie
    {
        public int cod { get; set; }
        public string? descriere { get; set; } 
    }