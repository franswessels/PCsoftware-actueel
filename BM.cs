using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using CyUSB;
using System.IO;
using GemBox.Spreadsheet;
namespace BulkLoop
{

    public partial class BMm : Form
    {
        // data tbv USB communicatie
        CyUSBDevice         loopDevice  = null;
        USBDeviceList       usbDevices  = null;
        CyBulkEndPoint      inEndpoint  = null;
        CyBulkEndPoint      outEndpoint = null;

        Thread  tXfers;
        bool bRunning       = false;
//        int     value;
        long    inCount;
        int     ep_paar;
        const byte LED_OFF = 100;
        const byte LED_ON  = 101;
        bool error;                              // later implementeren
        const int XFERSIZE26   = 3;
        const int XFERSIZE48   = 3;             //voorlopig communicatiecode plus 2 parameters
//        const int XFERSIZE11   = 3;             
        byte[] outData = new byte[10];
        byte[]  inData = new byte[10];

        // These 2 needed for TransfersThread to update the UI
        delegate void UpdateUICallback();
        UpdateUICallback updateUI;
     
        // data tbv beedvorming
       public string filnam;
       int hoogte; int breedte;
       Bitmap pm;
//       ExcelFile ef;
       byte[,] pixar;
       byte[,] kopie;
       bool pm_ok = false;
       bool kopie_ok = false;
       markeringen mk = new markeringen();
       konstanten kk = new konstanten();
       commandos cc = new commandos();
       int xpos;
       int ypos;
       bool ledaan = false;
//       int kaartteller = 0;

        public BMm()
        {
            InitializeComponent();
//            btnClear.Visible = true;
//            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
//            ExcelFile ef = new ExcelFile();
            openpaintdelegate();
            hoogte = kk.hoogte;
            breedte = kk.breedte;
            xpos = 0;
            ypos = 0;
            pm = new Bitmap(kk.hoogte, kk.breedte);
            pixar = new byte[kk.hoogte, kk.breedte];
            kopie = new byte[kk.hoogte, kk.breedte];

            // Setup the callback routine for updating the UI
            updateUI    = new UpdateUICallback(StatusUpdate);

            // Create a list of CYUSB devices
            usbDevices  = new USBDeviceList(CyConst.DEVICES_CYUSB);

            // Adding event handlers for device attachment and device removal
            usbDevices.DeviceAttached += new EventHandler(usbDevices_DeviceAttached);
            usbDevices.DeviceRemoved += new EventHandler(usbDevices_DeviceRemoved);

            // The below function sets the device with particular VID and PId 
            // and searches for the device with the same VID and PID.
            setDevice();
        }
        
//// next is the event handler for Device removal event.
        void usbDevices_DeviceRemoved(object sender, EventArgs e)
        {
            setDevice();
        }
//// next is the event handler for Device Attachment event.
        void usbDevices_DeviceAttached(object sender, EventArgs e)
        {
            setDevice();
        }
//// next function sets the device, as the one having VID=04b4 and PID=1004
//// this will detect only the devices with the above VID,PID combinations
        public void setDevice()
        {          
            loopDevice = usbDevices[0x04b4, 0x1004] as CyUSBDevice;

            if (loopDevice != null)
//                Text = loopDevice.FriendlyName;
                Text = "Bridge Mill";
            else
                Text = "Bulkloop - no device";

        }
//// closing the open form
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If close was selected while running the loopback, shut it down.
            if (bRunning) stop_thread();

            if (usbDevices != null) usbDevices.Dispose();
        }

