
namespace Ra;

class Program
{
    public static bool session;
    public static CfgCapitole? cfgCapitole;
    public static CfgLocalitati? cfgLocalitati;
    public static CfgExploatatii? cfgExploatatii;
    public static CfgFormeOrganizare? cfgFormeOrganizare;
    public static CfgDestinatiiCladiri? cfgDestinatiiCladiri;
    public static CfgLegaturiRudenie? cfgLegaturiRudenie;
    public static CfgModalitatiDetinere? cfgModalitatiDetinere;
    public static CfgTipuriActe? cfgTipuriActe;
    public static CfgTipuriRoluri? cfgTipuriRoluri;
    public static CfgCapitol2A? cfgCapitol2A;
    public static CfgCapitol3? cfgCapitol3;
    public static CfgCapitol4A? cfgCapitol4A;
    public static CfgCapitol4B1? cfgCapitol4B1;
    public static CfgCapitol4B2? cfgCapitol4B2;
    public static CfgCapitol4C? cfgCapitol4C;
    public static CfgCapitol5A? cfgCapitol5A;
    public static CfgCapitol5B? cfgCapitol5B;
    public static CfgCapitol5C? cfgCapitol5C;
    public static AdreseRoluri? adreseRoluri;
    public static PersoaneFizice? persoaneFizice;
    public static PersoaneJuridice? persoaneJuridice;
    static void Main(string[] args)
    {
        session = false;
        
        if (args.Length == 0)
        {
            Console.WriteLine("parola:");
            if( Parola.verificaParola( Console.ReadLine() ?? "102938") ){
                session = true;
            }
        }else
        {
            if( Parola.verificaParola(args[0])){
                session = true;
            }
        }
        
        if (session)
        {
            //Roluri roluri = new Roluri();
            //roluri.AdaugarePozitie();

            Opis.citesteOpis();
            Console.Write("apasa o tasta.");
            Console.ReadLine();

            //capitole
            cfgCapitole = new CfgCapitole();
            if (cfgCapitole.capitole is not null)
            {
                foreach (var item in cfgCapitole.capitole)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.denumire + "\t" +
                        item.descriere 
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            //localitati
            cfgLocalitati = new CfgLocalitati();
            if (cfgLocalitati.localitati is not null)
            {
                foreach (var item in cfgLocalitati.localitati)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.localitate + "\t" +
                        item.cod_siruta + "\t" +
                        item.cod_postal
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            //exploatatii
            cfgExploatatii = new CfgExploatatii();
            if (cfgExploatatii.exploatatii is not null)
            {
                foreach (var item in cfgExploatatii.exploatatii)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.descriere
                    );
                }
            } 
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            //destinatii cladiri
            cfgDestinatiiCladiri = new CfgDestinatiiCladiri();
            if (cfgDestinatiiCladiri.destinatiiCladiri is not null)
            {
                foreach (var item in cfgDestinatiiCladiri.destinatiiCladiri)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.abreviere + "\t" +
                        item.descriere
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            //forme de organizare
            cfgFormeOrganizare = new CfgFormeOrganizare();
            if (cfgFormeOrganizare.formeOrganizare is not null)
            {
                foreach (var item in cfgFormeOrganizare.formeOrganizare)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.abreviere + "\t" +
                        item.descriere
                    );
                }
            } 
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            //legauri rudenie
            cfgLegaturiRudenie = new CfgLegaturiRudenie();
            if (cfgLegaturiRudenie.legaturiRudenie is not null)
            {
                foreach (var item in cfgLegaturiRudenie.legaturiRudenie)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.descriere
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            //modalitati de detinere 
            cfgModalitatiDetinere = new CfgModalitatiDetinere();
            if (cfgModalitatiDetinere.modalitatiDetinere is not null)
            {
                foreach (var item in cfgModalitatiDetinere.modalitatiDetinere)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.abreviere + "\t" +
                        item.descriere
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            //tipuri acte
            cfgTipuriActe = new CfgTipuriActe();
            if (cfgTipuriActe.tipuriActe is not null)
            {
                foreach (var item in cfgTipuriActe.tipuriActe)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.abreviere + "\t" +
                        item.descriere
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            //tipuri roluri
            cfgTipuriRoluri = new CfgTipuriRoluri();
            if (cfgTipuriRoluri.tipuriRoluri is not null)
            {
                foreach (var item in cfgTipuriRoluri.tipuriRoluri)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.descriere
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            // configurare capitol 2 a
            cfgCapitol2A = new CfgCapitol2A();
            if (cfgCapitol2A.randuriCapitol2A is not null)
            {
                foreach (var item in cfgCapitol2A.randuriCapitol2A)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.descriere + "\t" + 
                        item.formula + "\t" + 
                        item.codParinte + "\t" + 
                        item.semn
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            // configurare capitol 3
            cfgCapitol3 = new CfgCapitol3();
            if (cfgCapitol3.randuriCapitol3 is not null)
            {
                foreach (var item in cfgCapitol3.randuriCapitol3)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.descriere + "\t" + 
                        item.formula + "\t" + 
                        item.codParinte + "\t" + 
                        item.semn
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            // configurare capitol 4 a
            Console.Write("CfgCapitol4A");
            cfgCapitol4A = new CfgCapitol4A();
            if (cfgCapitol4A.randuriCapitol4A is not null)
            {
                foreach (var item in cfgCapitol4A.randuriCapitol4A)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.descriere + "\t" + 
                        item.formula + "\t" + 
                        item.codParinte + "\t" + 
                        item.semn
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            // configurare capitol 4 b 1
            Console.WriteLine("Capitol4B1.");
            cfgCapitol4B1 = new CfgCapitol4B1();
            if (cfgCapitol4B1.randuriCapitol4B1 is not null)
            {
                foreach (var item in cfgCapitol4B1.randuriCapitol4B1)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.descriere + "\t" + 
                        item.formula + "\t" + 
                        item.codParinte + "\t" + 
                        item.semn
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            // configurare capitol 4 b 2
            Console.WriteLine("Capitol4B2");
            cfgCapitol4B2 = new CfgCapitol4B2();
            if (cfgCapitol4B2.randuriCapitol4B2 is not null)
            {
                foreach (var item in cfgCapitol4B2.randuriCapitol4B2)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.descriere + "\t" + 
                        item.formula + "\t" + 
                        item.codParinte + "\t" + 
                        item.semn
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            // configurare capitol 4 c
            Console.WriteLine("Capitol4C");
            cfgCapitol4C = new CfgCapitol4C();
            if (cfgCapitol4C.randuriCapitol4C is not null)
            {
                foreach (var item in cfgCapitol4C.randuriCapitol4C)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.descriere + "\t" + 
                        item.formula + "\t" + 
                        item.codParinte + "\t" + 
                        item.semn
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
             // configurare capitol 5 a
            Console.WriteLine("Capitol5A");
            cfgCapitol5A = new CfgCapitol5A();
            if (cfgCapitol5A.randuriCapitol5A is not null)
            {
                foreach (var item in cfgCapitol5A.randuriCapitol5A)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.descriere + "\t" + 
                        item.formula + "\t" + 
                        item.codParinte + "\t" + 
                        item.semn
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            // configurare capitol 5 b
            Console.WriteLine("Capitol5B");
            cfgCapitol5B = new CfgCapitol5B();
            if (cfgCapitol5B.randuriCapitol5B is not null)
            {
                foreach (var item in cfgCapitol5B.randuriCapitol5B)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.descriere + "\t" + 
                        item.formula + "\t" + 
                        item.codParinte + "\t" + 
                        item.semn
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            // configurare capitol 5 c
            Console.WriteLine("Capitol5C");
            cfgCapitol5C = new CfgCapitol5C();
            if (cfgCapitol5C.randuriCapitol5C is not null)
            {
                foreach (var item in cfgCapitol5C.randuriCapitol5C)
                {
                    Console.WriteLine(
                        item.cod + "\t" + 
                        item.descriere + "\t" + 
                        item.formula + "\t" + 
                        item.codParinte + "\t" + 
                        item.semn
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            // adrese_roluri
            Console.WriteLine("adrese_roluri");
            adreseRoluri = new AdreseRoluri();
            if (adreseRoluri.roluri is not null)
            {
                foreach (var item in adreseRoluri.roluri)
                {
                    Console.WriteLine(
                        item.id + "\t" + 
                        item.cod_cfg_localitati + "\t" + 
                        item.tip + "\t" + 
                        item.volum + "\t" + 
                        item.pozitie + "\t" + 
                        item.id_adresa_rol + "\t" + 
                        item.cod_cfg_exploatatii + "\t" + 
                        item.id_persoana + "\t" + 
                        item.rol_impozite + "\t" + 
                        item.data_declaratie + "\t" + 
                        item.nr_inregistrare + "\t" + 
                        item.data_inregistrare + "\t" + 
                        item.semnat + "\t" + 
                        item.anulat + "\t" 
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            // persoane_fizice
            Console.WriteLine("persoane_fizice");
            persoaneFizice = new PersoaneFizice();
            if (persoaneFizice.persoaneFizice is not null)
            {
                foreach (var item in persoaneFizice.persoaneFizice)
                {
                    Console.WriteLine(
                        item.id + "\t" + 
                        item.nume + "\t" + 
                        item.initiala + "\t" + 
                        item.prenume + "\t" + 
                        item.cnp + ": " + PersoanaFizica.verificareCNP(item.cnp ?? "" ) + "\t" + 
                        item.sex + "\t" +
                        item.data_nasterii + "\t" + 
                        item.email + "\t" + 
                        item.telefon + "\t" + 
                        item.buletin + "\t" + 
                        item.id_adrese 
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
            // persoane_juridice
            Console.WriteLine("persoane_juridice");
            persoaneJuridice = new PersoaneJuridice();
            if (persoaneJuridice.persoaneJuridice is not null)
            {
                foreach (var item in persoaneJuridice.persoaneJuridice)
                {
                    Console.WriteLine(
                        item.id + "\t" + 
                        item.cod_forma_organizare + "\t" + 
                        item.denumire + "\t" + 
                        item.filiala + "\t" + 
                        item.cif + "\t" + 
                        item.cui + "\t" + 
                        item.registrul_comertului + "\t" + 
                        item.nume_reprezentant + "\t" + 
                        item.intiala_reprezenant + "\t" + 
                        item.prenume_reprezentant + "\t" + 
                        item.functia + "\t" + 
                        item.telefon + "\t" +
                        item.email + "\t" + 
                        item.id_adrese 
                    );
                }
            }
            Console.Write("apasa o tasta.");
            Console.ReadLine();
        }
    }
}
