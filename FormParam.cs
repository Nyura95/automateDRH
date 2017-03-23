using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace automateDRH
{
    public partial class FormParam : Form
    {
        public FormParam()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = ConfigurationManager.AppSettings["NameConfig"];
            listparam.ColumnCount = 3;
            listparam.Columns[0].Name = "Key";
            listparam.Columns[0].Width = 100;
            listparam.Columns[0].ReadOnly = true;
            listparam.Columns[1].Name = "Description";
            listparam.Columns[1].Width = 200;
            listparam.Columns[1].ReadOnly = true;
            listparam.Columns[2].Name = "Value";
            listparam.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            listparam.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            listparam.AllowUserToAddRows = false;


            
            foreach (SettingsProperty currentProperty in Properties.Settings.Default.Properties)
            {
                Type type = typeof(DescConfiguration);
                MethodInfo method = type.GetMethod(currentProperty.Name);
                DescConfiguration c = new DescConfiguration();
                string result = (string)method.Invoke(c, null);
                string[] row = new string[] { currentProperty.Name, result, Properties.Settings.Default[currentProperty.Name].ToString() };
                listparam.Rows.Add(row);

            }
        }

        private void Valide_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Ëtes vous sûr de vouloir continuer ?", "Validation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No) return;

            for (int i = 0; i < listparam.RowCount; i++)
            {
                if (listparam.Rows[i].Cells[2].Value == null)
                {
                    listparam.Rows[i].Cells[2].Value = "";
                }
                Properties.Settings.Default[listparam.Rows[i].Cells[0].Value.ToString()] = listparam.Rows[i].Cells[2].Value.ToString();
            }
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
            this.Close();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Vous êtes sur le point de remettre les valeur par defaut.\n \nËtes vous sûr de vouloir continuer ?" , "Validation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No) return;

            Properties.Settings.Default.Reset();
            new FormParam().Show();
            this.Close();
        }
    }
}
