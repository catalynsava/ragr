using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

class CfgTipuriRoluri
{
    public List<TipRol>? tipuriRoluri {get; set;}
    public CfgTipuriRoluri(){
        try
        {
            this.tipuriRoluri = new List<TipRol>();
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT tr.cod, tr.descriere FROM cfg_tip_roluri tr ORDER BY tr.cod;";

            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
               this.tipuriRoluri.Add(new TipRol(){
                cod = reader.GetInt32("cod"),
                descriere = reader.GetString("descriere")
               });
            }
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            jso.IncludeFields=true;
            jso.WriteIndented=true;
            string json = JsonSerializer.Serialize( tipuriRoluri, jso );
            File.WriteAllText("CfgTipuriRoluri.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
class TipRol
    {
        public int cod { get; set; }
        public string? descriere { get; set; } 
    }