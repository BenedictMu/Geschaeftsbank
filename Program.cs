using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Konten
{
    public class Program
    {
        enum Aktionsmenue
        {
            neuerKunde,
            Einzahlen,
            Auszahlen,

            // Auszahlen eines Betrags vom eigenen Konto, Einzahlen eines Betrags auf ein existierendes, eingegebenes Konto
            // wiederholt ausgeführte Überweisung eines Betrags von einem Konto auf ein anderes Konto auszahlen bzw. einzahlen, max. 12-mal und einmal pro Monat
            Überweisen_Team3,
            DauerauftragMitAblaufdatum_Team3,

            // Abbuchen eines Betrags von einem existierenden, fremden Konto, Einzahlen dieses Betrags auf das eigene Konto
            // Rückzahlung einer widersprochenen Lastschriftanforderung vom eigenen Konto, Gutschreiben auf dem angeforderten Konto
            // Berechnung der Salden der Bank über alle Konten
            LastschriftDurchführen_Team4,
            LastschriftWidersprechen_Team4,

            // Datum muss bei jeder Menüwahl zufällig oder per Handeingabe fortgeschrieben werden
            // alle Kontobewegungen:
            // Einzahlungen, Auszahlungen, Überweisung, Dauerauftrag, Lastschrift und Lastschriftwiderspruch...
            // ...müssen inkl. Datum chronologisch geeignet im Konto gespeichert werden
            KontobewegungenAnzeigen_Team1,

            // Guthaben- und Dispozinsen müssen eingegeben und allen Kundenkonten mitgeteilt werden                                       
            // Berechnung und Speicherung der Guthaben- bzw. Dispozinsen ...
            // ... bei jeder Kontobewegung und bei jeder Zinsänderung bei Zugriff auf das Konto
            ZinssätzePflegen_Team2,
            ZinsenBerechnenSpeichernAnzeigen_Team2,

            KreditlinieErhöhen,

            SaldoDerBankÜberAlleKonten_Team4,
            KundennamenÄndern,

            Kontoauflösung = 50,
            ListeVergebenerKontonummern = 70,

            Beenden = 99
        }

        static void ausgabeInhaberKontonummerKontostandKreditlinie(List<Konto> KontoListe)
        {
            Console.WriteLine("\t| Kontoinhaber\t| Kontonummer\t| Kontostand\t| Kreditlinie\t| Guthabenszinssatz\t| Dispozinssatz\t| Dauerauftrag");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------");

            for (int i = 0; i < KontoListe.Count; i++)
            {
                Console.Write($"Nr. " + i);
                Console.Write("\t| " + KontoListe[i].getInhaber());
                Console.Write("\t\t| " + KontoListe[i].getKontonummer());
                Console.Write("\t\t| " + KontoListe[i].getKontostand() + " EUR");
                Console.Write("\t| " + KontoListe[i].getKreditlinie() + " EUR");
                Console.Write("\t| " + KontoListe[i].getGuthabenszinssatz() + " %");
                Console.WriteLine("\t\t\t| " + KontoListe[i].getDispozinssatz() + " %");
                Console.WriteLine("\t\t| " + KontoListe[i].getDauerauftragAktiv());
                if (((i + 1) % 5 == 0) | ((i + 1) % 10 == 0))
                {
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------");
                }
            }

            Console.WriteLine();
        }


        //*** Team 3 Start
        static bool ZielkundenkontoCheck(ref int zielkontonummer, ref List<Konto> KontoListe, ref int lfdNrInZielKontoListe)
        {
            for (int i = 0; i < KontoListe.Count(); i++)
            {
                if (KontoListe[i].getKontonummer() == zielkontonummer)
                {
                    lfdNrInZielKontoListe = i;

                    return true;
                }
                /*else if (i >= KontoListe.Count())
                {
                    Console.WriteLine("Nicht vorhanden if!");
                return false;
                }*/
            }
            Console.WriteLine("Kontonummer nicht vorhanden!");
            return false;
        }

        //*** Team 3 End

        //*** Team 4 Start

        //*** Team 4 End





        static void Main(string[] args)
        {
            Console.Title = $"FTE2 Schuljahr 2022/23, Projekt: unser digitaler Automat für den 24/7 Service mit Kundenkontos, {DateTime.Now.ToString()}";

            string bedienereingabe;

            int kontonummer;
            double geldbetrag;
            string name;

            Konto neukundenkonto;
            Konto bestandskundenkonto;

            List<Konto> KontoListe = new List<Konto>();

            //*** Team 3 Start
            int zielkontonummer = 0;
            int lfdNrInZielKontoListe = 0;
            int wiederholungenDauerauftrag;
            int dauerauftragTag;
            Konto zielkundenkonto;
            //*** Team 3 End

            //*** Team 1 Start
            DateTime datum = DateTime.Now;
            Random rnd = new Random();
            string sKontonummer;
            string sKontobewegung;
            //*** Team 1 End

            //*** Team 2 Start
            double guthabenzinssatz;
            double dispozinssatz;
            List<DateTime> Datumsaenderung = new List<DateTime>();
            Datumsaenderung.Add(datum);
            //*** Team 2 End

            //*** Team 4 Start
            double gesamtsaldo = 0;
            bool eingabekontrolle;
            string eingabe;
            int lastschriftnummerwahl;
            Konto lastschriftnummer;
            List<Konto> LastschriftListe = new List<Konto>();
            bool abbuchen = true;
            //*** Team 4 End

            do
            {
                Console.WriteLine("Der digitale 24/7 Service der M.E.S.K Bank begrüßt Sie herzlich bei der Erledigung Ihrer Anforderungen rund um Ihr Kundenkonto.");
                Console.WriteLine($"Wählen Sie aus der folgenden Liste Ihre gewünschte Aktion aus:\n");

                ausgabeInhaberKontonummerKontostandKreditlinie(KontoListe);


                Console.WriteLine();

                Console.WriteLine($"Das heutige Datum ist: {datum.ToShortDateString()}");

                Console.WriteLine();

                foreach (Aktionsmenue aktion in Enum.GetValues(typeof(Aktionsmenue)))
                {
                    Console.WriteLine("{0,3}     {1}", (int)aktion, (Aktionsmenue)aktion);
                }

                Console.Write("\nIhre gewählte Aktionsnummer lautet: ");
                bedienereingabe = Console.ReadLine();
                Console.WriteLine();

                if (bedienereingabe == "0")
                {
                    Console.Clear();
                    ausgabeInhaberKontonummerKontostandKreditlinie(KontoListe);
                    Console.Write("Bitte geben Sie Ihren Namen ein: ");
                    name = Console.ReadLine();
                    neukundenkonto = new Konto(name);
                    Console.Write("\nBitte geben Sie den Guthabenszinssatz zwischen 0 und 2 ein: ");
                    neukundenkonto.setGuthabenszinssatz(Convert.ToDouble(Console.ReadLine()));
                    Console.Write("Bitte geben Sie den Dispozinssatz zwischen 4 und 14 ein: ");
                    neukundenkonto.setDispozinssatz(Convert.ToDouble(Console.ReadLine()));
                    Console.WriteLine("\n--------------------------------------------------------");
                    Console.WriteLine("Willkommen - Sehr geehrte(r) Frau/Herr\t\t| " + neukundenkonto.getInhaber());
                    Console.WriteLine("Die Kontonummer Ihres neuen Kontos lautet\t| " + neukundenkonto.getKontonummer());
                    Console.WriteLine("Ihre Kontostand beträgt\t\t\t\t| " + neukundenkonto.getKontostand() + " EUR");
                    Console.WriteLine("Ihre Guthabenszinssatz beträgt\t\t\t| " + neukundenkonto.getGuthabenszinssatz() + " %");
                    Console.WriteLine("Ihre Dispozinssatz beträgt\t\t\t| " + neukundenkonto.getDispozinssatz() + " %");
                    Console.WriteLine("Ihre Kreditlinie wurde festgelegt auf\t\t| " + neukundenkonto.getKreditlinie() + " EUR");
                    KontoListe.Add(neukundenkonto);
                    Console.WriteLine("Sie können Ihr Konto im Rahmen Ihrer Kreditlinie nutzen.");
                    Console.WriteLine("--------------------------------------------------------");
                }
                else
                {
                    if ((bedienereingabe != "0") &
                        (bedienereingabe != "1") &
                        (bedienereingabe != "2") &
                        (bedienereingabe != "3") &
                        (bedienereingabe != "4") &
                        (bedienereingabe != "5") &
                        (bedienereingabe != "6") &
                        (bedienereingabe != "7") &
                        (bedienereingabe != "8") &
                        (bedienereingabe != "9") &
                        (bedienereingabe != "10") &
                        (bedienereingabe != "11") &
                        (bedienereingabe != "12") &
                        (bedienereingabe != "50") &
                        (bedienereingabe != "70") &
                        (bedienereingabe != "99"))
                    {
                        Console.WriteLine("falsche Eingabe " + bedienereingabe);
                    }
                    else
                    {
                        ausgabeInhaberKontonummerKontostandKreditlinie(KontoListe);
                        if (KontoListe.Count() > 0)
                        {
                            bool nichtEnhalten = true;
                            int lfdNrInKontoListe = 0;

                            //Ermittlung der laufenden Nummer in der KontoListe, an der die KontoListe[i].Kontonummer = kontonummer ist
                            do
                            {
                                do
                                {
                                    Console.Write("Bitte geben Sie Ihre Kontonummer ein: ");
                                    eingabe = Console.ReadLine();

                                    try
                                    {
                                        int kontrolle = Convert.ToInt32(eingabe);
                                        eingabekontrolle = false;
                                    }
                                    catch (Exception)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Das Format der Eingabe stimmt nicht, bitte erneut eingeben!!");
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                        eingabekontrolle = true;
                                    }
                                } while (eingabekontrolle == true);

                                kontonummer = Convert.ToInt32(eingabe);

                                for (int i = 0; i < KontoListe.Count(); i++)
                                {
                                    if (KontoListe[i].getKontonummer() == kontonummer)
                                    {
                                        lfdNrInKontoListe = i;
                                        nichtEnhalten = false;
                                        // Mit break kann die Ausführung einer Schleife beendet werden
                                        break;
                                    }
                                }

                            } while (nichtEnhalten);

                            bestandskundenkonto = KontoListe[lfdNrInKontoListe];

                            switch (bedienereingabe)
                            {
                                case "1": //einzahlen
                                    {
                                        Console.Write("Bitte geben Sie den Betrag ein, den Sie einzahlen: ");
                                        geldbetrag = Convert.ToDouble(Console.ReadLine());
                                        //*** Team 1 Start
                                        bestandskundenkonto.Einzahlen(geldbetrag, lfdNrInKontoListe, datum);
                                        //*** Team 1 End
                                        Console.WriteLine("Ihre Kontostand beträgt  " + bestandskundenkonto.getKontostand() + " EUR.");
                                    }
                                    break;

                                case "2": //auszahlen
                                    {
                                        Console.Write("Bitte geben Sie den Betrag ein, den Sie abheben möchten: ");
                                        geldbetrag = Convert.ToDouble(Console.ReadLine());
                                        //*** Team 1 Start
                                        bestandskundenkonto.Auszahlen(geldbetrag, lfdNrInKontoListe, datum);
                                        //*** Team 1 End
                                        Console.WriteLine("Ihre Kontostand beträgt  " + bestandskundenkonto.getKontostand() + " EUR.");
                                    }
                                    break;

                                //*** Team 3 Start
                                case "3": //Überweisen
                                    {
                                        do
                                        {
                                            Console.Write("Bitte geben Sie die Ziel-Kontonummer für Ihre Überweisung ein: ");
                                            eingabe = Console.ReadLine();
                                            try
                                            {
                                                int kontrolle = Convert.ToInt32(eingabe);
                                                eingabekontrolle = false;
                                            }
                                            catch (Exception)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Das Format der Eingabe stimmt nicht, bitte erneut eingeben!!");
                                                Console.ForegroundColor = ConsoleColor.Gray;
                                                eingabekontrolle = true;
                                            }
                                        } while (eingabekontrolle == true);
                                        zielkontonummer = Convert.ToInt32(eingabe);

                                        if (ZielkundenkontoCheck(ref zielkontonummer, ref KontoListe, ref lfdNrInZielKontoListe))
                                        {
                                            zielkundenkonto = KontoListe[lfdNrInZielKontoListe];

                                            if (zielkundenkonto.getKontonummer() != bestandskundenkonto.getKontonummer())
                                            {
                                                Console.Write("Bitte geben Sie den Betrag ein, den Sie überweisen wollen: ");
                                                geldbetrag = Convert.ToInt32(Console.ReadLine());

                                                if (bestandskundenkonto.UeberweisungAbbuchen(geldbetrag, lfdNrInKontoListe, datum))
                                                {
                                                    zielkundenkonto.UeberweisungGutschreiben(geldbetrag, zielkontonummer, datum);
                                                } //else-Bedingung in "uebreweisungabbuchen-methode"
                                            }
                                            else
                                            {
                                                Console.WriteLine("Zielkontonummer = Kontonummer!\nAbbruch. Keine Überweisung getätigt.");
                                                break;
                                            }

                                        }
                                        else
                                        {
                                            Console.WriteLine("Abbruch. Keine Überweisung getätigt.");
                                            break;
                                        }

                                        Console.WriteLine("Ihr Kontostand beträgt  " + bestandskundenkonto.getKontostand() + " EUR.");

                                    }
                                    break;

                                case "4": //Dauerauftrag (da in Spez nicht klar formuliert: nur ein DA der nicht änderbar ist!)
                                    {

                                        Console.WriteLine("Es ist immer nur ein Dauerauftrag pro Konto möglich!");
                                        if (!bestandskundenkonto.getDauerauftragAktiv())
                                        {

                                            //Eingabe der Zielkontennummer, Prüfen ob Zielkontennummer existiert
                                            do
                                            {
                                                Console.Write("Bitte geben Sie die Ziel-Kontonummer für Ihren Dauerauftrag ein: ");
                                                eingabe = Console.ReadLine();
                                                try
                                                {
                                                    int kontrolle = Convert.ToInt32(eingabe);
                                                    eingabekontrolle = false;
                                                }
                                                catch (Exception)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("Das Format der Eingabe stimmt nicht, bitte erneut eingeben!!");
                                                    Console.ForegroundColor = ConsoleColor.Gray;
                                                    eingabekontrolle = true;
                                                }
                                            } while (eingabekontrolle == true);

                                            zielkontonummer = Convert.ToInt32(eingabe);

                                            if (ZielkundenkontoCheck(ref zielkontonummer, ref KontoListe, ref lfdNrInZielKontoListe))
                                            {
                                                zielkundenkonto = KontoListe[lfdNrInZielKontoListe];
                                                if (zielkundenkonto.getKontonummer() != bestandskundenkonto.getKontonummer())
                                                {
                                                    Console.Write("Bitte geben Sie den Betrag ein, den Sie überweisen wollen: ");
                                                    geldbetrag = Convert.ToInt32(Console.ReadLine());
                                                    Console.Write("Bitte geben Sie an, zu welchem Tag im Monat überwiesen werden soll (z.B. '3' für den 3. des Monats; zwischen 1-28 (wegen Februar)): ");
                                                    dauerauftragTag = Convert.ToInt32(Console.ReadLine());
                                                    if (dauerauftragTag <= 28)
                                                    {
                                                        //-> Nur möglich zw. 1-28 (wegen Februar!)
                                                        Console.Write("Bitte geben Sie die Wiederholungen ein: ");
                                                        wiederholungenDauerauftrag = Convert.ToInt32(Console.ReadLine());

                                                        bestandskundenkonto.DauerauftragAktivieren(geldbetrag, lfdNrInZielKontoListe, wiederholungenDauerauftrag, dauerauftragTag, datum, zielkontonummer);
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Falscher Tag! Nur Werte 1-28! Abbruch.");
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Zielkontonummer = Kontonummer!\nAbbruch.");
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Abbruch. Kein Dauerauftrag erstellt.");
                                                break;

                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Es ist schon ein Dauerauftrag aktiv!");
                                            break;
                                        }

                                    }
                                    break;

                                //*** Team 3 End
                                //*** Team 4 Start

                                case "5": //Lastschrift einziehen
                                    {
                                        //Eingabe der Zielkontennummer, Prüfen ob Zielkontennummer existiert
                                        do
                                        {
                                            do
                                            {
                                                Console.Write("Bitte geben Sie die Kontonummer ein von der Sie eine Lastschrift einziehen wollen: ");
                                                eingabe = Console.ReadLine();
                                                try
                                                {
                                                    int kontrolle = Convert.ToInt32(eingabe);
                                                    eingabekontrolle = false;
                                                }
                                                catch (Exception)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("Das Format der Eingabe stimmt nicht, bitte erneut eingeben!!");
                                                    Console.ForegroundColor = ConsoleColor.Gray;
                                                    eingabekontrolle = true;
                                                }
                                            } while (eingabekontrolle == true);

                                            zielkontonummer = Convert.ToInt32(eingabe);

                                        } while (ZielkundenkontoCheck(ref zielkontonummer, ref KontoListe, ref lfdNrInZielKontoListe) == false);
                                       

                                        //zielkundenkonto = KontoListe[0];
                                        
                                        zielkundenkonto = KontoListe[lfdNrInZielKontoListe];

                                        //Eingabe des Geldbetrags und bearbeitung der Lastschrift

                                        do
                                        {
                                            Console.Write("Bitte geben Sie den Betrag ein, den Sie per Lastschrift einziehen wollen: ");
                                            eingabe = Console.ReadLine();
                                            try
                                            {
                                                int kontrolle = Convert.ToInt32(eingabe);
                                                eingabekontrolle = false;
                                            }
                                            catch (Exception)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("Das Format der Eingabe stimmt nicht, bitte erneut eingeben!!");
                                                Console.ForegroundColor = ConsoleColor.Gray;
                                                eingabekontrolle = true;
                                            }
                                        } while (eingabekontrolle == true);


                                        geldbetrag = Convert.ToInt32(eingabe);
                                        
                                        zielkundenkonto.Lastschriftabbbuchen(geldbetrag, lfdNrInKontoListe, abbuchen, datum);
                                        //Abfrage ob die Abbuchung erfolgreich war und ob fortgefahren werden kann
                                        if (zielkundenkonto.getAbbuchen() == false)
                                        {
                                            bestandskundenkonto.Lastschriftgutschreiben(geldbetrag, lfdNrInZielKontoListe, datum);
                                            Console.WriteLine("Ihr Kontostand beträgt  " + bestandskundenkonto.getKontostand() + " EUR.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Das Lastschriftverfahren wurde abgebrochen !!!");
                                        }

                                        //Erstellen einer Lastschriftliste zum späteren Widerspruch
                                        lastschriftnummer = new Konto(kontonummer, zielkontonummer, geldbetrag);
                                        LastschriftListe.Add(lastschriftnummer);
                                    }
                                    break;


                                case "6": //Lastschrift widerrufen
                                    {
                                        bool lastschriftvorhandenKonto = false;
                                        Console.Clear();

                                        Console.WriteLine("\tLastschr.Nr.\t\tEigene Kont.Nr.\t\tAndere Kont.Nr.\t\tLastsch. Betrag");
                                        for (int i = 0; i < LastschriftListe.Count(); i++)
                                        {
                                            if (LastschriftListe[i].getZielkontonummer() == kontonummer)
                                            {
                                                lastschriftvorhandenKonto = true;
                                                //Console.Write("\t" + LastschriftListe[i].getLastschriftnummer());
                                                Console.Write("\t" + i);
                                                Console.Write("\t\t\t" + LastschriftListe[i].getZielkontonummer());
                                                Console.Write("\t\t\t" + LastschriftListe[i].getKontonummer());
                                                Console.Write("\t\t\t" + LastschriftListe[i].getLastschriftbetrag());
                                                Console.Write("\n");
                                            }
                                        }
                                        if (lastschriftvorhandenKonto == false)
                                        {
                                            Console.WriteLine("\nVon Ihrem Konto wurde keine Lastschrift abgebucht!!");
                                        }
                                        else
                                        {


                                            do
                                            {
                                                Console.Write("\nBitte geben Sie die Nummer der Lastschrift ein die Sie Widerrufen möchten: ");
                                                eingabe = Console.ReadLine();
                                                try
                                                {
                                                    int kontrolle = Convert.ToInt32(eingabe);
                                                    eingabekontrolle = false;
                                                }
                                                catch (Exception)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("Das Format der Eingabe stimmt nicht, bitte erneut eingeben!!");
                                                    Console.ForegroundColor = ConsoleColor.Gray;
                                                    eingabekontrolle = true;
                                                }
                                            } while (eingabekontrolle == true);

                                            lastschriftnummerwahl = Convert.ToInt32(eingabe);
                                            bool lastschriftnummervorhanden = false;



                                            for (int i = 0; i < KontoListe.Count(); i++)
                                            {
                                                if (LastschriftListe[lastschriftnummerwahl].getKontonummer() == KontoListe[i].getKontonummer())
                                                {
                                                    lfdNrInKontoListe = i;
                                                    nichtEnhalten = false;
                                                    // Mit break kann die Ausführung einer Schleife beendet werden
                                                    break;
                                                }
                                            }

                                            zielkundenkonto = KontoListe[lfdNrInKontoListe];


                                            for (int i = 0; i < LastschriftListe.Count; i++)
                                            {
                                                if (lastschriftnummerwahl < LastschriftListe.Count)
                                                {
                                                    if (LastschriftListe[lastschriftnummerwahl].getZielkontonummer() == kontonummer)
                                                    {
                                                        lastschriftnummervorhanden = true;
                                                        Console.WriteLine($"Der Lastschrift mit der Nummer {lastschriftnummerwahl} wird nun widersprochen!");
                                                        
                                                        zielkundenkonto.Lastschriftabbbuchen(LastschriftListe[i].getLastschriftbetrag(), lfdNrInKontoListe, abbuchen, datum);
                                                        if (zielkundenkonto.getAbbuchen() == false)
                                                        {
                                                            bestandskundenkonto.Lastschriftwidersprechen(LastschriftListe[i].getLastschriftbetrag(), lfdNrInKontoListe, datum);
                                                            Console.WriteLine("Ihr Kontostand beträgt  " + bestandskundenkonto.getKontostand() + " EUR.");
                                                            Console.WriteLine($"Die Lastschrift {lastschriftnummerwahl} wurde gelöscht!");
                                                            LastschriftListe.RemoveAt(i);
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine($"Der Widerspruch ist fehlgeschlagen, das Konto mit der Nummer {LastschriftListe[i].getKontonummer()} hat nicht genügend Guthaben!");
                                                        }
                                                    }
                                                }
                                            }
                                            if (lastschriftnummervorhanden == false)
                                            {
                                                Console.WriteLine("Die von Ihnen gewählte Lastschriftnummer ist bei Ihrem Konto nicht vorhanden!!");
                                            }
                                        }

                                    }
                                    break;

                                //*** Team 4 End
                                //*** Team 1 Start

                                case "7": //Kontobewegungen anzeigen
                                    {
                                        Console.Clear();
                                        Console.Write($"Kontobewegung von: ");
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write($"{bestandskundenkonto.getInhaber()}\n");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.WriteLine(" _______________ ____________________ _______________ _______________________ ");
                                        Console.WriteLine("|Kontonummer\t|Bewegungsart\t\t|Betrag\t\t|Datum\t\t\t|");
                                        Console.WriteLine("|_______________|____________________|_______________|_______________________|");

                                        sKontonummer = Convert.ToString(bestandskundenkonto.getKontonummer());
                                        for (int i = 0; i < bestandskundenkonto.getKontobewegungsanzahl(); i++)
                                        {
                                            sKontobewegung = bestandskundenkonto.getsKontobewegungen(i);
                                            if (sKontobewegung.Substring(0, sKontobewegung.IndexOf(" ")) == sKontonummer)
                                            {
                                                Console.Write("| ");
                                                bestandskundenkonto.getKontobewegungen(i);
                                            }

                                        }
                                        Console.WriteLine("|_______________|____________________|_______________|_______________________|");
                                    }
                                    break;

                                //*** Team 1 End

                                //*** Team 2 Start

                                case "8": //Zinssätze pflegen
                                    {
                                        Console.Write("Bitte geben Sie den Guthabenszinssatz ein, den Sie bekommen: ");                            
                                        guthabenzinssatz = Convert.ToDouble(Console.ReadLine());                                                   
                                        Console.Write("Bitte geben Sie den Disposzinssatz ein, den Sie bekommen: ");                               
                                        dispozinssatz = Convert.ToDouble(Console.ReadLine());                                                      
                                        bestandskundenkonto.setGuthabenszinssatz(guthabenzinssatz);                                                
                                        bestandskundenkonto.setDispozinssatz(dispozinssatz);                                                       
                                        Console.WriteLine("Ihre Guthabenszinssatz beträgt  " + bestandskundenkonto.getGuthabenszinssatz() + " %.");
                                        Console.WriteLine("Ihre Dispozinssatz beträgt  " + bestandskundenkonto.getDispozinssatz() + " %.");        
                                    }
                                    break;

                                case "9": //Zinsen berechnen
                                    {
                                        Console.WriteLine("Ihre letzten Zinssätze sahen wie folgt aus:\n");                                        
                                                                                                                                                   
                                        bestandskundenkonto.Ausgabezinsen();
                                    }
                                    break;

                                //*** Team 2 End

                                case "10":
                                    {
                                        Console.Write("Bitte geben Sie den den Betrag ein, um den Sie die Kreditlinie erhöhen möchten: ");
                                        geldbetrag = Convert.ToInt32(Console.ReadLine());
                                        bestandskundenkonto.KreditlinieErhoehen(geldbetrag, lfdNrInKontoListe);
                                        Console.WriteLine("Ihre Kreditlinie beträgt  " + bestandskundenkonto.getKreditlinie() + " EUR.");
                                    }
                                    break;

                                //*** Team 4 Start
                                case "11": //Saldo Bank + Konten
                                    {
                                        for (int i = 0; i < KontoListe.Count; i++)
                                        {
                                            gesamtsaldo = gesamtsaldo + KontoListe[i].getKontostand();
                                            Console.WriteLine($"Der Saldo der Kontonummer {KontoListe[i].getKontonummer()} berträgt {KontoListe[i].getKontostand()}EUR.");
                                        }
                                        Console.WriteLine($"Der Gesamtsaldo der Bank beträgt {gesamtsaldo}EUR.");
                                    }
                                    break;


                                //*** Team 4 End

                                case "12": //Kundennamen ändern
                                    {
                                        Console.Write("Bitte geben Sie Ihren Namen ein: ");
                                        name = Console.ReadLine();
                                        bestandskundenkonto.setInhaber(name);
                                    }
                                    break;

                                case "50":
                                    {
                                        if (bestandskundenkonto.Kontoaufloesung(lfdNrInKontoListe, datum))
                                        {
                                            KontoListe.Remove(bestandskundenkonto);
                                        }
                                    }
                                    break;

                                case "70":
                                    {
                                        Konto.druckelisteVergebenerKontonummern();
                                    }
                                    break;
                                default:
                                    {
                                        Console.WriteLine("\n****************************************\nunder construction by the teams of FTE2\n****************************************");
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ohne Kundennummer können Sie keine Kontobewegungen ausführen...");
                        }
                    }
                }
                Konto.AusgabeKontozaehler();
                Console.ReadKey();
                Console.Clear();

                //*** Team 1 Start
                if (bedienereingabe != "99")
                {
                    int antwort;
                    string antwortein;
                    string datum_ein;
                    do
                    {

                        do
                        {
                            Console.Clear();
                            Console.Write($"Das vorherige Datum war: {datum.ToShortDateString()}\n\n");
                            Console.Write("Datum... \n --> für selber eingeben, die \"1\" drücken!\n --> für generieren lassen, die \"2\" drücken!\n Ihre Antwort: ");
                            antwortein = Console.ReadLine();

                        } while (!(antwortein.All(char.IsDigit)) || antwortein == "");

                        antwort = Convert.ToInt32(antwortein);

                    } while (!(antwort == 1 || antwort == 2));

                    switch (antwort)
                    {
                        case 1:
                            {
                                DateTime datum_eng;

                                do
                                {
                                    do
                                    {
                                        Console.Write("\nGeben Sie das heutige Datum ein (z.B. 21.02.2022) : ");
                                        datum_ein = Console.ReadLine();

                                        if (!(Regex.IsMatch(datum_ein, @"^[00.00.0000-99.99.9999]+$")) || datum_ein.All(char.IsDigit))
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("\nIhr Datum erfüllt die Vorgaben nicht!\nBitte versuchen Sie es erneut\n");
                                            Console.ForegroundColor = ConsoleColor.White;
                                        }

                                    } while (!(Regex.IsMatch(datum_ein, @"^[00.00.0000-99.99.9999]+$")) || datum_ein.All(char.IsDigit));

                                    datum_eng = Convert.ToDateTime(datum_ein);


                                    if (datum_eng > datum)
                                    {
                                        datum = datum_eng;
                                        Datumsaenderung.Add(datum);
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("\nIhr Datum liegt nicht in der Zukunft!\nBitte versuchen Sie es erneut.\n");
                                        Console.ForegroundColor = ConsoleColor.White;

                                    }

                                } while (datum_eng < datum);
                            }
                            break;

                        case 2:
                            {
                                datum = datum.AddDays(rnd.Next(1, 20));
                                Datumsaenderung.Add(datum);
                            }
                            break;

                        default:
                            {
                                Console.WriteLine("\nFalsche Eingabe!!!");
                            }
                            break;
                    }


                    Console.Clear();


                    //*** Team 3 Start

                    //Berechnung der aktiven Dauerauftrage
                    for (int i = 0; i < KontoListe.Count; i++)
                    {
                        //Testvariable(n): tmpktonr, 
                        int tmpktonr = KontoListe[i].getKontonummer();
                        double dabetrag;
                        int dazielkto;


                        zielkundenkonto = KontoListe[0];

                        if (KontoListe[i].getDauerauftragAktiv()) //Ist bei diesem Konto ein DA aktiv?
                        {
                            dazielkto = KontoListe[i].getDauerauftragZielkonto();
                            zielkontonummer = KontoListe[i].getDauerauftragZielkontonummer();
                            dabetrag = KontoListe[i].getDauerauftragBetrag();
                            //lfdNrInZielKontoListe = 0;

                            if (ZielkundenkontoCheck(ref zielkontonummer, ref KontoListe, ref lfdNrInZielKontoListe)) //Ist Zielkonto noch vorhanden?
                            {
                                if (KontoListe[i].DauerauftragDatumCheck(datum)) //DatumCheck + Abbuchen + Löschen Datum aus liste
                                {
                                    KontoListe[dazielkto].DauerauftragGutschreiben(dabetrag, datum); //Betrag Zielkonto überweisen
                                }
                            }

                            else
                            {
                                Console.WriteLine("Zielkundenkonto ist nicht mehr vorhanden! DA wird gelöscht!");
                                KontoListe[i].DauerauftragLoeschen();
                            }
                        }
                    }

                    //*** Team 3 End

                    //*** Team 2 Start

                    DateTime date1 = Datumsaenderung[Datumsaenderung.Count - 1];
                    DateTime date2 = Datumsaenderung[Datumsaenderung.Count - 2];

                    for (int i = 0; i < KontoListe.Count; i++)
                    {
                        KontoListe[i].Zinsberechnung(date1, date2, datum);
                    }

                    //*** Team 2 End

                }

                //*** Team 1 END


            } while (bedienereingabe != "99");

            Console.WriteLine("Sie haben den digitalen Automaten beendet");

            Console.WriteLine("Zum Beenden betätigen Sie bitte eine beliebige Taste...");
            Console.ReadKey();

        }
    }
}
