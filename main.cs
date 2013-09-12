using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BulkLoop
{
    public partial class main : Form
    {
        BMm usb_linker;
        speleditor spel;
        bool usb_linker_actief = false;
        bool speleditor_actief = false;
        public main()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usb_linker_actief == true)
            {
                usb_linker.Close();
                usb_linker_actief = false;
            }
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (usb_linker_actief == false)
            {
                usb_linker = new BMm();
                usb_linker_actief = true;
                usb_linker.Show();
            }
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (speleditor_actief == false)
            {
                spel = new speleditor();
                speleditor_actief = true;
                spel.Show();
            }

        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (speleditor_actief == true)
            {
                spel.Close();
                speleditor_actief = false;
            }

        }

        private void main_Load(object sender, EventArgs e)
        {

        }

        private void usb_linkToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