        private void setendpoint()
        {
            if (ep_paar == 11)
                if (loopDevice != null)
                {
                    outEndpoint = loopDevice.EndPointOf(0x01) as CyBulkEndPoint;
                    outEndpoint.TimeOut = 1000;
                    inEndpoint = loopDevice.EndPointOf(0x81) as CyBulkEndPoint;
                    inEndpoint.TimeOut = 1000;
                }
            if (ep_paar == 48)
                 if (loopDevice != null)
                 {
                     outEndpoint = loopDevice.EndPointOf(0x04) as CyBulkEndPoint;
                     inEndpoint = loopDevice.EndPointOf(0x88) as CyBulkEndPoint;
                     outEndpoint.TimeOut = 1000;
                     inEndpoint.TimeOut = 1000;
                 }
             if (ep_paar == 26)
                 if (loopDevice != null)
                 {
                     outEndpoint = loopDevice.EndPointOf(0x02) as CyBulkEndPoint;
                     inEndpoint = loopDevice.EndPointOf(0x86) as CyBulkEndPoint;
                     outEndpoint.TimeOut = 1000;
                     inEndpoint.TimeOut = 1000;
                 }
        }
//        public void start_EP1in_thread()
//        {
//            if (!bRunning)
//            {
////                inCount = 0;
//                bRunning = true;
//               //creates new thread
//                tXfers = new Thread(new ThreadStart(EP1in_thread));
//                tXfers.Priority = ThreadPriority.Highest;
//                //Starts the new thread
//                tXfers.Start(); // effectieve start
//            }
//        }
//        public void start_EP1_thread()
//        {
//            if (!bRunning)
//            {
////                inCount = 0;
//                bRunning = true;
//               //creates new thread
//                tXfers = new Thread(new ThreadStart(EP1uit_thread));
//                tXfers.Priority = ThreadPriority.Highest;
//                //Starts the new thread
//                tXfers.Start(); // effectieve start
//            }
//        }


        public void start_thread()
        {
            if (!bRunning)
            {
//                inCount = 0;
                bRunning = true;
               //creates new thread
                tXfers = new Thread(new ThreadStart(BM_Thread));
                tXfers.Priority = ThreadPriority.Highest;
                //Starts the new thread
                tXfers.Start(); // effectieve start
            }
        }
        public void stop_thread()
        {
                //Makes the thread stop and aborts the thread
//            if (bRunning)
//            {
                bRunning = false;

                if (tXfers == null) return;

                if (tXfers.IsAlive)
                {
                    tXfers.Abort();
                    tXfers.Join();
                    tXfers = null;
                }
//             }
//            txtData.Text += inCount.ToString();

        }
          
        public void StatusUpdate()
        {
//// This is the call back function for updating the UI(user interface) 
//// and is called from TransfersThread.
            textData.Text += " retour      ";

            if (ep_paar == 11)
                for (int i = 0; i < inCount; i++)
                {
                    textData.Text += inData[i].ToString();
                    textData.Text += "\t";
                }
            if (ep_paar == 26)
            for (int i = 0; i<inCount; i++)
            {
                textData.Text += inData[i].ToString();
                textData.Text += "\t";
            }
            if (ep_paar == 48)
            {
                for (int i = 0; i < inCount; i++)
                {
                    textData.Text += inData[i].ToString();
                    textData.Text += "\t";
                }
                textData.Text += "\r\n";
            }
            Refresh();

        }

