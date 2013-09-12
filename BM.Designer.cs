namespace BulkLoop
{
    partial class BMm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        
        /* Summary
         The function is used to dispose or clean up all the resources allocated, after the use.
         <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        */
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BMm));
            this.textData = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.iconvandisc = new System.Windows.Forms.Button();
            this.bewerkicon = new System.Windows.Forms.Button();
            this.setkruisje = new System.Windows.Forms.Button();
            this.analyse = new System.Windows.Forms.Button();
            this.werpnood = new System.Windows.Forms.Button();
            this.init_imager = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.toggleLED = new System.Windows.Forms.Button();
            this.werpwest = new System.Windows.Forms.Button();
            this.werpoost = new System.Windows.Forms.Button();
            this.werpzuid = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // textData
            // 
            resources.ApplyResources(this.textData, "textData");
            this.textData.Name = "textData";
            this.textData.TextChanged += new System.EventHandler(this.textData_TextChanged);
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // iconvandisc
            // 
            resources.ApplyResources(this.iconvandisc, "iconvandisc");
            this.iconvandisc.Name = "iconvandisc";
            this.iconvandisc.UseVisualStyleBackColor = true;
            this.iconvandisc.Click += new System.EventHandler(this.iconvandisc_Click);
            // 
            // bewerkicon
            // 
            resources.ApplyResources(this.bewerkicon, "bewerkicon");
            this.bewerkicon.Name = "bewerkicon";
            this.bewerkicon.UseVisualStyleBackColor = true;
            this.bewerkicon.Click += new System.EventHandler(this.bewerkicon_Click);
            // 
            // setkruisje
            // 
            resources.ApplyResources(this.setkruisje, "setkruisje");
            this.setkruisje.Name = "setkruisje";
            this.setkruisje.UseVisualStyleBackColor = true;
            this.setkruisje.Click += new System.EventHandler(this.setkruisje_Click);
            // 
            // analyse
            // 
            resources.ApplyResources(this.analyse, "analyse");
            this.analyse.Name = "analyse";
            this.analyse.UseVisualStyleBackColor = true;
            this.analyse.Click += new System.EventHandler(this.analyse_Click);
            // 
            // werpnood
            // 
            resources.ApplyResources(this.werpnood, "werpnood");
            this.werpnood.Name = "werpnood";
            this.werpnood.UseVisualStyleBackColor = true;
            this.werpnood.Click += new System.EventHandler(this.werpnood_Click);
            // 
            // init_imager
            // 
            resources.ApplyResources(this.init_imager, "init_imager");
            this.init_imager.Name = "init_imager";
            this.init_imager.UseVisualStyleBackColor = true;
            this.init_imager.Click += new System.EventHandler(this.init_imager_Click);
            // 
            // clear
            // 
            resources.ApplyResources(this.clear, "clear");
            this.clear.Name = "clear";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // toggleLED
            // 
            resources.ApplyResources(this.toggleLED, "toggleLED");
            this.toggleLED.Name = "toggleLED";
            this.toggleLED.UseVisualStyleBackColor = true;
            this.toggleLED.Click += new System.EventHandler(this.toggleLED_Click);
            // 
            // werpwest
            // 
            resources.ApplyResources(this.werpwest, "werpwest");
            this.werpwest.Name = "werpwest";
            this.werpwest.UseVisualStyleBackColor = true;
            this.werpwest.Click += new System.EventHandler(this.werpwest_Click);
            // 
            // werpoost
            // 
            resources.ApplyResources(this.werpoost, "werpoost");
            this.werpoost.Name = "werpoost";
            this.werpoost.UseVisualStyleBackColor = true;
            this.werpoost.Click += new System.EventHandler(this.werpoost_Click);
            // 
            // werpzuid
            // 
            resources.ApplyResources(this.werpzuid, "werpzuid");
            this.werpzuid.Name = "werpzuid";
            this.werpzuid.UseVisualStyleBackColor = true;
            this.werpzuid.Click += new System.EventHandler(this.werpzuid_Click);
            // 
            // BMm
            // 
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.werpzuid);
            this.Controls.Add(this.werpoost);
            this.Controls.Add(this.werpwest);
            this.Controls.Add(this.toggleLED);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.init_imager);
            this.Controls.Add(this.werpnood);
            this.Controls.Add(this.analyse);
            this.Controls.Add(this.setkruisje);
            this.Controls.Add(this.bewerkicon);
            this.Controls.Add(this.iconvandisc);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.textData);
            this.Name = "BMm";
            this.Load += new System.EventHandler(this.BM_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtData;
//        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button Noord;
//        public System.Windows.Forms.PictureBox pictureBox1;
//        private System.Windows.Forms.Button spelmolen;
        private System.Windows.Forms.Button button2;
//        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textData;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button iconvandisc;
        private System.Windows.Forms.Button bewerkicon;
        private System.Windows.Forms.Button setkruisje;
        private System.Windows.Forms.Button analyse;
        private System.Windows.Forms.Button werpnood;
        private System.Windows.Forms.Button init_imager;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Button toggleLED;
        private System.Windows.Forms.Button werpwest;
        private System.Windows.Forms.Button werpoost;
        private System.Windows.Forms.Button werpzuid;
    }
}

