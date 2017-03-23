using MysqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace automateDRH
{
    public partial class Home : Form
    {
        private Log Meslogs = new Log();
        private picturestart gestPicture = new picturestart();
        private Dossier Dossier = new Dossier();
        private Execu workerObject;
        public Home()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = ConfigurationManager.AppSettings["NameAppli"];
            list_log.View = View.Details;
            list_log.FullRowSelect = true;
            list_log.Columns.Add("Description", 400, HorizontalAlignment.Left);
            list_log.Columns.Add("Date", 170, HorizontalAlignment.Left);


            parseWorker.WorkerReportsProgress = true;
            parseWorker.WorkerSupportsCancellation = true;
        }

        private void start_Click(object sender, EventArgs e)
        {
            if (parseWorker.IsBusy != true)
            {
                parseWorker.RunWorkerAsync();
            }
            else
            {
                parseWorker.CancelAsync();
            }
        }

        private void parseWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            bool start = true;
            BackgroundWorker worker = sender as BackgroundWorker;
            Execu workerobject = new Execu(worker);
            workerObject = workerobject;

            picture_start.BackColor = gestPicture.start();
            if (!CheckAllPath("Path")) parseWorker.CancelAsync();
            if (!CheckMysql()) parseWorker.CancelAsync();
            while (start)
            {
                for (int i = 1; (i <= 10); i++)
                {
                    if ((worker.CancellationPending == true))
                    {
                        e.Cancel = true;
                        start = false;
                        break;
                    }
                    else
                    {
                        Dossier.setMyFile(Properties.Settings.Default.traitementPath);
                        if(Dossier.CheckExistZip())
                            GestionExtractZip(worker);
                        if (Dossier.CheckExist())
                            GestionExecuSql(worker);

                        Dossier.setMyFile(Properties.Settings.Default.CheckPath);
                        if (Dossier.CheckExistZip())
                            GestionExtractZip(worker);
                        if (Dossier.CheckExist())
                            GestionMoveFile(worker);
                        Thread.Sleep(500);
                    }
                }
            }


        }

        private void GestionExtractZip(BackgroundWorker worker)
        {
            Thread workerThread = new Thread(workerObject.DoWorkExtractZip);
            workerThread.Start();
            while (!workerThread.IsAlive) ;
            workerObject.RequestStop();
            workerThread.Join();
        }

        private void GestionMoveFile(BackgroundWorker worker)
        {
            Thread workerThread = new Thread(workerObject.DoWork);
            workerThread.Start();
            while (!workerThread.IsAlive) ;
            workerObject.RequestStop();
            workerThread.Join();
        }

        private void GestionExecuSql(BackgroundWorker worker)
        {
            Thread workerThread = new Thread(workerObject.DoWorkMysql);
            workerThread.Start();
            while (!workerThread.IsAlive) ;
            workerObject.RequestStop();
            workerThread.Join();
        }


        private void parseWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressbar.Value = e.ProgressPercentage;
            if (e.ProgressPercentage == 100) progressbar.Value = 0;
            if (e.UserState != null)
                list_log.Items.Add((ListViewItem)e.UserState);
            if (list_log.Items.Count != 0)
                list_log.EnsureVisible(list_log.Items.Count - 1);
        }

        private void parseWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            picture_start.BackColor = gestPicture.end();
            if (!(e.Error == null))
            {
                MessageBox.Show(e.Error.Message, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void changerConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormParam form2 = new FormParam();
            form2.Show();
        }

        private void Check_mysql_Click(object sender, EventArgs e)
        {
            MysqlDll Mysql = new MysqlDll(Properties.Settings.Default.hostmysql, Properties.Settings.Default.usermysql,
                    Properties.Settings.Default.mdpmysql, Properties.Settings.Default.database);

            MessageBox.Show(Mysql.getCo(), "Check !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Mysql.CloseMysql();
        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DescConfiguration desc = new DescConfiguration();
            MessageBox.Show(desc.APropos(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void log_Click(object sender, EventArgs e)
        {
            if (!CheckAllPath("LogPath")) return;
            WriteTxt write = new WriteTxt();
            write.WriteTxtByList(Properties.Settings.Default.LogPath + @"\" + Properties.Settings.Default.NameLog + ".txt", list_log);
        }

        private void Check_path_Click(object sender, EventArgs e)
        {
            if (CheckAllPath("Path")) MessageBox.Show("Les chemins sont ok !", "Check !", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool CheckAllPath(string @params)
        {
            string key = "";

            //
           // parseWorker.CancelAsync();
            foreach (SettingsProperty currentProperty in Properties.Settings.Default.Properties)
            {
                if (currentProperty.Name.Contains(@params))
                {
                    SettingsProperty property = new SettingsProperty(currentProperty.Name);
                    if(!Dossier.CheckIsValid((string)Properties.Settings.Default[property.Name])){
                        key = property.Name;
                    }
                }
            }

            if (key != "")
            {
                DescConfiguration desc = new DescConfiguration();
                SettingsProperty property = new SettingsProperty(key);
                MessageBox.Show(desc.ErreurPath(property.Name), "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } 
            return true;
        }

        private bool CheckMysql()
        {
            MysqlDll Mysql = new MysqlDll(Properties.Settings.Default.hostmysql, Properties.Settings.Default.usermysql,
                   Properties.Settings.Default.mdpmysql, Properties.Settings.Default.database);
            if(!Mysql.isConnect())
            {
                DescConfiguration desc = new DescConfiguration();
                MessageBox.Show(desc.ConnectMySQLFail(), "Erreur !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Mysql.isConnect();
        }
    }
}
