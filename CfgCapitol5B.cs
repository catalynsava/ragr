using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

class CfgCapitol5B
{
    public List<RandCapitol5B>? randuriCapitol5B {get; set;}
    public CfgCapitol5B(){
        try
        {
            this.randuriCapitol5B = new List<RandCapitol5B>();
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT c5b.cod, c5b.descriere, c5b.formula, c5b.cod_parinte, c5b.semn FROM cfg_capitol_5b c5b ORDER BY c5b.cod;";

            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
               this.randuriCapitol5B.Add(new RandCapitol5B(){
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
            string json = JsonSerializer.Serialize( randuriCapitol5B, jso );
            File.WriteAllText("CfgCapitol5B.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
class RandCapitol5B
    {
        public int cod { get; set; }
        public string? descriere { get; set; } 
        public string? formula {get; set; }
        public int? codParinte {get; set; }
        public string? semn { get; set; }
    }