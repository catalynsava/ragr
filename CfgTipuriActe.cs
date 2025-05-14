using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

class CfgTipuriActe
{
    public List<TipAct>? tipuriActe {get; set;}
    public CfgTipuriActe(){
        try
        {
            this.tipuriActe = new List<TipAct>();
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT ta.cod, ta.abreviere, ta.descriere FROM cfg_tipuri_acte ta ORDER BY ta.cod;";

            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
               this.tipuriActe.Add(new TipAct(){
                cod = reader.GetInt32("cod"),
                abreviere = reader.GetString("abreviere"),
                descriere = reader.GetString("descriere")
               });
            }
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            jso.IncludeFields=true;
            jso.WriteIndented=true;
            string json = JsonSerializer.Serialize( tipuriActe, jso );
            File.WriteAllText("CfgTipuriActe.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
class TipAct
    {
        public int cod { get; set; }
        public string? abreviere { get; set; } 
        public string? descriere { get; set; } 
    }