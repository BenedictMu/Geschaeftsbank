using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Konten
{
    public class Konto
    {
        // 1.1 Attribute
        private string Inhaber;
        public int Kontonummer;
        private double Kontostand;
        private double Kreditlinie;
		
		//*** Team 2 Start
		private double Guthabenzinssatz;
        private double Dispozinssatz; 
        private List<double> Zinssaetze = new List<double>(); 
		private List<double> Zeitraum = new List<double>();

        private double kvr;
        private double zins;
        private double knr;						
        private double zeit;
        private double zwischenergebnis;
		
		//*** Team 2 End

        //*** Team 3 Start
        private bool DauerauftragAktiv;
        private double DauerauftragBetrag;
        private int DauerauftragZielkonto; //[i]te Stelle in Liste "KontoListe"
        private int DauerauftragZielkontonummer; //Kontonummer wie d. Kunde sie sieht
        private DateTime DauerauftragErstellungDatum; //Datum der DA-Erstellung (Übergabe der Variable "jetzt" aus Main)
        private int DauerauftragWiederholungen;
        private int DauerauftragTag;
        private List<DateTime> DauerauftragDatum = new List<DateTime>();
        //*** Team 3 End

        //*** Team 4 Start
        private bool Abbuchen = false;
        private int Zielkontonummer;
        private double Lastschriftbetrag;
        //*** Team 4 End


        //1.2 Assoziationen
        // ... kommt nach Vererbung


        //1.3 lokale Variablen
        //*** Team 1 Start
        string sKontobewegung;
        List<string> Kontobewegungen = new List<string>();
        //*** Team 1 End


        //1.4 statische Variablen
        static int Kontozaehler = 0;

        static int anzahlVergebenerKontonummern;
        static int randomKontonummer;
        static Random zufall = new Random();
        static List<int> listeVergebenerKontonummern = new List<int>();
        static int stellenzahlVergebeneKontonummer = anzahlVergebenerKontonummern.ToString().Length;

        static int MinZufallszahl = (int)Math.Pow(10, stellenzahlVergebeneKontonummer);
        static int MaxZufallszahl = (int)Math.Pow(10, stellenzahlVergebeneKontonummer + 1);


        // 2.1 Konstruktor(en)

        public Konto(string _name)
        {
            setInhaber(_name);
            setKontonummer(nextnewKontonummer());
            setKontostand(100);
            setKreditlinie(-20);
            Kontozaehler++;
        }

        //*** Team 4 Start
        public Konto(int _kontonummer, int _zielkontonummer, double _lastschriftbetrag)
        {
            setKontonummer(_kontonummer);
            setZielkontonummer(_zielkontonummer);
            setLastschriftbetrag(_lastschriftbetrag);
        }
        //*** Team 4 End

        // 2.2 getter und setter

        public string getInhaber()
        {
            return Inhaber;
        }

        public void setInhaber(string _inhaber)
        {
            bool ungueltigerName = true;
            do
            {
                if (_inhaber.Length != 0)
                {
                    if (!(_inhaber.Substring(0, 1).Equals(" ")))
                    {
                        Console.Write(Inhaber);
                        Inhaber = _inhaber;
                        ungueltigerName = false;
                    }
                    else
                    {
                        Console.Write("Bitte geben Sie einen gültigen Inhabernamen ein: ");
                        _inhaber = Console.ReadLine();
                        Console.WriteLine(_inhaber);
                        ungueltigerName = true;
                    }
                }
                else
                {
                    Console.Write("Bitte geben Sie einen gültigen Inhabernamen ein: ");
                    _inhaber = Console.ReadLine();
                    ungueltigerName = true;
                }
            } while (ungueltigerName);
        }
        public double getKontostand()
        {
            return Kontostand;
        }

        private void setKontostand(double _kontostand)
        {
            Kontostand = _kontostand;
        }

        public double getKreditlinie()
        {
            return Kreditlinie;
        }

        private void setKreditlinie(double _kreditlinie)
        {
            Kreditlinie = _kreditlinie;
        }

        public int getKontonummer()
        {
            return Kontonummer;
        }

        private void setKontonummer(int _kontonummer)
        {
            Kontonummer = _kontonummer;
        }

        //*** Team 1 Start
        public int getKontobewegungsanzahl()
        {
            return Kontobewegungen.Count;
        }
        public void getKontobewegungen(int i)
        {

            Console.WriteLine(Kontobewegungen[i]);
        }
        public string getsKontobewegungen(int i)
        {
			return Kontobewegungen[i];
        }

        //*** Team 1 End
		
		//*** Team 2 Start
		public double getGuthabenszinssatz()
        {                                                                                           
            return Guthabenzinssatz;                                                                
        }                                                                                           
                                                                                                    
        public void setGuthabenszinssatz(double _guthabenzinssatz)                                  
        {                                                                                           
            bool ungueltigerGuthabenszinssatz = true;                                               
            do                                                                                      
            {                                                                                       
                if (_guthabenzinssatz > 0 && _guthabenzinssatz <= 2)                                
                {                                                                                   
                    Guthabenzinssatz = _guthabenzinssatz;                                           
                    ungueltigerGuthabenszinssatz = false;                                           
                }                                                                                   
                else if (_guthabenzinssatz <= 0)                                                    
                {                                                                                   
                    Console.WriteLine("Der von Ihnen eingegebene Guthabenszinssatz ist zu niedrig");
                    Console.Write("Geben Sie einen Guthabenszinssatz zwischen 0 und 2 ein: ");      
                    _guthabenzinssatz = Convert.ToDouble(Console.ReadLine());                                     
                }                                                                                   
                else                                                                                
                {                                                                                   
                    Console.WriteLine("Der von Ihnen eingegebene Guthabenszinssatz ist zu hoch");   
                    Console.Write("Geben Sie einen Guthabenszinssatz zwischen 0 und 2 ein: ");      
                    _guthabenzinssatz = Convert.ToDouble(Console.ReadLine());                       
                }
            } while (ungueltigerGuthabenszinssatz);                                             
        }                                                        
                                                                 
                                                                 
        public double getDispozinssatz()                         
        {                                                        
            return Dispozinssatz;                                
        }                                                        
                                                                 
                                                                 
        public void setDispozinssatz(double _Dispozinssatz)      
        {                                                        
            bool ungueltigerDispozinssatz = true;                
            do                                                   
            {                                                    
                if (_Dispozinssatz >= 4 && _Dispozinssatz <= 14) 
                {                                                
                    Dispozinssatz = _Dispozinssatz;              
                    ungueltigerDispozinssatz = false;            
                }                                                
                else if (_Dispozinssatz < 4)                     
                {                                                                               
                    Console.WriteLine("Der von Ihnen eingegebene Dispozinssatz ist zu niedrig");
                    Console.Write("Geben Sie einen Dispozinssatz zwischen 4 und 14 ein: ");     
                    _Dispozinssatz = Convert.ToDouble(Console.ReadLine());                      
                }                                                                               
                else                                                                            
                {                                                                               
                    Console.WriteLine("Der von Ihnen eingegebene Dispozinssatz ist zu hoch");   
                    Console.Write("Geben Sie einen Dispozinssatz zwischen 4 und 14 ein: ");     
                    _Dispozinssatz = Convert.ToDouble(Console.ReadLine());                      
                }                                                                               
            } while (ungueltigerDispozinssatz);                                                 
        } 
		
		public int getLaenge()                         
        {                                              
            return Zinssaetze.Count;                                                                
        }                                              
											 

        public void Ausgabezinsen()                                                                             
        {                                                                                                       
            for (int i = 0; i < Zinssaetze.Count; i++)                                                          
            {                                                                                                   
                Console.WriteLine("Zeitraum:\t" + Zeitraum[i] + " Tage\t\tZinsen:\t" + Zinssaetze[i] + " Euro");
            }                                                                                                   
        }       
		
		//*** Team 2 End


        //*** Team 3 Start
        public bool getDauerauftragAktiv()
        {
            return DauerauftragAktiv;
        }

        private void setDauerauftragAktiv(bool _wert)
        {
            DauerauftragAktiv = _wert;
        }

        public double getDauerauftragBetrag()
        {
            return DauerauftragBetrag;
        }

        private void setDauerauftragBetrag(double _betrag)
        {
            DauerauftragBetrag = _betrag;
        }

        public int getDauerauftragZielkonto()
        {
            return DauerauftragZielkonto;
        }

        private void setDauerauftragZielkonto(int _konto)
        {
            DauerauftragZielkonto = _konto;
        }

        public int getDauerauftragWiederholungen()
        {
            return DauerauftragWiederholungen;
        }

        private void setDauerauftragWiederholungen(int _wiederholungen)
        {
            DauerauftragWiederholungen = _wiederholungen;
            if (DauerauftragWiederholungen <= 0) //Sind Wiederholungen
            {
                setDauerauftragAktiv(false);
            }
        }

        public int getDauerauftragTag()
        {
            return DauerauftragTag;
        }

        private void setDauerauftragTag(int _tag)
        {
            DauerauftragTag = _tag;
        }

        public DateTime getDauerauftragErstellungDatum()
        {
            return DauerauftragErstellungDatum;
        }

        private void setDauerauftragErstellungDatum(DateTime _datum)
        {
            DauerauftragErstellungDatum = _datum;
        }

        public int getDauerauftragZielkontonummer()
        {
            return DauerauftragZielkontonummer;
        }

        private void setDauerauftragZielkontonummer(int _dauerauftragZielkontonummer)
        {
            DauerauftragZielkontonummer = _dauerauftragZielkontonummer;
        }
        //*** Team 3 End

        //*** Team 4 Start
        private void setAbbuchen(bool _abbuchen)
        {
            Abbuchen = _abbuchen;
        }

        public bool getAbbuchen()
        {
            return Abbuchen;
        }

        private void setZielkontonummer(int _zielkontonummer)
        {
            Zielkontonummer = _zielkontonummer;
        }
        public int getZielkontonummer()
        {
            return Zielkontonummer;
        }

        private void setLastschriftbetrag(double _lastschriftbetrag)
        {
            Lastschriftbetrag = _lastschriftbetrag;
        }
        public double getLastschriftbetrag()
        {
            return Lastschriftbetrag;
        }

        //*** Team 4 End

        // 2.3 weitere Methoden

        public void Einzahlen(double _betrag, int _kontonummer, DateTime _datum)
        { //hier wird die Nummer der Liste als Kontonummer verkauft! Hat Bücken falsch gemacht
            if (_betrag > 0)
            {
                setKontostand(Kontostand + _betrag);

                //*** Team 1 Start
                sKontobewegung = Convert.ToString(getKontonummer());
                sKontobewegung += " \t\t|Einzahlen\t| +";
                sKontobewegung += Convert.ToString(_betrag) + " EUR\t|";
                sKontobewegung += Convert.ToString(_datum.ToShortDateString());
                sKontobewegung += "\t|";
                Kontobewegungen.Add(sKontobewegung);
                //*** Team 1 End

                Console.WriteLine("Ihr Betrag in Höhe von " + _betrag + " EUR wurde dem Konto " + Kontonummer + " gutgeschrieben");
            }
            else
            {
                Console.WriteLine("Sie haben keinen gültigen Einzahlungsbetrag eingegeben: " + _betrag);
            }
        }

        public void Auszahlen(double _betrag, int _kontonummer, DateTime _datum)
        {
            if (_betrag >= 0)
            {
                if ((Kontostand - _betrag) >= Kreditlinie)
                {
                    setKontostand(Kontostand - _betrag);

                    //*** Team 1 Start
                    //Kontobewegung erfassen
                    sKontobewegung = Convert.ToString(getKontonummer());
                    sKontobewegung += " \t\t|Auszahlen\t| -";                                     //+= fügt weiter Zeichen dem String hinzu
                    sKontobewegung += Convert.ToString(_betrag) + " EUR\t|";
                    sKontobewegung += Convert.ToString(_datum.ToShortDateString());
                    sKontobewegung += "\t|";
                    Kontobewegungen.Add(sKontobewegung);                                    //nachdem String mit Daten befüllt ist, diesen der List hinzufügen
                    //*** Team 1 End

                    Console.WriteLine("Ihr gewünschter Auszahlungsbetrag konnte erfolgreich ausgezahlt werden.");
                }
                else
                {
                    Console.WriteLine("Ihr gewünschter Auszahlungsbetrag übersteigt Ihre Kreditlinie.");
                    Console.WriteLine("Bitte reduzieren Sie Ihren gewünschten Auszahlungsbetrag.");
                }
            }
            else
            {
                Console.WriteLine("Ihr Auszahlungsbetrag war negativ: " + _betrag);
                Console.WriteLine("Sie haben keinen Auszahlungsbetrag erhalten.");
            }
        }

        public void KreditlinieErhoehen(double _kreditlinie, int _kontonummer)
        {
            Console.Write("Bitte geben Sie die Höhe Ihres monatlichen Nettoeinkommens ein:");
            double maxKreditlinie = 3 * Convert.ToDouble(Console.ReadLine());

            if (maxKreditlinie >= _kreditlinie)
            {
                setKreditlinie(-_kreditlinie);
            }
            else
            {
                setKreditlinie(-maxKreditlinie);
            }
        }

        public bool Kontoaufloesung(int _kontonummer, DateTime _datum)
        {
            if (Kontostand >= 0)
            {
                Console.WriteLine("Ihr Guthaben in Höhe von " + Kontostand + " EUR wird Ihnen ausbezahlt.");
                Auszahlen(Kontostand, _kontonummer, _datum);
                Console.WriteLine("Ihr Konto wurde gelöscht.");
                Kontozaehler--;
                return true;
            }
            else
            {
                Console.WriteLine("Sie müssen vor der Auflösung Ihr Konto ausgleichen");
                return false;
            }
        }
		
		//*** Team 2 Start
		
		public void Zinsberechnung(DateTime _date1, DateTime _date2, DateTime _datum)
        {                                                           
            kvr = getKontostand();                                  
                                                                    
                                                                    
            //geändert                                             
            if (kvr >= 0)                                           
            {                                                       
                zins = getGuthabenszinssatz();                      
                                                                    
                zeit = (_date1 - _date2).TotalDays;                 
                zwischenergebnis = kvr * zins * zeit / 365;        
                zwischenergebnis = Math.Round(zwischenergebnis, 2); 
                knr = kvr + zwischenergebnis;

                setKontostand(knr);

                //*** Team 1 Start
                //Kontobewegung erfassen
                sKontobewegung = Convert.ToString(getKontonummer());
                sKontobewegung += " \t\t|Gutschrift (Zins)\t| +";
                sKontobewegung += Convert.ToString(zwischenergebnis) + " EUR\t|";
                sKontobewegung += Convert.ToString(_datum.ToShortDateString());
                sKontobewegung += "\t|";
                Kontobewegungen.Add(sKontobewegung);
                //*** Team 1 End
            }
            else                                                    
            {                                                      
                zins = getDispozinssatz();                          
                                                                    
                zeit = (_date1 - _date2).TotalDays;                 
                zwischenergebnis = kvr * zins * zeit / 365;        
                zwischenergebnis = Math.Round(zwischenergebnis, 2);
                knr = kvr + zwischenergebnis;                      
                                                                    
                setKontostand(knr);

                //*** Team 1 Start
                //Kontobewegung erfassen
                sKontobewegung = Convert.ToString(getKontonummer());
                sKontobewegung += " \t\t|Abbuchung (Dispo)\t| -";
                sKontobewegung += Convert.ToString(zwischenergebnis) + " EUR\t|";
                sKontobewegung += Convert.ToString(_datum.ToShortDateString());
                sKontobewegung += "\t|";
                Kontobewegungen.Add(sKontobewegung);
                //*** Team 1 End

            }
                                                       
            zeit = Math.Round(zeit, 0);                               
                                                        
            Zinssaetze.Add(zwischenergebnis);                      
            Zeitraum.Add(zeit);                                    
        }         
		
		//*** Team 2 End

        //*** Team 3 Start
        public bool UeberweisungAbbuchen(double _betrag, int _kontonummer, DateTime _datum)
        {
            if (_betrag > 0)
            {
                if ((Kontostand - _betrag) >= Kreditlinie)
                {
                    setKontostand(Kontostand - _betrag);

                    //*** Team 1 Start
                    sKontobewegung = Convert.ToString(getKontonummer());
                    sKontobewegung += " \t\t|Überweisung\t| -";
                    sKontobewegung += Convert.ToString(_betrag) + " EUR\t|";
                    sKontobewegung += Convert.ToString(_datum.ToShortDateString());
                    sKontobewegung += "\t|";
                    Kontobewegungen.Add(sKontobewegung);
                    //*** Team 1 End

                    return true;
                }
                else
                {
                    Console.WriteLine("Ihr gewünschter Überweisungsbetrag übersteigt Ihre Kreditlinie.");
                    Console.WriteLine("Bitte reduzieren Sie Ihren gewünschten Überweisungsbetrag.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Ihr Überweisungsbetrag war: " + _betrag + ". Nicht gültiger Betrag.");
                Console.WriteLine("Sie haben keine Überweisung getätigt.");
                return false;
            }
        }


        public void UeberweisungGutschreiben(double _betrag, int _kontonummer, DateTime _datum)
        {
            //darf nur in Verbindung mit vorhergehendem Aufruf von "UeberweisungAbbuchen()" aufgerufen werden!!
            setKontostand(Kontostand + _betrag);
            //*** Team 1 Start
            sKontobewegung = Convert.ToString(getKontonummer());
            sKontobewegung += " \t\t|Gutschrift\t| +";
            sKontobewegung += Convert.ToString(_betrag) + " EUR\t|";
            sKontobewegung += Convert.ToString(_datum.ToShortDateString());
            sKontobewegung += "\t|";
            Kontobewegungen.Add(sKontobewegung);
            //*** Team 1 End
            Console.WriteLine("Ihr Betrag in Höhe von " + _betrag + " EUR wurde dem Konto " + _kontonummer + " gutgeschrieben");
        }

        public void DauerauftragAktivieren(double _betrag, int _kontonummer, int _wiederholungen, int _tag, DateTime _datumjetzt, int _kontonummeranzeige)
        {
            //wie viele Abfragen soll es geben? Kein Check auf positive Beträge, Vorhandensein der Zielktonr., ....
            setDauerauftragBetrag(_betrag);
            setDauerauftragZielkonto(_kontonummer);
            setDauerauftragWiederholungen(_wiederholungen);
            setDauerauftragTag(_tag);
            setDauerauftragErstellungDatum(_datumjetzt);
            //setDauerauftragVergleichDatum(_datumjetzt);
            DauerauftragDatumListe(_datumjetzt, _wiederholungen, _tag);
            setDauerauftragZielkontonummer(_kontonummeranzeige);
            setDauerauftragAktiv(true);
            Console.WriteLine($"Dauerauftrag erstellt. {_wiederholungen} Überweisungen zu je {_betrag} EUR werden je zum nächst möglichen Zeitpunkt an Konto {_kontonummeranzeige} getätigt.");
        }

        public void DauerauftragDatumListe(DateTime _datumjetzt, int _wiederholungen, int _tag)
        {
            DauerauftragDatum.Clear(); //Liste komplett löschen für neuer DA

            DateTime tmpdatum = DauerauftragTagMod(_datumjetzt, _tag); //Datum jetzt mit gewünschtem Tag modifizieren

            if (tmpdatum > _datumjetzt) //Ist der mod. Tag > als jetzt -> Überweisung noch diesen Monat
            {
                DauerauftragDatum.Add(tmpdatum);

                for (int i = 0; i < _wiederholungen - 1; i++) //Der Liste das Datum zuführen
                {
                    DauerauftragDatum.Add(DauerauftragDatum[i].AddMonths(1));
                }

                for (int i = 0; i < DauerauftragDatum.Count; i++) //Die Einträge in der Liste mit dem Wunschtag modifizieren
                {
                    DauerauftragDatum[i] = DauerauftragTagMod(DauerauftragDatum[i], _tag);
                }
            }

            else //Ist der mod. Tag < als jetzt -> Überweisung erst nächsten Monat
            {
                DauerauftragDatum.Add(_datumjetzt.AddMonths(1));

                for (int i = 0; i < _wiederholungen - 1; i++) //Der Liste das Datum zuführen
                {
                    DauerauftragDatum.Add(DauerauftragDatum[i].AddMonths(1));
                }

                for (int i = 0; i < DauerauftragDatum.Count; i++) //Die Einträge in der Liste mit dem Wunschtag modifizieren
                {
                    DauerauftragDatum[i] = DauerauftragTagMod(DauerauftragDatum[i], _tag);
                }
            }
        }

        public DateTime DauerauftragTagMod(DateTime _datum, int _tag)
        {
            return new DateTime(_datum.Year, _datum.Month, _tag);
        }


        public bool DauerauftragDatumCheck(DateTime _jetzt)
        {

            for (int i = 0; i < DauerauftragDatum.Count; i++)
            {
                if (DauerauftragDatum[i] <= _jetzt) //DA-Datum < jetzt -> Überweisung tätigen
                {
                    if (DauerauftragAbbuchen(getDauerauftragBetrag(), getDauerauftragZielkonto(), _jetzt))
                    {
                        DauerauftragDatum.Remove(DauerauftragDatum[i]); //Löschen von dem Datum in der Liste
                        i--;
                        return true;
                    }

                    else
                    {
                        Console.WriteLine("DA nicht durchgeführt.");
                        return false;
                    }

                }


                else
                {
                    return false;

                }

            }
            return false;
        }

        public bool DauerauftragAbbuchen(double _betrag, int _kontonummer, DateTime _datum)
        {
            if ((getKontostand() - getDauerauftragBetrag()) >= Kreditlinie)
            {
                setKontostand(getKontostand() - getDauerauftragBetrag());
                //*** Team 1 Start
                sKontobewegung = Convert.ToString(getKontonummer());
                sKontobewegung += " \t\t|Dauerauftrag\t| -";
                sKontobewegung += Convert.ToString(_betrag) + " EUR\t|";
                sKontobewegung += Convert.ToString(_datum.ToShortDateString());
                sKontobewegung += "\t|";
                Kontobewegungen.Add(sKontobewegung);
                //*** Team 1 End
                return true;
            }

            else
            {
                Console.WriteLine("Ihr gewünschter Überweisungsbetrag übersteigt Ihre Kreditlinie.");
                Console.WriteLine("DA wird gelöscht.");
                setDauerauftragAktiv(false);
                return false;
            }
        }


        public void DauerauftragGutschreiben(double _betrag, DateTime _datum)
        {
            //darf nur in Verbindung mit vorhergehendem Aufruf von "DauerauftragAbbuchen()" aufgerufen werden!!
            setKontostand(Kontostand + _betrag); //Betrag dem Zielkonto gutschreiben
            //*** Team 1 Start
            sKontobewegung = Convert.ToString(getKontonummer());
            sKontobewegung += " \t\t|Gutschrift\t| +";
            sKontobewegung += Convert.ToString(_betrag) + " EUR\t|";
            sKontobewegung += Convert.ToString(_datum.ToShortDateString());
            sKontobewegung += "\t|";
            Kontobewegungen.Add(sKontobewegung);
            //*** Team 1 End
        }

        public void DauerauftragLoeschen() //Notlösung
        {
            setDauerauftragAktiv(false);
        }


        //*** Team 3 End

        //*** Team 4 Start
        public void Lastschriftabbbuchen(double _betrag, int _kontonummer, bool _abbuchen, DateTime _datum)
        {
            if (_betrag >= 0)
            {
                
                if ((Kontostand - _betrag) >= Kreditlinie)
                {
                    setKontostand(Kontostand - _betrag);
                    //*** Team 1 Start
                    sKontobewegung = Convert.ToString(getKontonummer());
                    sKontobewegung += " \t\t|Lastschrift\t| -";
                    sKontobewegung += Convert.ToString(_betrag) + " EUR\t|";
                    sKontobewegung += Convert.ToString(_datum.ToShortDateString());
                    sKontobewegung += "\t|";
                    Kontobewegungen.Add(sKontobewegung);
                    Console.WriteLine("Die Lastschrift wurde erfolgreich eingezogen.");
                    setAbbuchen(_abbuchen == false);
                    //*** Team 1 End
                }
                else
                {
                    setAbbuchen(_abbuchen == true);
                    Console.WriteLine("Ihr einzuziehender Bertrag übersteigt die Kreditlinie.");
                    Console.WriteLine("Bitte reduzieren Sie Ihren gewünschten Einzugsbetrag.");
                }
            }
            else
            {
                setAbbuchen(_abbuchen == true);
                Console.WriteLine("Ihr Einzugsbetrag war negativ: " + _betrag);
                Console.WriteLine("Sie haben keine Lastschrift erhalten.");
            }
        }

        public void Lastschriftgutschreiben(double _betrag, int _kontonummer, DateTime _datum)
        {
            if (_betrag > 0)
            {
                setKontostand(Kontostand + _betrag);
                //*** Team 1 Start
                sKontobewegung = Convert.ToString(getKontonummer());
                sKontobewegung += " \t\t|Gutschrift\t| +";
                sKontobewegung += Convert.ToString(_betrag) + " EUR\t|";
                sKontobewegung += Convert.ToString(_datum.ToShortDateString());
                sKontobewegung += "\t|";
                Kontobewegungen.Add(sKontobewegung);
                //*** Team 1 End
                Console.WriteLine("Ihr Betrag in Höhe von " + _betrag + " EUR wurde dem Konto " + Kontonummer + " gutgeschrieben");
            }
            else
            {
                Console.WriteLine("Sie haben keinen gültigen Einzahlungsbetrag eingegeben: " + _betrag);
            }
        }

        public void Lastschriftwidersprechen(double _betrag, int _kontonummer, DateTime _datum)
        {
            if (_betrag > 0)
            {
                setKontostand(Kontostand + _betrag);
                //*** Team 1 Start
                sKontobewegung = Convert.ToString(getKontonummer());
                sKontobewegung += " \t\t|Gutschrift\t| +";
                sKontobewegung += Convert.ToString(_betrag) + " EUR\t|";
                sKontobewegung += Convert.ToString(_datum.ToShortDateString());
                sKontobewegung += "\t|";
                Kontobewegungen.Add(sKontobewegung);
                //*** Team 1 End
                Console.WriteLine("Ihr Betrag in Höhe von " + _betrag + " EUR wurde dem Konto " + Kontonummer + " gutgeschrieben");
            }
            else
            {
                Console.WriteLine("Sie haben keinen gültigen Einzahlungsbetrag eingegeben: " + _betrag);
            }
        }

        //*** Team 4 End


        // 2.4 statische Methoden
        public static int nextnewKontonummer()
        {
            //Länge und Stellenzahl der Liste listeVergebenerKontonummern bestimmen,
            //die alle je eingesetzten Kontonummern speichert
            anzahlVergebenerKontonummern = listeVergebenerKontonummern.Count();
            stellenzahlVergebeneKontonummer = anzahlVergebenerKontonummern.ToString().Length;

            //Bestimmung der Min-/Maxgrenzen einer zufälligen, sich nicht wiederholenden Kontonummer
            MinZufallszahl = (int)Math.Pow(10, stellenzahlVergebeneKontonummer);
            MaxZufallszahl = (int)Math.Pow(10, stellenzahlVergebeneKontonummer + 1);

            //noch nie vergebenene Zufallsfallszahl aus dem eingeschränkten Bereich vergeben bestimmen
            do
            {
                randomKontonummer = zufall.Next(MinZufallszahl, MaxZufallszahl);

            } while (listeVergebenerKontonummern.Contains(randomKontonummer) & listeVergebenerKontonummern.Any());

            //neue Zufallszahl in die Liste aller Zufallszahlen anfügen und sortieren
            listeVergebenerKontonummern.Add(randomKontonummer);
            listeVergebenerKontonummern.Sort();
            druckelisteVergebenerKontonummern();

            return randomKontonummer;
        }

        public static void druckelisteVergebenerKontonummern()
        {
            Console.WriteLine();
            Console.WriteLine("\t| Lister aller vergebener Kontonummern:");
            Console.WriteLine("\t| bereits gelöscht sowie aktive");

            Console.WriteLine("-----------------------------------------------");

            for (int i = 0; i < listeVergebenerKontonummern.Count(); i++)
            {
                Console.WriteLine($"Nr. " + i + "\t| " + listeVergebenerKontonummern[i]);
                if (((i + 1) % 5 == 0) | ((i + 1) % 10 == 0))
                {
                    Console.WriteLine("-----------------------------------------------");
                }
            }
        }

        public static void AusgabeKontozaehler()
        {
            int a = listeVergebenerKontonummern.Count();
            int b = Kontozaehler;
            int c = a - b;
            Console.Write("\nDie M.E.S.K Bank hat bereits " + Kontozaehler + " Kund(In)en im Bestand.\n{0} Kund(In)en haben ihr Konto bereits gelöscht.\nBitte drücken für einen erneuten Start eine beliebige Taste... ", c);
        }
    }
}
