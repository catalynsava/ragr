using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

class CfgCapitol2A
{
    public List<RandCapitol2A>? randuriCapitol2A {get; set;}
    public CfgCapitol2A(){
        try
        {
            this.randuriCapitol2A = new List<RandCapitol2A>();
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT c2a.cod, c2a.descriere, c2a.formula, c2a.cod_parinte, c2a.semn FROM cfg_capitol_2a c2a ORDER BY c2a.cod;";

            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
               this.randuriCapitol2A.Add(new RandCapitol2A(){
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
            string json = JsonSerializer.Serialize( randuriCapitol2A, jso );
            File.WriteAllText("CfgCapitol2A.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
class RandCapitol2A
    {
        public int cod { get; set; }
        public string? descriere { get; set; } 
        public string? formula {get; set; }
        public int? codParinte {get; set; }
        public string? semn { get; set; }
    }