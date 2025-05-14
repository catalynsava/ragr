using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

class CfgFormeOrganizare
{
    public List<FormaOrganizare>? formeOrganizare {get; set;}
    public CfgFormeOrganizare(){
        try
        {
            this.formeOrganizare = new List<FormaOrganizare>();
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT fo.cod, fo.abreviere, fo.descriere FROM cfg_forme_organizare fo ORDER BY fo.cod;";

            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
               this.formeOrganizare.Add(new FormaOrganizare(){
                cod = reader.GetInt32("cod"),
                abreviere = reader.GetString("abreviere"),
                descriere = reader.GetString("descriere")
               });
            }
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            jso.IncludeFields=true;
            jso.WriteIndented=true;
            string json = JsonSerializer.Serialize( formeOrganizare, jso );
            File.WriteAllText("CfgFormeOrganizare.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
class FormaOrganizare
    {
        public int cod { get; set; }
        public string? abreviere { get; set; } 
        public string? descriere { get; set; } 
    }