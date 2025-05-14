using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

class CfgCapitol3
{
    public List<RandCapitol3>? randuriCapitol3 {get; set;}
    public CfgCapitol3(){
        try
        {
            this.randuriCapitol3 = new List<RandCapitol3>();
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT c3.cod, c3.descriere, c3.formula, c3.cod_parinte, c3.semn FROM cfg_capitol_3 c3 ORDER BY c3.cod;";

            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
               this.randuriCapitol3.Add(new RandCapitol3(){
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
            string json = JsonSerializer.Serialize( randuriCapitol3, jso );
            File.WriteAllText("CfgCapitol3.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
class RandCapitol3
    {
        public int cod { get; set; }
        public string? descriere { get; set; } 
        public string? formula {get; set; }
        public int? codParinte {get; set; }
        public string? semn { get; set; }
    }