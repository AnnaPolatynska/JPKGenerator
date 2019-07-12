using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Data.SqlClient;
using System.Xml.Serialization;


namespace nsJPK_Generator
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer(); // name space(using System.Timers;)  
                                   //connectionstring do połaczenia z bazą danych na sztywno
        //public static string _connString = (@"Data source=KRG411\SQLEXPRESS;database='vat';User id=vat;Password=95f)ek4n9!;");
        //public static string _connString = null;

        public static string _connString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionSQL"].ConnectionString;
        public static int _idJPK = -1; //nr JPK po którym generowane są dane do raportu.

        nsJPK_VAT.JPK jpkVAT = new nsJPK_VAT.JPK(); //struktura jpk

        private EventLog eventLog1;
        private int eventId = 1;

        public Service1()
        {
            InitializeComponent();


            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
        }//Service1
    

        protected override void OnStart(string[] args)
        {
            WriteToFile("Service is started at " + DateTime.Now);
            eventLog1.WriteEntry("In OnStart.");

            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            int timer_interval = int.Parse(System.Configuration.ConfigurationManager.AppSettings["timer_interval"].ToString());
            timer.Interval = timer_interval;
            timer.Enabled = true;
            timer.Start();
        }// OnStart

        protected override void OnStop()
        {  
            WriteToFile("Service is stopped at " + DateTime.Now);
            eventLog1.WriteEntry("In OnStop.");
            timer.Enabled = false;
        }//OnStop()

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            //Start do monitorowania
            SprawdzFor_Process();

            //WriteToFile("Sprawdzono Proces onTimer " + DateTime.Now);
            eventLog1.WriteEntry("Monitorowanie Systemu", EventLogEntryType.Information, eventId++);

        }//OnTimer(object sender, ElapsedEventArgs args)

        /*private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            //SprawdzFor_Process();
            WriteToFile("Service is onElapsedTime " + DateTime.Now);
        }*/

        /// <summary>
        /// Sprawdza tabelę dbo.app_jpk i gdy napotka true w kolumnie for_process to tworzy xml dla danej firmy. Po wygenerowaniu raportu zamienia na false(zrealizowane).
        /// </summary>
        public static void SprawdzFor_Process()
        {
            try
            {
                string connString = _connString;
                SqlConnection sqlConnection = new SqlConnection(connString);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM dbo.app_jpk WHERE for_process = 'true';", sqlConnection);
                DataSet dataSet = new DataSet("dbo.app_jpk");
                sqlDataAdapter.FillSchema(dataSet, SchemaType.Source, "dbo.app_jpk");
                sqlDataAdapter.Fill(dataSet, "dbo.app_jpk");
                DataTable dataTable = dataSet.Tables["dbo.app_jpk"];


                bool _for_process;
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    _for_process = bool.Parse(dataRow[@"for_process"].ToString());
                    if (_for_process == true)
                    {
                        _idJPK = int.Parse(dataRow["id"].ToString());

                        Service1 service1 = new Service1();
                        // tworzy plik xml o podanej nazwie w katalogu projektu
                        service1.WriteXMLToFile(_idJPK);

                        // zmienia po wygenerowaniu pliku status na zrealizowane
                        _for_process = false;
                        var sqlUpdate = "UPDATE dbo.app_jpk SET for_process = @for_process WHERE id=@id;";
                        using (SqlConnection sqlConnection1 = new SqlConnection(connString))
                        {
                            using (var command = new SqlCommand(sqlUpdate, sqlConnection1))
                            {
                                sqlConnection1.Open();
                                command.Parameters.AddWithValue("@id", _idJPK);
                                command.Parameters.AddWithValue("@for_process", _for_process);

                                int rows = command.ExecuteNonQuery();

                                _idJPK = int.Parse(dataRow["id"].ToString());
                            }
                        }
                    }
                    else { }
                    sqlConnection.Close();
                }//foreach
            }//try
            catch
            {
                Service1 service1 = new Service1();
                service1.WriteToFile("Nie można połączyć z bazą danych " + DateTime.Now);
            }
       
        }//SprawdzFor_Process()



        /// <summary>
        /// Zapisuje plik XML do pliku bez warunków
        /// </summary>
        public void WriteXMLToFile(int idJPK)
        {
            string name = System.Configuration.ConfigurationManager.AppSettings["filename"].ToString();
            string filename = name + idJPK + ".xml";
            //string path = AppDomain.CurrentDomain.BaseDirectory + "\\JPK";

            //int.Parse(System.Configuration.ConfigurationManager.AppSettings["timer_interval"].ToString());
            string path = AppDomain.CurrentDomain.BaseDirectory + System.Configuration.ConfigurationManager.AppSettings["folder"].ToString();
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\JPK\\" + DateTime.Now.ToShortDateString().Replace('/', '_')+"_"+ filename;
           // string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\JPK\\" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + "_" + filename;
            if (!File.Exists(filepath))
            {
                CreateXML(filepath);
                WriteToFile("Wygenerowano JPK nr " + filename + " " + DateTime.Now);
            }
            else
            {
                CreateXML(filename);
            }
           
            
        }// WriteXMLToFile(string Message)

        /// <summary>
        /// Tworzy XML.
        /// </summary>
        /// <param name="filename"></param>
        private void CreateXML(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(nsJPK_VAT.JPK)); // sposób formatowania to JPK
            TextWriter writer = new System.IO.StreamWriter(filename);

            FillNaglowek(); // Wypełnia strukturę XML naglówka
            FillSprzedaz(); // wypełnia strukturę XML sprzedaży
            FillZakup(); //Wypełnia strukturę XML zakupu.

            //zapis do pliku XmlSerializerem
            serializer.Serialize(writer, jpkVAT);//Serializacja

            writer.Flush();
            writer.Close();// sprzątnięcie i zamknięcie

        }// CreateXML(string filename)


        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\TEST";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\TEST\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Stworzenie pliku do zapisu.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }// WriteToFile(string Message)


        
     

        

        
        /// <summary>
        /// Wypełnia wiersze nagłówka
        /// </summary>
        private void FillNaglowek()
        {
            // PRZYPISANIE WARTOŚCI ZMIENNYM
            //Wiersze nagłówka -> KodFormularza, WariantFormularza, CelZlozenia, DataWytworzeniaJPK, DataOd, DataDo, NazwaSystemu
            sbyte _WariantFormularza = 3;//sbyte
            string _CelZlozenia = "0"; //string 1 - nowa deklaracja, 2 - korekta deklaracji

            DateTime _DataWytworzeniaJPK = DateTime.Now; //Parse(p.GetDataWytworzenia().ToString());// "od 2016-07-01T00:00:00Z do 2030-12-31T23:59:59Z"
            DateTime _DataOd = DateTime.Parse("2018-11-01"); // "2016-07-01" do 2030-01-01"
            DateTime _DataDo = DateTime.Parse("2019-01-01");  // "2006-01-01" do 2030-01-01"
            string _NazwaSystemu = "Token1";// (1-240) Nazwa systemu, z którego pochodzą dane

            //Wiersze Podmiot1 -> NIP, PelnaNazwa, Email
            string _NIP = GetNIP().ToString();//"123456" NIP z tabeli dbo.app_company
            string _PelnaNazwa = GetPelna_Nazwa().ToString(); //od 1 do 240 znaków
            //string _Email = p.GetMail().ToString(); //minOccurs="0" //od 5 do 50 znaków zawierająca @
           
            string _Email = System.Configuration.ConfigurationManager.AppSettings["email"].ToString(); // z pliku konfiguracyjnego
            //string _Email = "jakiś@mail.pl"; 

            //STRUKTURA XML
            var myNaglowek = new nsJPK_VAT.TNaglowek();

            //nagłówek -> KodFormularza, WariantFormularza, CelZlozenia, DataWytworzeniaJPK, DataOd, DataDo, NazwaSystemu
            myNaglowek.WariantFormularza = _WariantFormularza;
            myNaglowek.CelZlozenia = _CelZlozenia;
            myNaglowek.DataWytworzeniaJPK = _DataWytworzeniaJPK;
            myNaglowek.DataOd = _DataOd;
            myNaglowek.DataDo = _DataDo;
            myNaglowek.NazwaSystemu = _NazwaSystemu;

            //kod formulatrza ->  <KodFormularza kodSystemowy="JPK_VAT (3)" wersjaSchemy="1-1">JPK_VAT</KodFormularza>
            var myKodFormularza = new nsJPK_VAT.TNaglowekKodFormularza();
            _ = myKodFormularza.kodSystemowy; //wartośc z kodu automatycznego "JPK_VAT (3)"
            _ = myKodFormularza.wersjaSchemy; // wartość z kodu automatycznego "1-1"
            myKodFormularza.Value = nsJPK_VAT.TKodFormularza.JPK_VAT;

            myNaglowek.KodFormularza = myKodFormularza;

            // Podmiot1 -> NIP, PelnaNazwa, Email
            var myPodmiot1 = new nsJPK_VAT.JPKPodmiot1();
            myPodmiot1.NIP = _NIP;
            myPodmiot1.PelnaNazwa = _PelnaNazwa;
            myPodmiot1.Email = _Email;

            jpkVAT.Naglowek = myNaglowek;
            jpkVAT.Podmiot1 = myPodmiot1;

        }//FillNaglowek()


        /// <summary>
        /// Wypełnia rekordy sprzedaży (po odczytanym _idJPK = "for_JPK_id") z SQL do XML używając widoku SQL zamieniającego nulle na 0 ->[JPK_SF].
        /// </summary>
        private void FillSprzedaz()
        {
            string connString = _connString;
            SqlConnection sqlConnection = new SqlConnection(connString);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * from [JPK_SF] WHERE for_JPK_id = " + _idJPK, sqlConnection);
            DataSet dataSet = new DataSet("dbo.app_jpksales");

            sqlDataAdapter.FillSchema(dataSet, SchemaType.Source, "dbo.app_jpksales");
            sqlDataAdapter.Fill(dataSet, "dbo.app_jpksales");

            DataTable dt = dataSet.Tables["dbo.app_jpksales"];
            nsJPK_VAT.JPKSprzedazWiersz[] sprzedazWiersze = new nsJPK_VAT.JPKSprzedazWiersz[0];

            decimal zliczSprzedaz = 0;

            foreach (DataRow row in dt.Rows)
            {
                Array.Resize(ref sprzedazWiersze, sprzedazWiersze.Length + 1);

                var sprzedaz = new nsJPK_VAT.JPKSprzedazWiersz();

                sprzedaz.LpSprzedazy = row[@"LpSprzedazy"].ToString();  // liczba porzadkowa wiersza sprzedaży
                sprzedaz.NrKontrahenta = row[@"NrKontrahenta"].ToString();
                sprzedaz.NazwaKontrahenta = row[@"NazwaKontrahenta"].ToString();
                sprzedaz.AdresKontrahenta = row[@"AdresKontrahenta"].ToString();
                sprzedaz.DowodSprzedazy = row[@"DowodSprzedazy"].ToString();

                DateTime _dataWystwienia = DateTime.Parse(row[@"DataWystawienia"].ToString());
                sprzedaz.DataWystawienia = _dataWystwienia;

                //Data sprzedaży, o ile jest określona i różni się od daty wystawienia dowodu sprzedaży. W przeciwnym przypadku - pole puste
                DateTime _dataSprzedazy = DateTime.Parse(row[@"DataSprzedazy"].ToString());
                sprzedaz.DataSprzedazy = _dataSprzedazy;
                if (sprzedaz.DataSprzedazy != sprzedaz.DataWystawienia)
                {
                    sprzedaz.DataSprzedazySpecified = true;
                }
                else
                {
                    sprzedaz.DataSprzedazySpecified = false;
                } //DataSprzedazySpecified

                // K_10

                sprzedaz.K_10 = decimal.Parse(row[@"K_10"].ToString()); //Kwota netto - Dostawa towarów oraz świadczenie usług na terytorium kraju, zwolnione od podatku

                if (sprzedaz.K_10 != 0)
                {
                    sprzedaz.K_10Specified = true;
                }
                else
                {
                    sprzedaz.K_10Specified = false;
                }//K_10Specified

                //K_11
                try
                {
                    sprzedaz.K_11 = Decimal.Parse(row[@"K_11"].ToString()); //Kwota netto - Dostawa towarów oraz świadczenie usług poza terytorium kraju

                }
                catch { sprzedaz.K_11 = 0.00M; }
                if (sprzedaz.K_11 != 0)
                {
                    sprzedaz.K_11Specified = true;
                }
                else
                {
                    sprzedaz.K_11Specified = false;
                }//K_11Specified

                //K_12
                try
                {
                    sprzedaz.K_12 = Decimal.Parse(row[@"K_12"].ToString()); //Kwota netto - w tym świadczenie usług, o których mowa w art. 100 ust. 1 pkt 4 ustawy
                }
                catch
                {
                    sprzedaz.K_12 = 0.00M;
                }
                if (sprzedaz.K_12 != 0)
                {
                    sprzedaz.K_12Specified = true;
                }
                else
                {
                    sprzedaz.K_12Specified = false;
                } //K_12Specified 

                //K_13
                try
                {
                    sprzedaz.K_13 = Decimal.Parse(row[@"K_13"].ToString()); //Kwota netto - Dostawa towarów oraz świadczenie usług na terytorium kraju, opodatkowane stawką 0%
                }
                catch
                {
                    sprzedaz.K_13 = 0.00M;
                }
                if (sprzedaz.K_13 != 0)
                {
                    sprzedaz.K_13Specified = true;
                }
                else
                {
                    sprzedaz.K_13Specified = false;
                }// K_13Specified

                //K14
                try
                {
                    sprzedaz.K_14 = Decimal.Parse(row[@"K_14"].ToString()); //Kwota netto - w tym dostawa towarów, o której mowa w art. 129 ustawy
                }
                catch
                {
                    sprzedaz.K_14 = 0.00M;
                }
                if (sprzedaz.K_14 != 0)
                {
                    sprzedaz.K_14Specified = true;
                }
                else
                {
                    sprzedaz.K_14Specified = false;
                }// K_14Specified

                sprzedaz.K_15 = Decimal.Parse(row[@"K_15"].ToString()); // Kwota netto - Dostawa towarów oraz świadczenie usług na terytorium kraju, opodatkowane stawką 5%

                sprzedaz.K_16 = Decimal.Parse(row[@"K_16"].ToString()); //Kwota podatku należnego - Dostawa towarów oraz świadczenie usług na terytorium kraju, opodatkowane stawką 5%

                sprzedaz.K_17 = Decimal.Parse(row[@"K_17"].ToString()); //Kwota netto - Dostawa towarów oraz świadczenie usług na terytorium kraju, opodatkowane stawką 7% albo 8%

                sprzedaz.K_18 = Decimal.Parse(row[@"K_18"].ToString()); //Kwota podatku należnego - Dostawa towarów oraz świadczenie usług na terytorium kraju, opodatkowane stawką 7% albo 8%

                sprzedaz.K_19 = Decimal.Parse(row[@"K_19"].ToString()); //Kwota netto - Dostawa towarów oraz świadczenie usług na terytorium kraju, opodatkowane stawką 22% albo 23%

                sprzedaz.K_20 = Decimal.Parse(row[@"K_20"].ToString()); //Kwota podatku należnego - Dostawa towarów oraz świadczenie usług na terytorium kraju, opodatkowane stawką 22% albo 23%

                sprzedaz.K_21 = Decimal.Parse(row[@"K_21"].ToString());//Kwota netto - Wewnątrzwspólnotowa dostawa towarów
                if (sprzedaz.K_21 != 10)
                {
                    sprzedaz.K_21Specified = true;
                }
                else
                {
                    sprzedaz.K_21Specified = false;
                }//K_21Specified

                //K22
                sprzedaz.K_22 = Decimal.Parse(row[@"K_22"].ToString()); // Kwota netto - Eksport towarów
                if (sprzedaz.K_22 != 0)
                {
                    sprzedaz.K_22Specified = true;
                }
                else
                {
                    sprzedaz.K_22Specified = false;
                } //sprzedaz.K_22Specified

                //K23
                sprzedaz.K_23 = Decimal.Parse(row[@"K_23"].ToString());
                sprzedaz.K_24 = Decimal.Parse(row[@"K_24"].ToString());//Kwota podatku należnego - Wewnątrzwspólnotowe nabycie towarów

                //K25
                sprzedaz.K_25 = Decimal.Parse(row[@"K_25"].ToString()); // Kwota netto - Import towarów podlegający rozliczeniu zgodnie z art. 33a ustawy

                //K26
                sprzedaz.K_26 = Decimal.Parse(row[@"K_26"].ToString()); // Kwota podatku należnego - Import towarów podlegający rozliczeniu zgodnie z art. 33a ustawy

                //27
                sprzedaz.K_27 = Decimal.Parse(row[@"K_27"].ToString());// Kwota netto - Import usług z wyłączeniem usług nabywanych od podatników podatku od wartości dodanej, do których stosuje się art. 28b ustawy

                //K28
                sprzedaz.K_28 = Decimal.Parse(row[@"K_28"].ToString()); // Kwota podatku należnego - Import usług z wyłączeniem usług nabywanych od podatników podatku od wartości dodanej, do których stosuje się art. 28b ustawy

                //K29
                sprzedaz.K_29 = Decimal.Parse(row[@"K_29"].ToString());// Kwota netto - Import usług z wyłączeniem usług nabywanych od podatników podatku od wartości dodanej, do których stosuje się art. 28b ustawy

                //K30
                sprzedaz.K_30 = Decimal.Parse(row[@"K_30"].ToString());//Kwota podatku należnego - Import usług z wyłączeniem usług nabywanych od podatników podatku od wartości dodanej, do których stosuje się art. 28b ustawy

                //K31
                sprzedaz.K_31 = Decimal.Parse(row[@"K_31"].ToString()); //Kwota netto - Dostawa towarów oraz świadczenie usług, dla których podatnikiem jest nabywca zgodnie z art. 17 ust. 1 pkt 7 lub 8 ustawy (wypełnia dostawca)
                if (sprzedaz.K_31 != 0)
                {
                    sprzedaz.K_31Specified = true;
                }
                else
                {
                    sprzedaz.K_31Specified = false;
                }// K_31Specified

                //K32
                sprzedaz.K_32 = Decimal.Parse(row[@"K_32"].ToString()); ; //Kwota netto - Dostawa towarów, dla których podatnikiem jest nabywca zgodnie z art. 17 ust. 1 pkt 5 ustawy (wypełnia nabywca)

                //K33
                sprzedaz.K_33 = Decimal.Parse(row[@"K_33"].ToString());//Kwota podatku należnego - Dostawa towarów, dla których podatnikiem jest nabywca zgodnie z art. 17 ust. 1 pkt 5 ustawy (wypełnia nabywca)

                //K_34
                sprzedaz.K_34 = Decimal.Parse(row[@"K_34"].ToString()); //Kwota netto - Dostawa towarów oraz świadczenie usług, dla których podatnikiem jest nabywca zgodnie z art. 17 ust. 1 pkt 7 lub 8 ustawy (wypełnia nabywca)

                //K_35
                sprzedaz.K_35 = Decimal.Parse(row[@"K_35"].ToString()); // Kwota podatku należnego - Dostawa towarów oraz świadczenie usług, dla których podatnikiem jest nabywca zgodnie z art. 17 ust. 1 pkt 7 lub 8 ustawy (wypełnia nabywca)

                //K_36
                sprzedaz.K_36 = Decimal.Parse(row[@"K_36"].ToString()); //Kwota podatku należnego od towarów j usług objętych spisem z natury, o którym mowa w art. 14 ust. 5 ustawy
                if (sprzedaz.K_36 != 0)
                {
                    sprzedaz.K_36Specified = true;
                }
                else
                {
                    sprzedaz.K_36Specified = false;
                }// K_36Specified

                //K_37
                sprzedaz.K_37 = Decimal.Parse(row[@"K_37"].ToString()); //Zwrot odliczonej lub zwróconej kwoty wydatkowanej na zakup kas rejestrujących, o którym mowa w art. 111 ust. 6 ustawy
                if (sprzedaz.K_37 != 0)
                {
                    sprzedaz.K_37Specified = true;
                }
                else
                {
                    sprzedaz.K_37Specified = false;
                }// K_37Specified

                //K_38
                sprzedaz.K_38 = Decimal.Parse(row[@"K_38"].ToString()); //Kwota podatku należnego od wewnątrzwspólnotowego nabycia środków transportu, wykazanego w elemencie K_24, podlegająca wpłacie w terminie, o którym mowa w art. 103 ust. 3, w związku z ust. 4 ustawy
                if (sprzedaz.K_38 != 0)
                {
                    sprzedaz.K_38Specified = true;
                }
                else
                {
                    sprzedaz.K_38Specified = false;
                }// K_38Specified

                // K_39
                sprzedaz.K_39 = Decimal.Parse(row[@"K_39"].ToString()); // Kwota podatku od wewnątrzwspólnotowego nabycia paliw silnikowych, podlegająca wpłacie w terminach, o których mowa w art. 103 ust. 5a j 5b ustawy

                if (sprzedaz.K_39 != 0)
                {
                    sprzedaz.K_39Specified = true;
                }
                else
                {
                    sprzedaz.K_39Specified = false;
                }// K_39Specified

                sprzedazWiersze[sprzedazWiersze.Length - 1] = sprzedaz;

                //suma kwot z elementów K_16, K_18, K_20, K_24, K_26, K_28, K_30, K_33, K_35, K_36 j K_37 pomniejszona o kwotę z elementów K_38 j K_39
                zliczSprzedaz += sprzedaz.K_16 + sprzedaz.K_18 + sprzedaz.K_20 + sprzedaz.K_24 + sprzedaz.K_26 + sprzedaz.K_28 + sprzedaz.K_30 + sprzedaz.K_33 + sprzedaz.K_35 + sprzedaz.K_36 + sprzedaz.K_37 - sprzedaz.K_38 - sprzedaz.K_39;

            }//foreach

            jpkVAT.SprzedazWiersz = sprzedazWiersze;

            //Sumy kontrolne dla ewidencji sprzedaży VAT
            // SprzedazCtrl-> LiczbaWierszySprzedazy, PodatekNalezny
            var sprzedazCtrl = new nsJPK_VAT.JPKSprzedazCtrl();


            sprzedazCtrl.LiczbaWierszySprzedazy = (sprzedazWiersze.Length).ToString(); //Liczba wierszy ewidencji sprzedaży, w okresie którego dotyczy JPK

            //Podatek należny wg ewidencji sprzedaży w okresie, którego dotyczy JPK - 
            //suma kwot z elementów K_16, K_18, K_20, K_24, K_26, K_28, K_30, K_33, K_35, K_36 j K_37 pomniejszona o kwotę z elementów K_38 j K_39

            sprzedazCtrl.PodatekNalezny = zliczSprzedaz;


            jpkVAT.SprzedazCtrl = sprzedazCtrl;

            //data
            //dt.Columns.Add();

        }//FillSprzedaz()

        /// <summary>
        /// Wypełnia rekordy zakupu (po odczytanym _idJPK = "for_JPK_id") uzywając widoku SQL zamieniającego nulle na 0 ->[JPK_PURCHASES]
        /// </summary>
        private void FillZakup()
        {
            string connString = _connString;
            SqlConnection sqlConnection = new SqlConnection(connString);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * from [JPK_PURCHASES] WHERE for_JPK_id = " + _idJPK, sqlConnection);
            DataSet dataSet = new DataSet("dbo.app_jpkpurchases");

            sqlDataAdapter.FillSchema(dataSet, SchemaType.Source, "dbo.app_jpkpurchases");
            sqlDataAdapter.Fill(dataSet, "dbo.app_jpkpurchases");

            DataTable dt = dataSet.Tables["dbo.app_jpkpurchases"];

            nsJPK_VAT.JPKZakupWiersz[] zakupWiersze = new nsJPK_VAT.JPKZakupWiersz[0];

            decimal zliczZakup = 0;

            foreach (DataRow row in dt.Rows)
            {
                Array.Resize(ref zakupWiersze, zakupWiersze.Length + 1);
                var zakup = new nsJPK_VAT.JPKZakupWiersz();

                zakup.LpZakupu = row[@"LpZakupu"].ToString();//Lp. wiersza ewidencji zakupu VAT
                zakup.NrDostawcy = row[@"NrDostawcy"].ToString(); //Numer, za pomocą którego dostawca (kontrahent) jest zidentyfikowany na potrzeby podatku lub podatku od wartości dodanej
                zakup.NazwaDostawcy = row[@"NazwaDostawcy"].ToString();
                zakup.AdresDostawcy = row[@"AdresDostawcy"].ToString();
                zakup.DowodZakupu = row[@"DowodZakupu"].ToString();

                zakup.DataZakupu = DateTime.Parse(row[@"DataZakupu"].ToString()); //ToDateTime();

                //DateTime _dataWplywu = DateTime.Parse("2019-02-01");// Data wpływu dowodu zakupu
                DateTime _dataWplywu = DateTime.Parse(row[@"dataWplywu"].ToString());
                zakup.DataWplywu = _dataWplywu;

                DateTime _minData = DateTime.Parse("1000-01-01"); //Data minimalna bo nie może być zero.

                if (zakup.DataWplywu > _minData)
                {
                    zakup.DataWplywuSpecified = true;
                }
                else
                {
                    zakup.DataWplywuSpecified = false;
                }//DataWplywuSpecified

                zakup.K_43 = Decimal.Parse(row[@"K_43"].ToString()); //Kwota netto - Nabycie towarów i usług zaliczanych u podatnika do środków trwałych
                zakup.K_44 = Decimal.Parse(row[@"K_44"].ToString());  //Kwota podatku naliczonego - Nabycie towarów i usług zaliczanych u podatnika do środków trwałych
                zakup.K_45 = Decimal.Parse(row[@"K_45"].ToString()); //Kwota netto - Nabycie towarów i usług pozostałych
                zakup.K_46 = Decimal.Parse(row[@"K_46"].ToString()); //Kwota podatku naliczonego - Nabycie towarów i usług pozostałych

                zakup.K_47 = Decimal.Parse(row[@"K_47"].ToString()); //Korekta podatku naliczonego od nabycia środków trwałych
                if (zakup.K_47 != 0)
                {
                    zakup.K_47Specified = true;
                }
                else
                {
                    zakup.K_47Specified = false;
                }

                zakup.K_48 = Decimal.Parse(row[@"K_48"].ToString()); //Korekta podatku naliczonego od pozostałych nabyć
                if (zakup.K_48 != 0)
                {
                    zakup.K_48Specified = true;
                }
                else
                {
                    zakup.K_48Specified = false;
                }

                zakup.K_49 = Decimal.Parse(row[@"K_49"].ToString()); //Korekta podatku naliczonego, o której mowa w art. 89b ust. 1 ustawy
                if (zakup.K_49 != 0)
                {
                    zakup.K_49Specified = true;
                }
                else
                {
                    zakup.K_49Specified = false;
                }

                zakup.K_50 = Decimal.Parse(row[@"K_50"].ToString()); //Korekta podatku naliczonego, o której mowa w art. 89b ust. 4 ustawy
                if (zakup.K_50 != 0)
                {
                    zakup.K_50Specified = true;
                }
                else
                {
                    zakup.K_50Specified = false;
                }

                zakupWiersze[zakupWiersze.Length - 1] = zakup;

                //Razem kwota podatku naliczonego do odliczenia - suma kwot z elementów K_44, K_46, K_47, K_48, K_49 i K_50
                zliczZakup += zakup.K_44 + zakup.K_46 + zakup.K_47 + zakup.K_48 + zakup.K_49 + zakup.K_50;
            }// for zakup

            jpkVAT.ZakupWiersz = zakupWiersze;

            // ZakupCtrl -> LiczbaWierszyZakupow, PodatekNaliczony
            var zakupCtrl = new nsJPK_VAT.JPKZakupCtrl();
            zakupCtrl.LiczbaWierszyZakupow = (zakupWiersze.Length).ToString(); // liczba wierszy zakupów.
            //Razem kwota podatku naliczonego do odliczenia - suma kwot z elementów K_44, K_46, K_47, K_48, K_49 i K_50
            zakupCtrl.PodatekNaliczony = zliczZakup;

            jpkVAT.ZakupCtrl = zakupCtrl;

            sqlConnection.Close();

        }// FillZakup()

        /// <summary>
        /// Pobiera NIP firmy z dbo.app_company po id jpk z tabeli app_jpk
        /// </summary>
        /// <returns>NIP firmy</returns>
        private string GetNIP()
        {
            DataTable dataTable = new DataTable();

            string connString = _connString;
            SqlConnection sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();
            SqlCommand query = new SqlCommand("SELECT * FROM[vat].[dbo].[app_company] JOIN [vat].[dbo].[app_jpk] ON[vat].[dbo].[app_company].id = [vat].[dbo].[app_jpk].for_company_id WHERE [vat].[dbo].[app_jpk].id = " + _idJPK.ToString() + ";", sqlConnection);

            SqlDataReader queryResult = query.ExecuteReader();
            dataTable.Load(queryResult);

            DataRow dataRow = dataTable.Rows[0];
            string _NIP = dataRow["company_nip"].ToString();
            sqlConnection.Close();
            return _NIP;
        }//GetNIP

        /// <summary>
        /// Pobiera nazwę firmy z dbo.app_company po id jpk z tabeli app_jpk
        /// </summary>
        /// <returns>Pełną nazwę firmy</returns>
        private string GetPelna_Nazwa()
        {
            DataTable dataTable = new DataTable();

            string connString = _connString;
            SqlConnection sqlConnection = new SqlConnection(connString);
            sqlConnection.Open();
            SqlCommand query = new SqlCommand("SELECT * FROM[vat].[dbo].[app_company] JOIN [vat].[dbo].[app_jpk] ON[vat].[dbo].[app_company].id = [vat].[dbo].[app_jpk].for_company_id WHERE [vat].[dbo].[app_jpk].id = " + _idJPK.ToString() + ";", sqlConnection);

            SqlDataReader queryResult = query.ExecuteReader();
            dataTable.Load(queryResult);
            DataRow dataRow = dataTable.Rows[0];
            string _Pelna_Nazwa = dataRow["company_name"].ToString();
            sqlConnection.Close();
            return _Pelna_Nazwa;
        }//GetPelna_Nazwa()

        // Zapytanie do tabeli app_jpksales
        private void SelectQuery_app_jpksales()
        {
            string connString = _connString;
            SqlConnection sqlConnection = new SqlConnection(connString); //nawiązanie połączenia z connStringa z bazą Vat tabela dbo.app_jpksales_TEST


            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT * FROM dbo.app_jpksalesTEST;", sqlConnection);
            DataSet dataSet = new DataSet("dbo.app_jpksalesTEST");

            sqlDataAdapter.FillSchema(dataSet, SchemaType.Source, "dbo.app_jpksalesTEST");
            sqlDataAdapter.Fill(dataSet, "dbo.app_jpksalesTEST");

            DataTable dataTable = dataSet.Tables["dbo.app_jpksalesTEST"];

            foreach (DataRow dataRow in dataTable.Rows)
            {
                Console.WriteLine("{0} {1} {2} {3} {4} {5}", dataRow["NrKontrahenta"].ToString(), dataRow["NazwaKontrahenta"].ToString(), dataRow["AdresKontrahenta"].ToString(), dataRow["DowodSprzedazy"].ToString(), dataRow["DataWystawienia"].ToString(), dataRow["DataSprzedazy"].ToString());

            };
            sqlConnection.Close();

        }//SelectQuery_app_jpksalesTEST()



    }//Service1 : ServiceBase
}//ns nsJPK_Generator
