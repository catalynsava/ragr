using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

class CfgModalitatiDetinere
{
    public List<ModalitateDetinere>? modalitatiDetinere {get; set;}
    public CfgModalitatiDetinere(){
        try
        {
            this.modalitatiDetinere = new List<ModalitateDetinere>();
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT md.cod, md.abreviere, md.descriere FROM cfg_modalitati_detinere md ORDER BY md.cod;";

            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
               this.modalitatiDetinere.Add(new ModalitateDetinere(){
                cod = reader.GetInt32("cod"),
                abreviere = reader.GetString("abreviere"),
                descriere = reader.GetString("descriere")
               });
            }
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            jso.IncludeFields=true;
            jso.WriteIndented=true;
            string json = JsonSerializer.Serialize( modalitatiDetinere, jso );
            File.WriteAllText("CfgModalitatiDetinere.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
class ModalitateDetinere
    {
        public int cod { get; set; }
        public string? abreviere { get; set; } 
        public string? descriere { get; set; } 
    }