using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

class CfgLegaturiRudenie
{
    public List<LegaturaRudenie>? legaturiRudenie {get; set;}
    public CfgLegaturiRudenie(){
        try
        {
            this.legaturiRudenie = new List<LegaturaRudenie>();
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT lr.cod, lr.descriere FROM cfg_legaturi_rudenie lr ORDER BY lr.cod;";

            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
               this.legaturiRudenie.Add(new LegaturaRudenie(){
                cod = reader.GetInt32("cod"),
                descriere = reader.GetString("descriere")
               });
            }
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            jso.IncludeFields=true;
            jso.WriteIndented=true;
            string json = JsonSerializer.Serialize( legaturiRudenie, jso );
            File.WriteAllText("CfgLegaturiRudenie.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
class LegaturaRudenie
    {
        public int cod { get; set; }
        public string? abreviere { get; set; } 
        public string? descriere { get; set; } 
    }