namespace BulkLoop
{

    public partial class BMm
    {
        public int welkekaart(byte[,] kopie)
        {

            int kaart, error, waarde;


            error = preproc(kopie);

            kaart = geefkleur(kopie);
            waarde = geefwaarde(kopie);
            //            textData.Text += "\r\n";
            //            textData.Text += waarde.ToString();


            kaart = kaart + waarde;
            return kaart;
        }
        public int geefkleur(byte[,] kopie)
        {
            int l, r, b, o;
            int wangen, band;
            int licht, voet, kop;

            l = mk.grensli;
            r = mk.grensre;
            b = mk.kdak;
            o = mk.kvloer;
            licht = telmiddenlicht(kopie, l, r, b, o);            // relatief in %; nog eens nader bekijken
            wangen = geefwangen(kopie, l, r, b, o);            // relatief in %
            band = geefband(kopie, l, r, b, o);                // relatief in %
            voet = geefvoet(kopie, l, r, b, o);                // absoluut; uitwaaiering
            //textData.Text += "\r\n";
            //textData.Text += licht;
            //textData.Text += "\t";
            //textData.Text += band.ToString();
            //textData.Text += "\t";
            //textData.Text += wangen.ToString();
            //textData.Text += "\t";
            //textData.Text += voet.ToString();
            //textData.Text += "\t";
            //            kop=geefkop(kopie, l, r, b, o);  // alternatief voor discrim klaver/ruiten

            //    case BINNENKADER:   // Carta Mundi Internationaal
            if (licht > 1) return kk.KLAVEREN;
            if (band < 50) return kk.HARTEN;
            if ((wangen + band - 10 * voet) < 130) return kk.SCHOPPEN;
            return kk.RUITEN;
            //      if((band)>77) return HARTEN;
            //      if(kop==1)return(KLAVEREN);
            //      if(voet==1)return(RUITEN);
            //      return(SCHOPPEN);

            //    case BUITENKADER:   // Resink; voorlopig; in ontwikkeling april '04
            //      if(licht > 17) return KLAVEREN;               // Kres
            //      if((band)>77) return HARTEN;
            //      if(voet==1) return(RUITEN);
            //      return(SCHOPPEN);
        }
        public int telmiddenlicht(byte[,] kopie, int l, int r, int b, int o)
        {
            //// geeft het percentage witte pixels
            int xx, yy, midx = (l + r) / 2, midy = (o + b) / 2, licht, totaal;
            int delx = (20 * (r - l)) / 100, dely = (20 * (o - b)) / 100;

            licht = 0; totaal = 0;

            for (yy = midy - dely; yy <= midy + dely; yy++)
                for (xx = midx - delx; xx <= midx + delx; xx++)
                {
                    totaal++;
                    if (kopie[yy, xx] < 80) licht++;
                }
            if (totaal <= 0) return (100);
            return ((100 * licht) / totaal);
        } // EINDE 'TELMIDDENLICHT'
        public int geefwangen(byte[,] kopie, int l, int r, int b, int o)
        {
            //// versie met accumulerende zwarting
            int t, tt, tel, telzwartlinks, telzwartrechts, totaal = o - b - 2;

            //// eerst meten aan de rechterkant
            tel = 0; telzwartrechts = 0; tt = r;
            while (tel <= 0 && tt > l)
            {
                for (t = b + 2; t < o - 1; t++)
                {
                    if (kopie[t, tt] < 80) tel++;
                }
                tt--;
            }
            //// tot zover naar links voor het vinden van de laatste zwarte pixels rechts
            if (tt > l)
            {
                tt--;                     // !!!!!!!!!!!
                for (t = b + 2; t < o - 1; t++)
                //// vertikale meetlijn op twee na laatste pixels rechts
                {
                    if (kopie[t, tt] < 80) telzwartrechts++;
                }

                //// nu meten aan de linkerkant
                tel = 0; tt = l; telzwartlinks = 0;
                while (tel <= 0 && tt < r)
                {
                    for (t = b + 2; t < o - 1; t++)
                    {
                        if (kopie[t, tt] < 80) tel++;
                    }
                    tt++;
                }
                if (tt < r)
                {
                    tt++;
                    for (t = b + 2; t < o - 1; t++)
                    {
                        if (kopie[t, tt] < 80) telzwartlinks++;
                    }
                }

                //// beveiliging tegen delen door nul
                if (totaal <= 0) return (0);
                if (telzwartlinks < telzwartrechts) return ((telzwartlinks * 100) / totaal);
                else return ((telzwartrechts * 100) / totaal);
            }
            return 0;
        }
        public int geefband(byte[,] kopie, int l, int r, int b, int o)
        {
            int t, tel, totaal;
            tel = 0; totaal = 0;
            for (t = l + 1; t < r; t++)
            {
                if (kopie[b + 4, t] < 80) tel++;
                totaal++;
            }
            if (totaal > 0) return ((tel * 100) / totaal);
            else return (-1);
        }
        public int geefvoet(byte[,] kopie, int l, int r, int b, int o)
        {
            //#define NRIJEN 5 ??
            int t, tt, tel, tel2, telstart;
            t = o; telstart = 0;
            while (telstart <= 0 && t > (o + b) / 2)
            {
                for (tt = l + 1; tt < r - 1; tt++)
                {
                    if (kopie[t, tt] < 80) telstart++;
                }
                t--;
                //// wijst nu naar de voorlaatste rij met zwarte pixels
                //// hier begint opwaarts het onderzoek van de voet
                //// wordt deze breder of niet?
            }
            tel = 0;
            for (tt = l + 1; tt < r - 1; tt++)
            {
                if (kopie[t, tt] < 80) tel++;
            }
            if (tel < telstart) return (0);
            telstart = tel;
            tel = 0; t--;
            for (tt = l + 1; tt < r - 1; tt++)
            {
                if (kopie[t, tt] < 80) tel++;
            }
            if (tel < telstart) return (0);
            telstart = tel;
            tel = 0; t--;
            for (tt = l + 1; tt < r - 1; tt++)
            {
                if (kopie[t, tt] < 80) tel++;
            }
            if (tel < telstart) return (0);
            else return (1);
        }
        public int geefwaarde(byte[,] kopie) // waarde herkenning
        {
            int il90, ilm, irb, irm, iro, ir90, ir10;
            int wb = mk.wr - mk.wl;
            int hh = mk.wvloer - mk.wdak;

            il90 = insn_links(kopie, 90, 1); // links boven
            ilm = insn_links(kopie, 40, 2);
            ir90 = insn_rechts(kopie, 90, 2);
            irb = insn_rechts(kopie, 70, 1);
            irm = insn_rechts(kopie, 40, 1);
            iro = insn_rechts(kopie, 20, 1);
            ir10 = insn_rechts(kopie, 10, 1);
            //textData.Text += "\r\n";
            //textData.Text += "Breedte= ";
            //textData.Text += (mk.wr - mk.wl).ToString();
            //  textData.Text += "\r\n";
            //  textData.Text += 90.ToString();
            //  textData.Text += "\t";
            //  textData.Text += il90.ToString();
            //  //textData.Text += "\r\n";
            //  //textData.Text += 70.ToString();
            //  //textData.Text += "\t";
            //  //textData.Text += ilb.ToString();
            //  textData.Text += "\r\n";
            //  textData.Text += 40.ToString();
            //  textData.Text += "\t";
            //  textData.Text += ilm.ToString();
            //  //textData.Text += "\r\n";
            //  //textData.Text += 20.ToString();
            //  //textData.Text += "\t";
            //  //textData.Text += ilo.ToString();
            //  //textData.Text += "\r\n";
            //  //textData.Text += 10.ToString();
            //  //textData.Text += "\t";
            //  //textData.Text += il10.ToString();

            //  textData.Text += "\r\n  ";
            //  textData.Text += 90.ToString();
            //  textData.Text += "\t";
            //  textData.Text += ir90.ToString();
            //  textData.Text += "\r\n  ";
            //  textData.Text += 70.ToString();
            //  textData.Text += "\t";
            //  textData.Text += irb.ToString();
            //  textData.Text += "\r\n  ";
            //  textData.Text += 40.ToString();
            //  textData.Text += "\t";
            //  textData.Text += irm.ToString();
            //  textData.Text += "\r\n  ";
            //  textData.Text += 20.ToString();
            //  textData.Text += "\t";
            //  textData.Text += iro.ToString();
            //  textData.Text += "\r\n  ";
            //  textData.Text += 10.ToString();
            //  textData.Text += "\t";
            //  textData.Text += ir10.ToString();

            ////////////////////// begin discriminatie //////////////////
            ////////////////////// plaatjes bridgebond ///////////////
            //// Honeurs worden herkend aan omkadering
            //// B als insnede ilb voldoende groot is (>50%)
            //// H als insnede irm groter dan 25% breedte waardekader
            ////   alternatief B >> V

            if (mk.BINNENKADER) // Bridgebond
            {
                if (((ilm * 100) / wb) > 44) return 9;  // =BOER
                if ((((irm + iro) * 100) / wb) > 25) return 11; // =HEER
                return 10; // = VROUW
            }
            //// nu onderzoek lagere kaarten (en aas van BB):
            if (((iro * 100) / wb) > 40) return 5;  // =ZEVEN
            if (((il90 * 100) / wb) > 45) return 2;  // =VIER
            if ((((il90 + ir90) * 100) / wb) > 60) return 12; // =AAS
            if ((((ilm + ir90) * 100) / wb) > 60) return 3;  // =VIJF
            if (((ilm * 100) / wb) > 50) return 1;  // =DRIE
            if (((irb * 100) / wb) > 50) return 4;  // =ZES
            if (((irm * 100) / wb) > 40) return 0;  // =TWEE
            if (((ilm * 100) / wb) > 50) return 7;  // =NEGEN
            if (((il90 + ilm + ir90 + irb + irm + ir10) * 100) / wb < 30) return 8;  // =TIEN
            return 6;  // =ACHT
        }
        public int insn_links(byte[,] kopie, int hoogte, int delta)   // telt lichte pixels
        {
            //// neemt langste van een aantal witte rijen vanaf links
            int wv0, wv1, wv2, meta, t0, t1, pix;

            wv0 = 0;
            wv2 = mk.wvloer - ((hoogte * (mk.wvloer - mk.wdak)) / 100);
            for (t0 = wv2 - delta; t0 <= wv2 + delta; t0++)
                if ((t0 < mk.wvloer) && (t0 > mk.wdak))
                {
                    meta = 1; wv1 = 0; t1 = mk.wl + 1;
                    while ((meta == 1) && (t1 < mk.wr))
                    {
                        meta = 0;
                        pix = kopie[t0, t1++];
                        if (pix < 0) return (pix);
                        if (pix < 80)
                        {
                            //                         wv1++; meta = 1; kopie[t0, t1 - 1] = 140;
                            wv1++; meta = 1;
                        }
                    }
                    if (wv0 < wv1) wv0 = wv1;
                }
            return wv0;
        }
        public int insn_rechts(byte[,] kopie, int hoogte, int delta)  // telt lichte pixels
        {
            //// neemt langste van een aantal rijen vanaf rechts
            int t0, t1, wv0, wv1, wv2, meta, pix;
            wv0 = 0;
            wv2 = mk.wvloer - ((hoogte * (mk.wvloer - mk.wdak)) / 100);
            for (t0 = wv2 - delta; t0 <= wv2 + delta; t0++)
            {
                meta = 1; wv1 = 0; t1 = mk.wr - 1;
                while ((meta == 1) && (t1 > mk.wl))
                {
                    meta = 0;
                    pix = kopie[t0, t1--];
                    if (pix < 0) return (pix);
                    if (pix < 80)
                    {
                        //                      wv1++; meta=1; kopie[t0,t1+1]=140;
                        wv1++; meta = 1;
                    }
                }
                if (wv0 < wv1) wv0 = wv1;
            }
            return wv0;
        }

