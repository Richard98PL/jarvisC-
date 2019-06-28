using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Diagnostics;
using Microsoft.VisualStudio.Shell;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace DesktopApp1
{
    public partial class Form1 : Form
    {
        static string rogalski;
        static string nazwaPiosenkiWylosowanej;
        static string ostatnioUzytaKomenda = "zadna";
        static int[] indeksyPiosenekJuzWylosowanych = new int[0];
        static int ileLosowanoPiosenek = 0;
        private static readonly Random rnd = new Random();
        static SpeechSynthesizer JarvisVoice = new SpeechSynthesizer();
        SpeechRecognitionEngine recognizer;
        public Form1()
        {
                JarvisVoice.SelectVoice("Microsoft David Desktop");
            JarvisVoice.Rate = 2;
                recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
                recognizer.SetInputToDefaultAudioDevice();
                Grammar slownikJarvisa = MyGrammar();
                recognizer.LoadGrammar(slownikJarvisa);
                recognizer.RequestRecognizerUpdate();
                recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
                recognizer.RecognizeAsync(RecognizeMode.Multiple);

            
            InitializeComponent();
            JarvisVoice.Speak("Welcome home");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on the link below to continue learning how to build a desktop app using WinForms!
            System.Diagnostics.Process.Start("http://aka.ms/dotnet-get-started-desktop");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thanks!");
        }

         Grammar MyGrammar()
        {
            String[] slownikJarvisa = new String[]
            {   //muzyka najpierw
                "jarvis play some song",
                "jarvis play highway to hell",
                "jarvis play paranoid",
                "jarvis play sultans of swing",
                "jarvis play he is",
                "jarvis play he is a pirate",
                "jarvis play holiday",
                "jarvis play hard rock hallelujah",
                "jarvis play vendetta",
                "jarvis play bohemian rhapsody",
                "jarvis play dont stop me now",
                "jarvis play back from the dead",
                "jarvis play wash away",
                "jarvis play dance macabre",
                "jarvis play some music",
                //dotad muzyka
                "jarvis facebook",
                "jarvis youtube",
                "jarvis stop music",
                "jarvis shutdown",
                "jarvis youtube",
                "jarvis facebook",
                "jarvis google",
                "jarvis league of legends",
                "jarvis launch my webservice",
                "jarvis close browser",
                "jarvis restart",
                "jarvis hide",
                "jarvis desktop",
                "jarvis teamspeak",
                "bye bye jarvis",
                "jarvis are you there",
                "jarvis when were you created",
                "jarvis do you love me",
                "jarvis who created you",
                "jarvis shutdown computer in 5 minutes",
                "jarvis don't shutdown computer",
                "jarvis once again",
                "hello jarvis",
                "next",
                "yes please",
                "jarvis play some music",
                "jarvis last used command",
                "jarvis my playlist",
                "jarvis destroy the city",
                "jarvis execute order 66"


            };
            GrammarBuilder builderJarvisa = new GrammarBuilder(new Choices(slownikJarvisa));
            Grammar Jarvis = new Grammar(builderJarvisa);
            return Jarvis;

        }
        // Handle the SpeechRecognized event.  
        void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            string piosenka = "";
            rogalski = e.Result.Text;
            
            string obcietyRogalski = "";
            
            if (rogalski != "hello jarvis")
            {
                if (rogalski == "jarvis are you there")
                {
                    JarvisVoice.Speak("yes sir, i'm still here");
                    JarvisVoice.Speak("jarvis destroy the city");
                }
                else if (rogalski == "jarvis when were you created")
                    JarvisVoice.Speak("it was 20th day of may, year 2019 sir");
                else if (rogalski == "jarvis do you love me")
                    JarvisVoice.Speak("i don't have feelings sir, i'm sorry");
                else if (rogalski == "jarvis who created you")
                    JarvisVoice.Speak("it was tony stark, or you sir, i'm not sure right now, i feel very confused");
                else if (rogalski == "jarvis shutdown" || rogalski == "bye bye jarvis")
                {
                    JarvisVoice.Speak("Bye bye sir, have a good time!");
                    Application.Exit(); // nie wiem czemu nie wychodzi w tym momencie....
                }
                else if (rogalski == "next" || rogalski == "yes please") { }
                else if (rogalski == "jarvis last used command") { }
                else
                {
                    obcietyRogalski = rogalski.Substring(6);
                    JarvisVoice.Speak("yes sir, executing " + obcietyRogalski);
                }
            }
            else JarvisVoice.Speak("Welcome sir");
            if(rogalski != "jarvis last used command") ostatnioUzytaKomenda = rogalski;
            
            if(obcietyRogalski.Length>7)piosenka = obcietyRogalski.Substring(6);

            if(rogalski =="jarvis last used command")
            {
                if (ostatnioUzytaKomenda == "zadna")JarvisVoice.Speak("There is none");
                else JarvisVoice.Speak(ostatnioUzytaKomenda);
            }

            if(rogalski == "jarvis my playlist")
            {
                Process.Start("C:\\Users\\Ryszard\\Desktop\\Music\\my playlist.xspf");
            }

            if((rogalski == "jarvis play " + piosenka) && (rogalski != "jarvis play some song" )&& (rogalski != "jarvis play some music"))
            {
                Process.Start("C:\\Users\\Ryszard\\Desktop\\Music\\" +piosenka+ ".mp3");
            }
            if(rogalski == "jarvis play some song" || rogalski=="next" || rogalski == "yes please" || rogalski == "jarvis play some music")
            {
                JarvisPlaySomeSong();
            }

            if(ostatnioUzytaKomenda != "zadna" && rogalski == "jarvis once again")
            {
                JarvisVoice.Speak("yes sir, once again " + ostatnioUzytaKomenda);
            }
            if (rogalski == "jarvis stop music")
            {
                Process.Start("C:\\Users\\Ryszard\\Documents\\JarvisCommands\\killvlc.cmd");
            }

            if (rogalski == "jarvis youtube")
            {
                Process.Start("http://www.youtube.com");
            }

            if (rogalski == "jarvis facebook")
            {
                Process.Start("http://www.facebook.com");
            }

            if (rogalski == "jarvis google")
            {
                Process.Start("http://www.google.com");
            }

            if(rogalski == "jarvis league of legends")
            {
                Process.Start("C:\\Riot Games\\League of Legends\\LeagueClient.exe");
            }

            if(rogalski == "jarvis launch my webservice")
            {
                Process.Start("https://webservisrogalski.herokuapp.com/");
            }

            if(rogalski == "jarvis close browser")
            {
                Process.Start("C:\\Users\\Ryszard\\Documents\\JarvisCommands\\killbrowser.cmd");
            }

            if(rogalski == "jarvis restart")
            {
                Process.Start("C:\\Users\\Ryszard\\Desktop\\Jarvis.exe");
                this.Close();
            }

            if(rogalski == "jarvis hide")
            {
                this.WindowState = FormWindowState.Minimized;
            }

            if(rogalski == "jarvis desktop")
            {
                Process.Start("C:\\Users\\Ryszard\\Documents\\JarvisCommands\\desktop.cmd");
            }

            if (rogalski == "jarvis teamspeak")
            {
                Process.Start("C:\\Users\\Ryszard\\AppData\\Local\\TeamSpeak 3 Client\\ts3client_win64.exe");
            }

            if(rogalski == "jarvis shutdown computer in 5 minutes")
            {
                Process.Start("C:\\Users\\Ryszard\\Documents\\JarvisCommands\\shutdown5.cmd");
            }

            if(rogalski == "jarvis don't shutdown computer")
            {
                Process.Start("C:\\Users\\Ryszard\\Documents\\JarvisCommands\\shutdownabort.cmd");
            }

            if(rogalski=="jarvis destroy the city")
            {
                JarvisVoice.Speak("I wish you tasty pizza");
                
            }

           
            




        }
        static public string losujPiosenke()
        {

            int ktoraPiosenka;
            
            string[] Songs = new string[]
            {
                "highway to hell","dance macabre","paranoid","sultans of swing","he is",
                "square hammer","he is a pirate","holiday","hard rock hallelujah","vendetta",
                "bohemian rhapsody","vendetta","dont stop me now","back from the dead","wash away"
            };
            int ilePiosenek = Songs.Length;
            ktoraPiosenka = rnd.Next(0, Songs.Length - 1);
            while (indeksyPiosenekJuzWylosowanych.Contains(ktoraPiosenka))
            {
                ktoraPiosenka = rnd.Next(0, Songs.Length - 1);
                if (ileLosowanoPiosenek+1 == ilePiosenek)
                {
                    JarvisVoice.Speak("No more songs, sir");
                    return "highway to hell";

                }
            }
            ileLosowanoPiosenek++;
            System.Array.Resize(ref indeksyPiosenekJuzWylosowanych, ileLosowanoPiosenek);
            indeksyPiosenekJuzWylosowanych[ileLosowanoPiosenek-1] = ktoraPiosenka;
            

            return Songs[ktoraPiosenka];
        }
        static public void JarvisPlaySomeSong()
        {
            
            if (rogalski == "jarvis play some song" || rogalski == "next" || rogalski == "jarvis play some music")
            {
                nazwaPiosenkiWylosowanej = losujPiosenke();
                JarvisVoice.Speak("How about " + nazwaPiosenkiWylosowanej + "?");
            }
            if (rogalski == "yes please")
            {
                Array.Resize(ref indeksyPiosenekJuzWylosowanych, 0);
                ileLosowanoPiosenek = 0;
                try { Process.Start("C:\\Users\\Ryszard\\Desktop\\Music\\" + nazwaPiosenkiWylosowanej + ".mp3"); }
                catch (Exception e) { };
            }
        }
    }
}
