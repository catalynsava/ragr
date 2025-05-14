using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

public class PersoaneJuridice
{
    public List<PersoanaJuridica>? persoaneJuridice {get; set;}

    public PersoaneJuridice(){
        try
        {
            this.persoaneJuridice = new List<PersoanaJuridica>();
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            string sql = "SELECT ";
            sql += "	pj.id";
            sql += "	, pj.cod_forma_organizare";
            sql += "	, pj.denumire";
            sql += "	, pj.filiala";
            sql += "	, pj.cif";
            sql += "	, pj.cui";
            sql += "	, pj.registrul_comertului";
            sql += "	, pj.nume_reprezentant";
            sql += "	, pj.intiala_reprezenant";
            sql += "	, pj.prenume_reprezentant";
            sql += "	, pj.functia";
            sql += "	, pj.telefon";
            sql += "	, pj.email";
            sql += "	, pj.id_adrese";
            sql += " FROM persoane_juridice pj;";
            Console.WriteLine(sql);
            command.CommandText = sql;

            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                this.persoaneJuridice.Add(new PersoanaJuridica(){
                    id = reader.GetString("id"),
                    cod_forma_organizare = reader.IsDBNull(reader.GetOrdinal("cod_forma_organizare")) ? null : reader.GetInt32("cod_forma_organizare"),
                    denumire = reader.GetString("denumire"),
                    filiala = reader.GetOrdinal("filiala").ToString(),
                    cif = reader.GetOrdinal("cif").ToString(), 
                    cui = reader.GetOrdinal("cui").ToString(), 
                    registrul_comertului = reader.GetOrdinal("registrul_comertului").ToString(), 
                    nume_reprezentant = reader.GetOrdinal("nume_reprezentant").ToString(),
                    intiala_reprezenant = reader.GetOrdinal("intiala_reprezenant").ToString(),
                    prenume_reprezentant = reader.GetOrdinal("prenume_reprezentant").ToString(),
                    functia = reader.GetOrdinal("functia").ToString(),
                    telefon = reader.GetOrdinal("telefon").ToString(),
                    email = reader.GetOrdinal("email").ToString(),
                    id_adrese = reader.GetOrdinal("id_adrese").ToString(),
               });
            }
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            jso.IncludeFields=true;
            jso.WriteIndented=true;
            string json = JsonSerializer.Serialize( persoaneJuridice, jso );
            File.WriteAllText("PersoaneJuridice.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
public class PersoanaJuridica
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public int? cod_forma_organizare { get; set; }
        public string denumire { get; set; } = "";
        public string filiala { get; set; } = "";
        public string cif { get; set; } = "";
        public string cui { get; set; } = "";
        public string registrul_comertului { get; set; } = "";
        public string nume_reprezentant { get; set; } = "";
        public string intiala_reprezenant { get; set; } = "";
        public string prenume_reprezentant { get; set; } = "";
        public string functia { get; set; } = "";
        public string telefon { get; set; } = "";
        public string email { get; set; } = "";
        public string id_adrese { get; set; } = Guid.NewGuid().ToString();
    }

