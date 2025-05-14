using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

class CfgLocalitati
{
    public List<Localitate>? localitati {get; set;}
    public CfgLocalitati(){
        try
        {
            this.localitati = new List<Localitate>();
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT l.cod, l.localitate, l.cod_siruta, l.cod_postal FROM cfg_localitati l ORDER BY l.cod;";

            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
               this.localitati.Add(new Localitate(){
                cod = reader.GetInt32("cod"),
                localitate = reader.GetString("localitate"),
                cod_siruta = reader.GetString("cod_siruta"),
                cod_postal = reader.GetString("cod_postal")
               });
            }
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            jso.IncludeFields=true;
            jso.WriteIndented=true;
            string json = JsonSerializer.Serialize( localitati, jso );
            File.WriteAllText("CfgLocalitati.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
class Localitate
    {
        public int cod { get; set; } 
        public string? localitate { get; set; } 
        public string? cod_siruta { get; set; }
        public string? cod_postal { get; set; }
    }