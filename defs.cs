using System;
using System.Collections.Generic;
using System.Text;

namespace BulkLoop
{
    public class markeringen
    {
        public int grensli, grensre;
        public int wcentrx, wcentry, kcentrx, kcentry;
        public int kl, kr;          // definitieve afmetingen
        public int kdak, kvloer;    //
        public int wl, wr;          //
        public int wdak, wvloer;    //
        public bool BINNENKADER;
        public int dr = 80;
    }
    public class commandos
    {
        public int UITWERPEN = 160;
        public int IMAGER = 170;
        public int INIT = 1;
        public int NOORD = 0;
        public int OOST = 1;
        public int ZUID = 2;
        public int WEST = 3;
    }
    public class konstanten
    {
        public int SCHOPPEN = 39;
        public int HARTEN = 26;
        public int RUITEN = 13;
        public int KLAVEREN = 0;
        public int breedte = 128;
        public int hoogte = 128;
    }
    public struct spelinfo
    {
        public int nummer;
        public string dealer;
        public string kwetsbaarheid;
    } 
}

