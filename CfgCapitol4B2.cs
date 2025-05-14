using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

class CfgCapitol4B2
{
    public List<RandCapitol4B2>? randuriCapitol4B2 {get; set;}
    public CfgCapitol4B2(){
        try
        {
            this.randuriCapitol4B2 = new List<RandCapitol4B2>();
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT c4b2.cod, c4b2.descriere, c4b2.formula, c4b2.cod_parinte, c4b2.semn FROM cfg_capitol_4b2 c4b2 ORDER BY c4b2.cod;";

            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
               this.randuriCapitol4B2.Add(new RandCapitol4B2(){
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
            string json = JsonSerializer.Serialize( randuriCapitol4B2, jso );
            File.WriteAllText("CfgCapitol4B2.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
class RandCapitol4B2
    {
        public int cod { get; set; }
        public string? descriere { get; set; } 
        public string? formula {get; set; }
        public int? codParinte {get; set; }
        public string? semn { get; set; }
    }