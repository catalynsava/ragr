using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

class CfgCapitole
{
    public List<Capitol>? capitole {get; set;}
    public CfgCapitole(){
        try
        {
            this.capitole = new List<Capitol>();
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT c.cod, c.denumire, c.descriere FROM cfg_capitole c ORDER BY c.cod;";

            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
               this.capitole.Add(new Capitol(){
                cod = reader.GetInt32("cod"),
                denumire = reader.GetString("denumire"),
                descriere = reader.GetString("descriere")
               });
            }

            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            jso.IncludeFields=true;
            jso.WriteIndented=true;

            string json = JsonSerializer.Serialize(capitole, jso);
            Console.WriteLine(json);
            File.WriteAllText("CfgCapitole.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
class Capitol
    {
        public int cod { get; set; }
        public string? denumire { get; set; } 
        public string? descriere { get; set; } 
    }