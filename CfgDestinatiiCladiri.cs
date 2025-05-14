using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

class CfgDestinatiiCladiri
{
    public List<DestinatieCladire>? destinatiiCladiri {get; set;}
    public CfgDestinatiiCladiri(){
        try
        {
            this.destinatiiCladiri = new List<DestinatieCladire>();
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT dc.cod, dc.abreviere, dc.descriere FROM cfg_destinatii_cladiri dc ORDER BY dc.cod;";

            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
               this.destinatiiCladiri.Add(new DestinatieCladire(){
                cod = reader.GetInt32("cod"),
                abreviere = reader.GetString("abreviere"),
                descriere = reader.GetString("descriere")
               });
            }
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            jso.IncludeFields=true;
            jso.WriteIndented=true;
            string json = JsonSerializer.Serialize( destinatiiCladiri, jso );
            File.WriteAllText("CfgDestinatiiCladiri.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
class DestinatieCladire
    {
        public int cod { get; set; }
        public string? abreviere { get; set; } 
        public string? descriere { get; set; } 
    }