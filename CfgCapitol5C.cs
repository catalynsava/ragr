using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

class CfgCapitol5C
{
    public List<RandCapitol5C>? randuriCapitol5C {get; set;}
    public CfgCapitol5C(){
        try
        {
            this.randuriCapitol5C = new List<RandCapitol5C>();
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT c5c.cod, c5c.descriere, c5c.formula, c5c.cod_parinte, c5c.semn FROM cfg_capitol_5c c5c ORDER BY c5c.cod;";

            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
               this.randuriCapitol5C.Add(new RandCapitol5C(){
                cod = reader.GetInt32("cod"),
                descriere = reader.GetString("descriere"),
                formula = reader.GetString("formula"),
                codParinte = reader.GetInt32("cod_parinte"),
                semn = reader.GetString("semn")
               });
            }
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            jso.IncludeFields=true;
            jso.WriteIndented=true;
            string json = JsonSerializer.Serialize( randuriCapitol5C, jso );
            File.WriteAllText("CfgCapitol5C.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
class RandCapitol5C
    {
        public int cod { get; set; }
        public string? descriere { get; set; } 
        public string? formula {get; set; }
        public int? codParinte {get; set; }
        public string? semn { get; set; }
    }