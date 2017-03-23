namespace automateDRH
{
    partial class Home
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
            this.list_log = new System.Windows.Forms.ListView();
            this.start = new System.Windows.Forms.Button();
            this.parseWorker = new System.ComponentModel.BackgroundWorker();
            this.progressbar = new System.Windows.Forms.ProgressBar();
            this.picture_start = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changerConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aProposToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Check_mysql = new System.Windows.Forms.Button();
            this.Check_path = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picture_start)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // list_log
            // 
            this.list_log.Location = new System.Drawing.Point(12, 27);
            this.list_log.Name = "list_log";
            this.list_log.Size = new System.Drawing.Size(563, 320);
            this.list_log.TabIndex = 0;
            this.list_log.UseCompatibleStateImageBehavior = false;
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(581, 9);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 1;
            this.start.Text = "Start";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // parseWorker
            // 
            this.parseWorker.WorkerReportsProgress = true;
            this.parseWorker.WorkerSupportsCancellation = true;
            this.parseWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.parseWorker_DoWork);
            this.parseWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.parseWorker_ProgressChanged);
            this.parseWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.parseWorker_RunWorkerCompleted);
            // 
            // progressbar
            // 
            this.progressbar.Location = new System.Drawing.Point(581, 69);
            this.progressbar.Name = "progressbar";
            this.progressbar.Size = new System.Drawing.Size(100, 23);
            this.progressbar.TabIndex = 3;
            // 
            // picture_start
            // 
            this.picture_start.BackColor = System.Drawing.Color.Red;
            this.picture_start.Location = new System.Drawing.Point(581, 38);
            this.picture_start.Name = "picture_start";
            this.picture_start.Size = new System.Drawing.Size(26, 25);
            this.picture_start.TabIndex = 4;
            this.picture_start.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(581, 98);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Rapport Log";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.log_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem,
            this.plusToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(694, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changerConfigToolStripMenuItem});
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.configurationToolStripMenuItem.Text = "Configuration";
            // 
            // changerConfigToolStripMenuItem
            // 
            this.changerConfigToolStripMenuItem.Name = "changerConfigToolStripMenuItem";
            this.changerConfigToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.changerConfigToolStripMenuItem.Text = "Changer Config";
            this.changerConfigToolStripMenuItem.Click += new System.EventHandler(this.changerConfigToolStripMenuItem_Click);
            // 
            // plusToolStripMenuItem
            // 
            this.plusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aProposToolStripMenuItem});
            this.plusToolStripMenuItem.Name = "plusToolStripMenuItem";
            this.plusToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.plusToolStripMenuItem.Text = "Plus";
            // 
            // aProposToolStripMenuItem
            // 
            this.aProposToolStripMenuItem.Name = "aProposToolStripMenuItem";
            this.aProposToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.aProposToolStripMenuItem.Text = "A propos";
            this.aProposToolStripMenuItem.Click += new System.EventHandler(this.aProposToolStripMenuItem_Click);
            // 
            // Check_mysql
            // 
            this.Check_mysql.Location = new System.Drawing.Point(581, 127);
            this.Check_mysql.Name = "Check_mysql";
            this.Check_mysql.Size = new System.Drawing.Size(75, 23);
            this.Check_mysql.TabIndex = 8;
            this.Check_mysql.Text = "Test Mysql";
            this.Check_mysql.UseVisualStyleBackColor = true;
            this.Check_mysql.Click += new System.EventHandler(this.Check_mysql_Click);
            // 
            // Check_path
            // 
            this.Check_path.Location = new System.Drawing.Point(581, 156);
            this.Check_path.Name = "Check_path";
            this.Check_path.Size = new System.Drawing.Size(75, 23);
            this.Check_path.TabIndex = 9;
            this.Check_path.Text = "Test chemin";
            this.Check_path.UseVisualStyleBackColor = true;
            this.Check_path.Click += new System.EventHandler(this.Check_path_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 359);
            this.Controls.Add(this.Check_path);
            this.Controls.Add(this.Check_mysql);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.picture_start);
            this.Controls.Add(this.progressbar);
            this.Controls.Add(this.start);
            this.Controls.Add(this.list_log);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Home";
            this.Text = "Home";
            ((System.ComponentModel.ISupportInitialize)(this.picture_start)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView list_log;
        private System.Windows.Forms.Button start;
        private System.ComponentModel.BackgroundWorker parseWorker;
        private System.Windows.Forms.ProgressBar progressbar;
        private System.Windows.Forms.PictureBox picture_start;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changerConfigToolStripMenuItem;
        private System.Windows.Forms.Button Check_mysql;
        private System.Windows.Forms.ToolStripMenuItem plusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aProposToolStripMenuItem;
        private System.Windows.Forms.Button Check_path;
    }
}