using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

class AdreseRoluri
{
    public List<AdresaRol>? roluri {get; set;}
    public AdreseRoluri(){
        try
        {
            this.roluri = new List<AdresaRol>();
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            string sql = "SELECT ";
            sql += "	roluri.id";
            sql += "	, roluri.cod_cfg_localitati";
            sql += "	, roluri.tip";
            sql += "	, roluri.volum";
            sql += "	, roluri.pozitie";
            sql += "	, roluri.id_adresa_rol";
            sql += "	, roluri.cod_cfg_exploatatii";
            sql += "	, roluri.id_persoana";
            sql += "	, roluri.rol_impozite";
            sql += "	, roluri.data_declaratie";
            sql += "	, roluri.nr_inregistrare";
            sql += "	, roluri.data_inregistrare";
            sql += "	, roluri.semnat";
            sql += "	, roluri.anulat";
            sql += " FROM adrese_roluri roluri";
            sql += " ORDER BY ";
            sql += "	roluri.tip";
            sql += "	, roluri.cod_cfg_localitati";
            sql += "	, roluri.volum";
            sql += "	, roluri.pozitie; ";
            Console.WriteLine(sql);
            command.CommandText = sql;

            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                this.roluri.Add(new AdresaRol(){
                id = reader.GetString("id"),
                cod_cfg_localitati = reader.GetInt32("cod_cfg_localitati"),
                tip = reader.GetInt32("tip"),
                volum = reader.GetInt32("volum"),
                pozitie = reader.GetInt32("pozitie"),
                id_adresa_rol = reader.GetString("id_adresa_rol"),
                cod_cfg_exploatatii = reader.GetInt32("cod_cfg_exploatatii"),
                id_persoana = reader.GetString("id_persoana"),
                rol_impozite = reader.GetString("rol_impozite"),
                data_declaratie = reader.IsDBNull(reader.GetOrdinal("data_declaratie")) ? null : reader.GetDateTime("data_declaratie") ,
                nr_inregistrare =  reader.GetString("nr_inregistrare"),
                data_inregistrare = reader.IsDBNull(reader.GetOrdinal("data_inregistrare")) ? null : reader.GetDateTime("data_inregistrare"),
                semnat = reader.IsDBNull(reader.GetOrdinal("semnat")) ? null : reader.GetInt32("semnat"),
                anulat = reader.IsDBNull(reader.GetOrdinal("anulat")) ? null : reader.GetInt32("anulat")
               });
            }
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            jso.IncludeFields=true;
            jso.WriteIndented=true;
            string json = JsonSerializer.Serialize( roluri, jso );
            File.WriteAllText("adrese_roluri.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
class AdresaRol
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public int? cod_cfg_localitati { get; set; } 
        public int? tip { get; set; }
        public int? volum { get; set; }
        public int? pozitie { get; set; }
        public string id_adresa_rol { get; set; } = Guid.NewGuid().ToString();
        public int? cod_cfg_exploatatii { get; set; }
        public string id_persoana { get; set; } = Guid.NewGuid().ToString();
        public string? rol_impozite { get; set; }
        public DateTime? data_declaratie { get; set; }
        public string? nr_inregistrare { get; set; }
        public DateTime? data_inregistrare { get; set; }
        public int? semnat { get; set; }
        public int? anulat { get; set; }
    }