        public void BM_Thread()
        {
            int  xferLen = XFERSIZE48;
            bool bResult = true;

            if (ep_paar == 26) xferLen = XFERSIZE26;
//            if (ep_paar == 11) xferLen = XFERSIZE11;

            // Loop stops if either an IN or OUT transfer fails
            for (; bRunning && bResult; )
            {
                {
                    //calls the XferData function for bulk transfer(OUT/IN) in the cyusb.dll
                    bResult = outEndpoint.XferData(ref outData, ref xferLen);


                    if (bResult)
                    {
                        //calls the XferData function for bulk transfer(OUT/IN) in the cyusb.dll
                        bResult = inEndpoint.XferData(ref inData, ref xferLen);
                        inCount = xferLen;
                    }

                    // Call StatusUpdate() in the main thread

                    Invoke(updateUI);
                    stop_thread();
                //kaartteller++;
                //if (kaartteller < 2) werp(kaartteller);
                //textData.Text += "\r\nzzz\r\n ";
                }

            }
            bRunning = false;

            // Call StatusUpdate() in the main thread
            Invoke(updateUI);
            stop_thread();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtData.Text = "leeg";
        }
        /*
         private void lees_plaatje_Click(object sender, EventArgs e)
        {
            txtData.Text = "start leescommando";
            inCount = 0;
            ep_paar = 26;
            setendpoint();
            start_thread();

        }

        private void kies()
        {
            int hg = kk.hoogte;
            int br =kk.breedte;
            byte volgendebyte;
            OpenFileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = "d:\\Cypress/software/kaarticons";
            fd.RestoreDirectory = true;
            if (fd.ShowDialog() == DialogResult.OK)
                   {
//                       file_ok = true;
                       filnam = fd.FileName;
//                       this.txtData.Text += "\r\n";
//                       this.txtData.Text += "filenaam: ";
//                       this.txtData.Text += filnam;

                       FileStream instream = new FileStream(filnam, FileMode.Open, FileAccess.Read);
                       for (int i = 0; i < hg; i++)
                           for (int j = 0; j < br; j++)
                           {
                               volgendebyte = (byte)instream.ReadByte();
                               if (volgendebyte != 80) pixar[i, j] = 0;
                               else pixar[i,j] = volgendebyte;
                           }
                       pm=pix2pm(pixar, hg, br);
                       instream.Close();
                       pm_ok = true;
                   }
            if (pm_ok && !display_ok) openpaintdelegate(); 
        }
        */
        private void openpaintdelegate()
        {
                pictureBox2.BackColor = Color.White;
                // Connect the Paint event of the PictureBox to the event handler method.
                pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(pictureBox_Paint);

                // Add the PictureBox control to the Form. 
                this.Controls.Add(pictureBox2);


//                        MessageBox.Show("mededeling");
        }
        private void pictureBox_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(pm, xpos, ypos);
            pictureBox2.Refresh();
        }
        private void BM_Load(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void BM_Load_1(object sender, EventArgs e)
        {

        }

        private void iconvandisc_Click(object sender, EventArgs e)
        {
            int hg = kk.hoogte;
            int br = kk.breedte;
            byte volgendebyte;
            OpenFileDialog fd = new OpenFileDialog();
            fd.RestoreDirectory = true;
            if (fd.ShowDialog() == DialogResult.OK)
                   {
                       filnam = fd.FileName;
                       textData.Text += "\r\nfilenaam: ";
                       textData.Text += filnam;

                       FileStream instream = new FileStream(filnam, FileMode.Open, FileAccess.Read);
                       for (int i = 0; i < hg; i++)
                           for (int j = 0; j < br; j++)

                           {
                               volgendebyte = (byte)instream.ReadByte();
                               if (volgendebyte != 80) pixar[i, j] = 0;
                               else pixar[i,j] = volgendebyte;
//                               if (j == 15) pixar[i, j] = 140; // test
                           }
                       instream.Close();
                       pm_ok = true;
                   }
                   pm = pix2pm(pixar, hg, br);

        }            
            
        private Bitmap pix2pm(byte[,] pixelarray, int hoogte, int breedte)
        {
            Bitmap uit = new Bitmap(hoogte, breedte);
            if (pixelarray != null)
            for (int i = 0; i < hoogte; i++)
                
                for (int j = 0; j < breedte; j++)
                {
                    if (pixelarray[i, j] < 80) { uit.SetPixel(j, i, Color.White); pixelarray[i, j] = 0; }
                    switch(pixelarray[i, j])
                    {
                        case 80 : uit.SetPixel(j, i, Color.Black); break;
                        case 140: uit.SetPixel(j, i, Color.Red); break;
                        case 254: uit.SetPixel(j, i, Color.Blue); break;
                        case 253: uit.SetPixel(j, i, Color.Green); break;
                        case 252: uit.SetPixel(j, i, Color.Yellow); break;
                        case 255: uit.SetPixel(j, i, Color.Gray); break;
                    }
                }
            return uit;
        }
        private void arraykopie(byte[,] inkom, byte[,] uitg, int hg, int br)
        {
            for (int h = 0; h < hg; h++)
                for (int v = 0; v < br; v++)
                    uitg[h, v] = inkom[h, v];
        }
        private void bewerkicon_Click(object sender, EventArgs e)
        {
//            if (kopie_ok)
            arraykopie(pixar, kopie, 128, 128);

            {
                int resultaat;
                resultaat = preproc(kopie);
                //            for (int y = 10; y < 90; y++) kopie[y, mk.grensli] = 140; //rood
                //            for (int y = 10; y < 90; y++) kopie[y, mk.grensre] = 140; //rood
                for (int y = mk.wdak; y < mk.wvloer; y++) kopie[y, mk.wl] = 140; //rood
                for (int y = mk.wdak; y < mk.wvloer; y++) kopie[y, mk.wr] = 140; //rood
                for (int y = mk.kdak; y < mk.kvloer; y++) kopie[y, mk.kl] = 140; //rood
                for (int y = mk.kdak; y < mk.kvloer; y++) kopie[y, mk.kr] = 140; //rood
                for (int x = mk.grensli; x < mk.grensre; x++) kopie[mk.kvloer, x] = 140; //rood
                for (int x = mk.grensli; x < mk.grensre; x++) kopie[mk.kdak, x] = 140; //rood
                for (int x = mk.grensli; x < mk.grensre; x++) kopie[mk.wvloer, x] = 140; //rood
                for (int x = mk.grensli; x < mk.grensre; x++) kopie[mk.wdak, x] = 140; //rood
                pm = pix2pm(kopie, 128, 128);
//                kopie_ok = false;
            }
        }
        public void setkruis()
        {
            int Yk = 30;
            int Xk = 30;
                for (int t = 0; t < 7; t++)
            {
                kopie[Yk + t - 2, Xk] = 140;
                kopie[Yk, Xk + t - 2] = 140;
            }
            pm = pix2pm(kopie, 128, 128);           
        }

        private void setkruisje_Click(object sender, EventArgs e)
        {
            setkruis();
        }

        private void analyse_Click(object sender, EventArgs e)
        {
            //            if (kopie_ok)
            arraykopie(pixar, kopie, 128, 128);

            {
                int resultaat;
                resultaat = welkekaart(kopie);
                textData.Text += "\r\n";
                textData.Text += "kaart is nummer: ";
                textData.Text += resultaat.ToString();
                pm = pix2pm(kopie, 128, 128);
                kopie_ok = false;
            }
        }

        private void display_output(int nn)
        {
            textData.Text += "output     ";
            for (int i = 0; i < nn; i++)
            { 
                textData.Text += outData[i].ToString();
                textData.Text += "\t";
            }
            textData.Text += "\r\n";
        }

        public void werp(int wr)
        {
            inCount = 0;
            ep_paar = 48;
            outData[0] = (byte)cc.UITWERPEN;
            outData[1] = (byte)wr;
            outData[2] = 3;
            display_output(3);
            setendpoint();
            start_thread();

        }
        private void werpnood_Click(object sender, EventArgs e)
        {

            textData.Text += "\r\nBMcommando via koppel 48\r\n ";
            werp(cc.NOORD);

        }


        private void init_imager_Click(object sender, EventArgs e)
        {
            textData.Text += "start init imager via koppel 48\r\n";
            inCount = 0;
            ep_paar = 48;
            outData[0] = (byte)cc.IMAGER;
            outData[1] = 2; // tijdelijk tbv testen ; later   (byte)cc.INIT;
            outData[2] = 3;
            display_output(3); 
            setendpoint();
            start_thread();

        }

        private void clear_Click(object sender, EventArgs e)
        {
            textData.Text = "leeg";
        }

        private void toggleLED_Click(object sender, EventArgs e)
        {
            textData.Text += "toggle LED via koppel 48\r\n";
            inCount = 0;
            ep_paar = 48;
            if(ledaan) outData[0] = 101; else outData[0]=100;
            ledaan = !ledaan;
            outData[1] = 5;
            outData[2] = 5;
            display_output(3);
            setendpoint();
            start_thread();

        }

        private void werpwest_Click(object sender, EventArgs e)
        {
            textData.Text += "\r\nBMcommando via koppel 48\r\n ";
            werp(cc.WEST);

        }

        private void werpoost_Click(object sender, EventArgs e)
        {
            textData.Text += "\r\nBMcommando via koppel 48\r\n ";
            werp(cc.OOST);

        }

        private void werpzuid_Click(object sender, EventArgs e)
        {
            textData.Text += "\r\nBMcommando via koppel 48\r\n ";
            werp(cc.ZUID);

        }

        private void textData_TextChanged(object sender, EventArgs e)
        {

        }
   }
}