        public int preproc(byte[,] kopie) // nieuwe opzet
        {
            //// zoek grenslijn links
            int tel = 3;
            int xpos = 30;
            while (tel > 2 && xpos >= 0)
            {
                tel = 0;
                xpos--;
                for (int y = 20; y < 80; y++)
                {
                    if (xpos >= 0 && xpos < 128)
                    {
                        if (kopie[y, xpos] >= 80) tel++;
                    }
                }
            }
            mk.grensli = xpos;
            mk.grensre = 40; // voorlopig
            //// is er een binnenkaderlijn?
            tel = 0;
            xpos = mk.grensli;
            mk.BINNENKADER = false;
            while (tel < 2 && xpos < 55)
            {
                xpos++;
                {
                    if (xpos >= 0 && xpos < 128)
                    {
                        if (kopie[100, xpos] >= 80) tel++;
                        if (kopie[101, xpos] >= 80) tel++;
                        kopie[100, xpos] = 140;
                    }
                }
            }
            if (tel >= 2) mk.BINNENKADER = true;
            mk.grensre = xpos;  // voorlopig 
            if (xpos < 55)
            {
                mk.grensre++;
                for (ypos = 100; ypos > 0; ypos--)
                {
                    if (xpos >= 0 && xpos < 128)
                    {
                        kopie[ypos, xpos] = 0;
                        kopie[ypos, xpos + 1] = 0; // zet even twee blanco vertikale lijnen
                    }
                }
            }

            //// zoek rechter begrenzing
            {
                tel = 0;
                xpos = mk.grensre;
                while (tel < 3 && xpos > mk.grensli)
                {
                    tel = 0;
                    xpos--;
                    for (int y = 10; y < 90; y++)
                    {
                        if (xpos >= 0 && xpos < 128)
                        {
                            if (kopie[y, xpos] >= 80) tel++;
                        }
                    }
                }
                mk.grensre = xpos;

            }
            //// zoek ondergrens kleur-icon
            {
                tel = 0;
                ypos = 99;
                while (tel < 3 && xpos < 128)
                {
                    tel = 0;
                    ypos--;
                    for (int x = mk.grensli + 1; x < mk.grensre; x++)
                    {
                        if (ypos >= 0 && ypos < 128)
                        {
                            if (kopie[ypos, x] >= 80) tel++;
                        }
                    }
                }
                mk.kvloer = ypos;

            }
            //// zoek bovenkant kleur-icon
            {
                tel = 3;
                ypos = mk.kvloer;
                while (tel > 2 && xpos >= 0)
                {
                    tel = 0;
                    ypos--;
                    for (int x = mk.grensli + 1; x < mk.grensre; x++)
                    {
                        if (ypos >= 0 && ypos < 128)
                        {
                            if (kopie[ypos, x] >= 80) tel++;
                        }
                    }
                }
                mk.kdak = ypos;

            }
            //// zoek ondergrens waarde-icon
            {
                tel = 0;
                ypos = mk.kdak;
                while (tel < 3 && xpos < 128)
                {
                    tel = 0;
                    ypos--;
                    for (int x = mk.grensli + 1; x < mk.grensre; x++)
                    {
                        if (ypos >= 0 && ypos < 128)
                        {
                            if (kopie[ypos, x] >= 80) tel++;
                        }
                    }
                }
                mk.wvloer = ypos;


            }
            //// zoek bovenkant kleur-icon
            {
                tel = 3;
                ypos = mk.wvloer;
                while (tel > 2 && xpos >= 0)
                {
                    tel = 0;
                    ypos--;
                    for (int x = mk.grensli + 1; x < mk.grensre; x++)
                    {
                        if (ypos >= 0 && ypos < 128)
                        {
                            if (kopie[ypos, x] >= 80) tel++;
                        }
                    }
                }
                mk.wdak = ypos;
                if (ypos < 0 || ypos > 100) mk.wdak = 10;
            }
            //// zoek linker begrenzing waarde-icon
            {
                tel = 0;
                xpos = mk.grensli - 1;
                while (tel < 3 && xpos < mk.grensre)
                {
                    tel = 0;
                    xpos++;
                    for (int y = mk.wdak; y < mk.wvloer; y++)
                    {
                        if (xpos >= 0 && xpos < 128)
                        {
                            if (kopie[y, xpos] >= 80) tel++;
                        }
                    }
                }
                mk.wl = xpos;

            }
            //// zoek rechter begrenzing waarde-icon
            {
                tel = 0;
                xpos = mk.grensre + 1;
                while (tel < 3 && xpos > mk.grensli)
                {
                    tel = 0;
                    xpos--;
                    for (int y = mk.wdak; y < mk.wvloer; y++)
                    {
                        if (xpos >= 0 && xpos < 128)
                        {
                            if (kopie[y, xpos] >= 80) tel++;
                        }
                    }
                }
                mk.wr = xpos;

            }
            //// zoek linker begrenzing kleur-icon
            {
                tel = 0;
                xpos = mk.grensli - 1;
                while (tel < 3 && xpos < mk.grensre)
                {
                    tel = 0;
                    xpos++;
                    for (int y = mk.kdak; y < mk.kvloer; y++)
                    {
                        if (xpos >= 0 && xpos < 128)
                        {
                            if (kopie[y, xpos] >= 80) tel++;
                        }
                    }
                }
                mk.kl = xpos;

            }
            //// zoek rechter begrenzing kleur-icon
            {
                tel = 0;
                xpos = mk.grensre + 1;
                while (tel < 3 && xpos > mk.grensli)
                {
                    tel = 0;
                    xpos--;
                    for (int y = mk.kdak; y < mk.kvloer; y++)
                    {
                        if (xpos >= 0 && xpos < 128)
                        {
                            if (kopie[y, xpos] >= 80) tel++;
                        }
                    }
                }
                mk.kr = xpos;

            }

            //// resumee grenslijnen
            //for (int y = 10; y < 90; y++) kopie[y, mk.grensli] = 140; //rood
            //for (int y = 10; y < 90; y++) kopie[y, mk.grensre] = 140; //rood
            //for (int x = mk.grensli; x < mk.grensre; x++) kopie[mk.kleuron, x] = 140; //rood
            //for (int x = mk.grensli; x < mk.grensre; x++) kopie[mk.kleurbo, x] = 140; //rood
            //for (int x = mk.grensli; x < mk.grensre; x++) kopie[mk.waardeon, x] = 140; //rood
            //for (int x = mk.grensli; x < mk.grensre; x++) kopie[mk.waardebo, x] = 140; //rood

            //// bereken centra
            mk.wcentrx = (mk.grensli + mk.grensre) / 2;
            mk.wcentry = (mk.wdak + mk.wvloer) / 2;
            mk.kcentrx = (mk.grensli + mk.grensre) / 2;
            mk.kcentry = (mk.kdak + mk.kvloer) / 2;

            //textData.Text += mk.wcentrx.ToString();
            //textData.Text += "\t";
            //textData.Text += mk.wcentry.ToString();
            //textData.Text += "\r\n";
            //textData.Text += mk.kcentrx.ToString();
            //textData.Text += "\t";
            //textData.Text += mk.kcentry.ToString();
            //textData.Text += "\r\n";
            //textData.Text += "\r\n";

            return 0;
        }
    }
}
