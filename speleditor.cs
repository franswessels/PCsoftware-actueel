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
//        spelinfo spi = new spelinfo();
        string[] noord = new string[4];
        string[] oost = new string[4];
        string[] zuid = new string[4];
        string[] west = new string[4];
//        string kv;

        public speleditor()
        {
            kaartspel sp = new kaartspel();
            //noord[0] = "0000t00000002";
            //noord[1] = "0000000060000";
            //noord[2] = "0000008000032";
            //noord[3] = "0k0j098765000";
            //oost[0] = "0k00000765000";
            //oost[1] = "00q0008705402";
            //oost[2] = "0k00090060000";
            //oost[3] = "0000000000000";
            //zuid[0] = "a00j000000030";
            //zuid[1] = "a00jt00000000";
            //zuid[2] = "a0qjt00000000";
            //zuid[3] = "00q0t00000002";
            //west[0] = "00q0098000400";
            //west[1] = "0k00090000030";
            //west[2] = "0000000705400";
            //west[3] = "a000000000430";
            //kv = "zwwnnnnnznznwnnwwownozzzozowoonoowzzowznzwooowwnzwoz";
            //Console.WriteLine(noord[1]);
            //Console.WriteLine(noord[1][0]);
            //Console.WriteLine(noord[1][1]);
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

                // hallo
                string line;
                // dag
                bool skip = false;
                int start, stop, num, len, t;

                // Read the file and display it line by line.
                System.IO.StreamReader file = new System.IO.StreamReader(filnam);
                while ((line = file.ReadLine()) != null)
                {
//                    memo.Text += "\r\n";
//                    memo.Text += line;
                    if (line[0] == '{') skip = true;
                    //                    if (line[0] == '}') skip = false;
                    if (line[0] == '[' && !skip)
                    {

                        if (String.Compare(line, 1, "BOARD ", 0, 6) == 0)
                        {
                            t=0; while(line[t] != '"' && t<line.Length) t++; t++;   // vind begin van token
                            String token = geeftoken(line,'"',t);
                            memo.Text += "\r\n " + token;
                            registreerspelnummer(token); 
                        }
                        if (String.Compare(line, 1, "DEALER ", 0, 7) == 0)
                        {
                            t=0; while(line[t] != '"' && t<line.Length) t++; t++;   // vind begin van token
                            String token = geeftoken(line, '"',t);
                            memo.Text += "\r\n "+token;
                            registreerstarter(token);
                        }
                        if (String.Compare(line, 1, "VULNERABLE ", 0, 11) == 0)
                        {
                            t=0; while(line[t] != '"' && t<line.Length) t++; t++;   // vind begin van token
                            String token = geeftoken(line, '"',t);
                            memo.Text += "\r\n " + token;
                            registreerkwetsbaarheid(token);
                        }
                        if (String.Compare(line, 1, "DEAL ", 0, 5) == 0)
                        {
                            t=0; while(line[t] != '"' && t<line.Length) t++; t++;   // vind begin van token
                            String token = geeftoken(line, '"',t);
                            memo.Text += "\r\n " + token;
                            registreerverdeling(token);
                        }
                    }
                }
                file.Close();
            }
        }
        String geeftoken(String str, char kar,int start)
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
//                cond_act.spelnummer = nummer;
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
            //            cond_act.starter = kar;
            //            actafsp.start = kar;
        }
        void registreerkwetsbaarheid(String str)
        {
//    cond_act.nz_kwetsbaar=false;
//    cond_act.ow_kwetsbaar=false;
            if(String.Compare(str,"NONE")==0 ||String.Compare(str,"LOVE")==0 ||String.Compare(str,"-")==0)
            {
                memo.Text += "\r\nniemand kwetsbaar";
            }

            if(String.Compare(str,"NS")==0 )
            {
//                cond_act.nz_kwetsbaar=true;
                memo.Text += "\r\nNS kwetsbaar";
            }

            if(String.Compare(str,"EW")==0 )
            {
//                cond_act.ow_kwetsbaar=true;
                memo.Text += "\r\nEW kwetsbaar";
            }

            if(String.Compare(str,"ALL")==0 ||String.Compare(str,"BOTH")==0)
            {
//                cond_act.nz_kwetsbaar=true;
//                cond_act.ow_kwetsbaar=true;
                memo.Text += "\r\nallen kwetsbaar";
            }
        }
        void registreerverdeling(String str)
        {
            String refer = "NESW";
            String[] tokens=new String[4];
            byte sr = (byte)refer.IndexOf(str[0],0,refer.Length);
            int p = 0;
            memo.Text += "\r\n startrichting is " + sr;
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


     }
}
