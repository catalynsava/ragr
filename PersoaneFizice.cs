using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Ra;

public class PersoaneFizice
{
    public List<PersoanaFizica>? persoaneFizice { get; set; }

    public PersoaneFizice(){
        try
        {
            this.persoaneFizice = new List<PersoanaFizica>();
            var builder = new ConfigurationBuilder();
            IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            using var connection = new MySqlConnection(
                configuration.GetSection("connection").Value
               
            );
            connection.Open();

            using var command = connection.CreateCommand();
            string sql = "SELECT ";
            sql += "	pf.id";
            sql += "	, pf.cnp";
            sql += "	, pf.sex";
            sql += "	, pf.data_nasterii";
            sql += "	, pf.nume";
            sql += "	, pf.initiala";
            sql += "	, pf.prenume";
            sql += "	, pf.email";
            sql += "	, pf.telefon";
            sql += "	, pf.buletin";
            sql += "	, pf.id_adrese";
            sql += " FROM persoane_fizice pf;";
            Console.WriteLine(sql);
            command.CommandText = sql;

            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                this.persoaneFizice.Add(new PersoanaFizica(){
                    id = reader.GetString("id"),
                    cnp = reader.GetString("cnp"),
                    sex = reader.GetInt32("sex"),
                    data_nasterii = reader.IsDBNull(reader.GetOrdinal("data_nasterii")) ? null :  reader.GetDateTime("data_nasterii"),
                    nume = reader.GetString("nume"),
                    initiala = reader.GetString("initiala"),
                    prenume = reader.GetString("prenume"),
                    email = reader.GetString("email"),
                    telefon = reader.GetString("telefon"),
                    buletin = reader.GetString("buletin"),
                    id_adrese = reader.GetString("id_adrese")
               });
            }
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            jso.IncludeFields=true;
            jso.WriteIndented=true;
            string json = JsonSerializer.Serialize( persoaneFizice, jso );
            File.WriteAllText("PersoaneFizice.json", json);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
public class PersoanaFizica
    {
        public string? id { get; set; } = Guid.NewGuid().ToString();
        public string? cnp { get; set; }
        public int sex { get; set; } = 0;
        public DateTime? data_nasterii { get; set; }
        public string? nume { get; set; }
        public string? initiala { get; set; }
        public string? prenume { get; set; }
        public string? email { get; set; }
        public string? telefon { get; set; }
        public string? buletin { get; set; }
        public string id_adrese { get; set; } =Guid.NewGuid().ToString();

        public static string verificareCNP(string pCnp){
            string[] m = {"1", "3", "5", "7"};
            string[] f = ["2", "4", "6", "8"];
            string mesajValidare = "";
            //sex valid
            if (pCnp.Length > 0)
            {
                if (int.TryParse( pCnp.Substring(0,1), out _ ))
                {
                    if(
                    int.Parse( pCnp.Substring(0,1) ) > 0 
                    && 
                    int.Parse( pCnp.Substring(0,1) ) < 9
                    ) {
                        if (m.Contains(pCnp.Substring(0,1)))
                        {
                            mesajValidare = "M";
                        }
                        else if ( f.Contains(pCnp.Substring(0,1)) )
                        {
                            mesajValidare = "F";
                        }else{
                            mesajValidare = "{sex?}";
                        }
                    }else
                    {
                        mesajValidare = "{sex?}";
                    }
                }else
                {
                    mesajValidare ="{sex?}";
                }
            }
            //--
            mesajValidare += " ";
            // extrage anul nașterii
            if (pCnp.Length > 2)
            {
                if ( pCnp.All(Char.IsNumber) )
                {
                    if(pCnp[0] == '1' || pCnp[0] == '2'){
                        mesajValidare += "19" + pCnp.Substring(1,2);
                    }else if (pCnp[0] == '3' || pCnp[0] == '4')
                    {
                        mesajValidare += "18" + pCnp.Substring(1,2);
                    }else if (pCnp[0] == '5' || pCnp[0] == '6' || pCnp[0] == '7' || pCnp[0] == '8')
                    {
                        if ( int.Parse("20" + pCnp.Substring(1,2)) <= DateTime.Now.Year )
                        {
                            mesajValidare += "20" + pCnp.Substring(1,2);
                        }else{
                            mesajValidare += "{?an}";
                        }
                    }else{
                        mesajValidare += "{?an}";
                    }
                }else{
                    mesajValidare += "{?an}";
                }
            }else{
                    mesajValidare += "{?an}";
            }
            //--
            mesajValidare += " ";  
            //extrage luna nașterii
            string[] lunileAnului = ["ianuarie", "februarie", "martie", "aprilie", "mai", "iunie", "iulie", "august", "septembrie", "octombrie", "noiembrie", "decembrie"];
            if ( pCnp.Length > 4 )
            {
                if ( pCnp.Substring(3,2).All(Char.IsNumber) ){
                    if( int.Parse(pCnp.Substring(3,2)) > 0 && int.Parse(pCnp.Substring(3,2)) < 13 ){
                        mesajValidare += lunileAnului[int.Parse(pCnp.Substring(3,2))-1];
                    }else{
                        mesajValidare += "{?luna}";
                    }
                }else{
                    mesajValidare += "{?luna}";
                }
            } else {
                mesajValidare += "{?luna}";
            }
            //--
            mesajValidare += " ";
            // extrage ziua nașterii
            if (pCnp.Length > 6)
            {
                if ( pCnp.Substring(5,2).All(Char.IsNumber) )
                {
                    mesajValidare += pCnp.Substring(5,2);
                }else{
                    mesajValidare += "{?zi}";
                }
            }else{
                mesajValidare += "{?zi}";
            }
            //--
            mesajValidare += " ";
            // data nasterii validă
            string tmpAn;
            string tmpLuna;
            string tmpZi;
            if (pCnp.Length > 6)
            {
                if (pCnp.All(Char.IsNumber))
                {
                    if( pCnp.Substring(0,1) == "1" || pCnp.Substring(0,1) == "2" ){
                        tmpAn = "19" + pCnp.Substring(1,2);
                    }else if( pCnp.Substring(0,1) == "3" || pCnp.Substring(0,1) == "4" ){
                        tmpAn = "18" + pCnp.Substring(1,2);
                    }else if( pCnp.Substring(0,1) == "5" || pCnp.Substring(0,1) == "6" || pCnp.Substring(0,1) == "7" || pCnp.Substring(0,1) == "8" ){
                        tmpAn = "20" + pCnp.Substring(1,2);
                    }else{
                        tmpAn = "";
                    }
                    tmpLuna = pCnp.Substring(3,2);
                    tmpZi = pCnp.Substring(5,2);
                    if( DateTime.TryParse(tmpAn + "-" + tmpLuna + "-" + tmpZi, out _) ){
                        mesajValidare += "";
                    }else{
                        mesajValidare += "{?data}";
                    }
                }else{
                    mesajValidare += "{?data}";
                }
            }else{
                mesajValidare += "{?data}";
            }
            //--
            //mesajValidare += " ";
            // judet
            string[] judete = ["Alba", "Arad", "Argeș", "Bacău", "Bihor", "Bistrița-Năsăud", "Botoșani", "Brașov", "Brăila", "Buzău", "Caraș-Severin", "Cluj", "Constanța", "Covasna", "Dâmbovița", "Dolj", "Galați", "Gorj", "Harghita", "Hunedoara", "Ialomița", "Iași", "Ilfov", "Maramureș", "Mehedinți", "Mureș", "Neamț", "Olt", "Prahova", "Satu Mare", "Sălaj", "Sibiu", "Suceava", "Teleorman", "Timiș", "Tulcea", "Vaslui", "Vâlcea", "Vrancea", "București", "București - Sector 1", "București - Sector 2", "București - Sector 3", "București - Sector 4", "București - Sector 5", "București - Sector 6", "Călărași", "Giurgiu", "Bucuresti - Sector 7 (desființat)", "Bucuresti - Sector 8 (desființat)"];
            if (pCnp.Length > 8)
            {
                if ( pCnp.Substring(7,2).All(Char.IsNumber) )
                {
                    if ( int.Parse(pCnp.Substring(7,2)) <= 50 )
                    {
                        mesajValidare += judete[int.Parse(pCnp.Substring(7,2))-1];
                    }else
                    {
                        mesajValidare += "{?județ}";
                    }
                }else{
                    mesajValidare += "{?județ}";
                }
            }else
            {
                mesajValidare += "{?județ}";
            }
            //--
            mesajValidare += " ";
            //cifra control
            int[] control = { 2, 7, 9, 1, 4, 6, 3, 5, 8, 2, 7, 9 };
            int suma = 0;

            if (pCnp.Length > 11)
            {
                for (int i = 0; i < 12; i++)
                {
                    suma += (pCnp[i] - '0') * control[i];
                }
                int rest = suma % 11;
                int cifraControl = rest == 10 ? 1 : rest;
                if ( pCnp.Length > 12 )
                {
                    if (int.Parse(pCnp.Substring(12,1)) != cifraControl)
                    {
                        mesajValidare += pCnp.Substring(12,1) + "->" + cifraControl;
                    }else{
                        mesajValidare += "{valid}";
                    }
                }else{
                    mesajValidare += pCnp + "?" + "->" + cifraControl;
                }
                
            }else{
                mesajValidare += "{?cifra.control}";
            }
            return mesajValidare;
        }
    }

