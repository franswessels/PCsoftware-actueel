namespace BulkLoop
{
    partial class speleditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.bestandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transportenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spelNaarBMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spelVanBMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bMactieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sorteerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memo = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bestandToolStripMenuItem,
            this.transportenToolStripMenuItem,
            this.bMactieToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(937, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // bestandToolStripMenuItem
            // 
            this.bestandToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.bestandToolStripMenuItem.Name = "bestandToolStripMenuItem";
            this.bestandToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.bestandToolStripMenuItem.Text = "file";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.newToolStripMenuItem.Text = "new";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.openToolStripMenuItem.Text = "open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.saveToolStripMenuItem.Text = "save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.saveAsToolStripMenuItem.Text = "save as";
            // 
            // transportenToolStripMenuItem
            // 
            this.transportenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spelNaarBMToolStripMenuItem,
            this.spelVanBMToolStripMenuItem});
            this.transportenToolStripMenuItem.Name = "transportenToolStripMenuItem";
            this.transportenToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.transportenToolStripMenuItem.Text = "transport";
            // 
            // spelNaarBMToolStripMenuItem
            // 
            this.spelNaarBMToolStripMenuItem.Name = "spelNaarBMToolStripMenuItem";
            this.spelNaarBMToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.spelNaarBMToolStripMenuItem.Text = "spel naar BM";
            // 
            // spelVanBMToolStripMenuItem
            // 
            this.spelVanBMToolStripMenuItem.Name = "spelVanBMToolStripMenuItem";
            this.spelVanBMToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.spelVanBMToolStripMenuItem.Text = "spel van BM";
            // 
            // bMactieToolStripMenuItem
            // 
            this.bMactieToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sorteerToolStripMenuItem});
            this.bMactieToolStripMenuItem.Name = "bMactieToolStripMenuItem";
            this.bMactieToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.bMactieToolStripMenuItem.Text = "BMactie";
            // 
            // sorteerToolStripMenuItem
            // 
            this.sorteerToolStripMenuItem.Name = "sorteerToolStripMenuItem";
            this.sorteerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sorteerToolStripMenuItem.Text = "sorteer";
            this.sorteerToolStripMenuItem.Click += new System.EventHandler(this.sorteerToolStripMenuItem_Click);
            // 
            // memo
            // 
            this.memo.Location = new System.Drawing.Point(115, 36);
            this.memo.Multiline = true;
            this.memo.Name = "memo";
            this.memo.Size = new System.Drawing.Size(506, 200);
            this.memo.TabIndex = 1;
            this.memo.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // speleditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(937, 474);
            this.ControlBox = false;
            this.Controls.Add(this.memo);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.Black;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "speleditor";
            this.Text = "speleditor";
            this.Load += new System.EventHandler(this.speleditor_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem bestandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transportenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spelNaarBMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spelVanBMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bMactieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sorteerToolStripMenuItem;
        private System.Windows.Forms.TextBox memo;
    }
}