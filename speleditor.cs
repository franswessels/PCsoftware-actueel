using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using System;
using System.IO;
//using System.Text;


namespace BulkLoop
{
    public partial class speleditor : Form
    {
        public string filnam;
        public byte[] os;
        deal spel;
        string[] noord = new string[4];
        string[] oost = new string[4];
        string[] zuid = new string[4];
        string[] west = new string[4];
        String refs = "23456789TJQKA";



        public speleditor()
        {
            os = new byte[52];
            spel = new deal();
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            //            fd.InitialDirectory = "D:\\BridgeMill statisch deel/cypress";
            fd.RestoreDirectory = true;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                filnam = fd.FileName;
                memo.Text += "\r\nfilenaam: ";
                memo.Text += filnam;

                string line;
                bool skip = false;
                int t;

                // Read the file and display it line by line.
                System.IO.StreamReader file = new System.IO.StreamReader(filnam);
                while ((line = file.ReadLine()) != null)
                {
                    if (line[0] == '{') skip = true;
                    //                    if (line[0] == '}') skip = false;
                    if (line[0] == '[' && !skip)
                    {

                        if (String.Compare(line, 1, "BOARD ", 0, 6) == 0)
                        {
                            t=0; while(line[t] != '"' && t<line.Length) t++; t++;   // vind begin van token
                            String token = geeftoken(line,'"',t);
                            registreerspelnummer(token); 
                        }
                        if (String.Compare(line, 1, "DEALER ", 0, 7) == 0)
                        {
                            t=0; while(line[t] != '"' && t<line.Length) t++; t++;   // vind begin van token
                            String token = geeftoken(line, '"',t);
                            registreerstarter(token);
                        }
                        if (String.Compare(line, 1, "VULNERABLE ", 0, 11) == 0)
                        {
                            t=0; while(line[t] != '"' && t<line.Length) t++; t++;   // vind begin van token
                            String token = geeftoken(line, '"',t);
                            registreerkwetsbaarheid(token);
                        }
                        if (String.Compare(line, 1, "DEAL ", 0, 5) == 0)
                        {
                            t=0; while(line[t] != '"' && t<line.Length) t++; t++;   // vind begin van token
                            String token = geeftoken(line, '"',t);
                            registreerverdeling(token);
                        }
                    }
                }
                file.Close();
            }
            memo.Text += "\r\n spelnummer: " + spel.BOARD;
            memo.Text += "\r\n gever:" + spel.DEALER;
            memo.Text += "\r\n kwetsbaar:" + spel.VULN + "\r\n verdeelstring: \r\n";
            for (int t = 0; t < 52; t++)
            {
                memo.Text += spel.HANDS[t];
            }
        }
        String geeftoken(String str, char kar, int start)
        {
            int t=start;
            String result = "";
            while(str[t] != kar && t<str.Length)
            {
                result = String.Concat(result,str.Substring(t,1));
                t++;
            }
            return result;
        }

        void registreerspelnummer(String str)
            {
                int num = 0; int t = 0; ;
                for (t = 0; t < str.Length; t++)
                    num = 10 * num + (int)(str[t] - '0');
                memo.Text += "\r\n Spelnummer is " + num;
                spel.BOARD = num;
            }
        void registreerstarter(String str)
        {
            int kar;

            kar = str[0];
            if (kar == 'N') kar = 0;
            if (kar == 'E') kar = 1;
            if (kar == 'S') kar = 2;
            if (kar == 'W') kar = 3;
            memo.Text += "\r\n Dealer is " + kar;
            spel.DEALER = kar;
            //            cond_act.starter = kar;
            //            actafsp.start = kar;
        }
        void registreerkwetsbaarheid(String str)
        {
//    cond_act.nz_kwetsbaar=false;
//    cond_act.ow_kwetsbaar=false;
            if(String.Compare(str,"NONE")==0 ||String.Compare(str,"LOVE")==0 ||String.Compare(str,"-")==0)
            {
                memo.Text += "\r\n niemand kwetsbaar";
                spel.VULN = 0;
            }

            if(String.Compare(str,"NS")==0 )
            {
//                cond_act.nz_kwetsbaar=true;
                memo.Text += "\r\n NS kwetsbaar";
                spel.VULN = 1;
            }

            if(String.Compare(str,"EW")==0 )
            {
//                cond_act.ow_kwetsbaar=true;
                memo.Text += "\r\n EW kwetsbaar";
                spel.VULN = 2;
            }

            if(String.Compare(str,"ALL")==0 ||String.Compare(str,"BOTH")==0)
            {
//                cond_act.nz_kwetsbaar=true;
//                cond_act.ow_kwetsbaar=true;
                memo.Text += "\r\n allen kwetsbaar";
                spel.VULN = 3;
            }
        }
        void registreerverdeling(String str)
        {
            String refer = "NESW";
            String[] tokens = new String[4];
            String hand;
            byte sr = (byte)refer.IndexOf(str[0],0,refer.Length);
            int p = 0;
            memo.Text += "\r\n eerste hand " + sr;
            while (str[p] != ':' && p < 81) p++;
            for(int t=0; t<4; t++)
            {
                p++;
                while (str[p] != ' ' && p < str.Length)
                {
                    tokens[t] = String.Concat(tokens[t], str.Substring(p, 1));
                    p++;
                }
                memo.Text += "\r\n test: " + tokens[t];
            }
            for (int t = 0; t < 4; t++)     // vier handen
            {
                hand = String.Copy(tokens[(int)t]);
//                memo.Text += "\r\n " + hand;
                p = 0;
                int kl = 0;
                int num = 0;
                char kar;

                    for(p=0; p<hand.Length; p++)
                    {
                        kar = hand[p];
                        if (kar != '.')
                        {
                            num = (3 - kl) * 13 + refs.IndexOf(kar, 0, refs.Length);
//                            memo.Text += "\r\n " + t + p + " " + num;
                            os[num] = (byte)((t+sr)%4);
                            spel.HANDS[num] = os[num];
                        }
                        else kl++;
                    }

                 memo.Text += "\r\n";
                for (p = 0; p < os.Length; p++) memo.Text += os[p];
            }
        }


        private void saveToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

        }

        private void speleditor_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void sorteerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String wind = "NOSW" ;
            memo.Text += "\r\n karakter: "+ wind.IndexOf("W",0,4);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


     }
    public class deal
    {
        private int spelnummer=-1;
        private int gever=-1;
        private int kwetsbaar=0;
        private byte[] verdeling = new byte[52];

        public int BOARD
        {
            get {return spelnummer;}
            set {spelnummer = value;}
        }
        public int DEALER
        {
            get {return gever;}
            set {gever = value;}
        }
        public int VULN
        {
            get {return kwetsbaar;}
            set { kwetsbaar = value;}
        }
        public byte[] HANDS
        {
            get {return verdeling;}
            set {verdeling = value;}
        }   
    }
}
