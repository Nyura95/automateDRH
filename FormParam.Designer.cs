namespace automateDRH
{
    partial class FormParam
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
            this.listparam = new System.Windows.Forms.DataGridView();
            this.Valide = new System.Windows.Forms.Button();
            this.reset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.listparam)).BeginInit();
            this.SuspendLayout();
            // 
            // listparam
            // 
            this.listparam.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listparam.ColumnHeadersVisible = false;
            this.listparam.Location = new System.Drawing.Point(12, 7);
            this.listparam.Name = "listparam";
            this.listparam.Size = new System.Drawing.Size(843, 292);
            this.listparam.TabIndex = 0;
            // 
            // Valide
            // 
            this.Valide.Location = new System.Drawing.Point(402, 305);
            this.Valide.Name = "Valide";
            this.Valide.Size = new System.Drawing.Size(75, 23);
            this.Valide.TabIndex = 1;
            this.Valide.Text = "Valider";
            this.Valide.UseVisualStyleBackColor = true;
            this.Valide.Click += new System.EventHandler(this.Valide_Click);
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(483, 305);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(75, 23);
            this.reset.TabIndex = 2;
            this.reset.Text = "Par defaut";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // FormParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 334);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.Valide);
            this.Controls.Add(this.listparam);
            this.Name = "FormParam";
            this.Text = "FormParam";
            ((System.ComponentModel.ISupportInitialize)(this.listparam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView listparam;
        private System.Windows.Forms.Button Valide;
        private System.Windows.Forms.Button reset;


    }
}