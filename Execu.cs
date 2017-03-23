using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using MysqlConnector;
using ICSharpCode.SharpZipLib.Zip;
using System.Windows.Forms;
using System.Drawing;
using System.Configuration;
namespace automateDRH
{
    class Execu
    {
        private volatile bool _shouldStop;
        private int Progress = 0;
        private Dictionary<string[], Color> ReportExecu = new Dictionary<string[], Color>();
        private Dossier MonDossier = new Dossier();

        private BackgroundWorker worker;
        private Log Meslogs = new Log();

        public Execu(BackgroundWorker Worker)
        {
            worker = Worker;

        }

        public void RequestStop()
        {
            _shouldStop = true;
        }

        public void DoWorkExtractZip()
        {
            AddList(string.Format("Nouveau fichier zip !"), Color.FromName(Properties.Settings.Default.ColorLogStart));
            MonDossier.setMyFile(Properties.Settings.Default.CheckPath);
            int Progression = 100 / MonDossier.getLenghtFileZip();
            AddProgress(0);
            foreach (FileInfo MesFichiers in MonDossier.getFilesByExt(".zip"))
            {
                FastZip Zip = new FastZip();
                Zip.ExtractZip(Properties.Settings.Default.CheckPath + @"\" + MesFichiers.Name, Properties.Settings.Default.CheckPath, null);
                File.Delete(MesFichiers.FullName);
            }
        }

        public void DoWork()
        {
            AddList(string.Format("Nouveau traitement !"), Color.FromName(Properties.Settings.Default.ColorLogStart));
            string FichierDeplacer = "";
            MonDossier.setMyFile(Properties.Settings.Default.CheckPath);
            int Progression = 100 / MonDossier.getLenghtFile();
            AddProgress(0);
            foreach (FileInfo MesFichiers in MonDossier.getFiles())
            {
                bool Move = false;
                while (!Move)
                {
                    try
                    {
                        File.Move(MesFichiers.DirectoryName + @"\" + MesFichiers.Name, MonDossier.getDesti() + MesFichiers.Name);
                        FichierDeplacer += MesFichiers.Name + Properties.Settings.Default.LogSeparation;
                        Move = true;
                    }
                    catch
                    {
                        Thread.Sleep(1000);
                    }
                }
                if (Progress + Progression < 101)
                    AddProgress(Progress + Progression);
            }
            AddList(string.Format("Nouveau fichier déplacer : ({0})", FichierDeplacer.Substring(0, FichierDeplacer.Length - Properties.Settings.Default.LogSeparation.Length)), Color.FromName(Properties.Settings.Default.ColorLogNormal));
            AddProgress(0);
        }

        public void DoWorkMysql()
        {
            AddList(string.Format("Traitement des fichiers !"), Color.FromName(Properties.Settings.Default.ColorLogStart));
            AddProgress(10);
            ExecutSql();
            AddProgress(50);
            AddList(string.Format("Compression des fichiers"), Color.FromName(Properties.Settings.Default.ColorLogNormal));
            ZipFile();
            AddProgress(90);
            DeletAllFile();
            AddList(string.Format("TERMINER !"), Color.FromName(Properties.Settings.Default.ColorLogStart));  
        }

        private void ExecutSql()
        {
            MonDossier.setMyFile(Properties.Settings.Default.traitementPath);
            int ProgressExecFile = 100 / MonDossier.getLenghtFile();
            AddProgress(0);
            foreach (FileInfo MesFichiers in MonDossier.getFiles())
            {
                AddList(string.Format("Fichier traité : {0}", MesFichiers.Name), Color.FromName(Properties.Settings.Default.ColorLogNormal));
                MysqlDll Mysql = new MysqlDll(Properties.Settings.Default.hostmysql, Properties.Settings.Default.usermysql,
                    Properties.Settings.Default.mdpmysql, Properties.Settings.Default.database);

                AddList(string.Format("SQL CONNECT : {0}", Mysql.getCo()), Mysql.etacColor());

                if (Mysql.isConnect())
                {
                    string ext = Properties.Settings.Default.ExtFile;
                    if (!ext.Contains(".")) ext = "." + ext;
                    Boolean mysqlError = false;
                    //On set le fichier ou ce trouve les .sql
                    Dossier DossierSQL = new Dossier();
                    string Fichier = MesFichiers.Name.Replace(ext, "");
                    //On check si il y a bien un fichier de présent
                    if (!DossierSQL.CheckIsValid(Properties.Settings.Default.SqlPath + @"\" + Fichier))
                    {
                        //Si c'eszt le fichier Gestime c'est bon
                        if (MesFichiers.Name.Contains("EXP_REPOS_DUS__AN_"))
                        {
                            Fichier = "Gestime";
                        }
                        else
                        {
                            //Il n'existe pas de fichier sous le nom du fichier csv
                            AddList(string.Format("Erreur fichier innexistant pour : {0}", MesFichiers.Name), Color.FromName(Properties.Settings.Default.ColorLogFail));
                            mysqlError = true;
                        }
                    }

                    if (!mysqlError)
                    {
                        DossierSQL.setMyFile(Properties.Settings.Default.SqlPath + @"\" + Fichier);
                        AddList(string.Format("Ouverture fichier : {0}", MesFichiers.Name.Replace(ext, "")), Color.FromName(Properties.Settings.Default.ColorLogTraitement));
                        //On boucle sur les fichiers SQL
                        foreach (FileInfo MesFichiersSQL in DossierSQL.getFilesSql())
                        {
                            AddList(string.Format("Fichier SQL traité : {0}", MesFichiersSQL.Name), Color.FromName(Properties.Settings.Default.ColorLogTraitement));
                            Mysql.SendRequestSql(MesFichiersSQL.FullName, MesFichiers.Name);

                            if (Mysql.getQuery() != "OK")
                            {
                                writeFail(MesFichiersSQL.Name, Mysql.getQuery());
                                AddList(string.Format("Erreur SQL sur : {0}", MesFichiersSQL.Name), Color.FromName(Properties.Settings.Default.ColorLogFail));
                                mysqlError = true;
                            }
                            AddList(string.Format("Retour SQL : {0}", Mysql.getQuery()), Mysql.getColorQuery());

                        }

                        if (mysqlError)
                        {
                            if (File.Exists(Properties.Settings.Default.FailPath + @"\" + MesFichiers.Name))
                            {
                                File.Delete(Properties.Settings.Default.FailPath + @"\" + MesFichiers.Name);
                            }
                            File.Move(MesFichiers.FullName, Properties.Settings.Default.FileFailPath + @"\" + MesFichiers.Name);
                        }

                        string close = Mysql.CloseMysql();
                        AddList(string.Format("Fermeture SQL : {0}", close), Color.FromName(Properties.Settings.Default.ColorLogNormal));
                    }
                }
                else
                {
                    AddList(string.Format("Erreur MYSQL ! Impossible de se connecter au serveur."), Color.FromName(Properties.Settings.Default.ColorLogFail));
                }
                if (Progress + ProgressExecFile < 101)
                    AddProgress(Progress + ProgressExecFile);
                foreach (string fihcierPOST in Properties.Settings.Default.PosttraitementTable.Split(';'))
                {
                    if(fihcierPOST == MesFichiers.Name)
                        PostTraitement();
                }
            }
            AddProgress(0);
        }

        private void writeFail(string Name, string Response)
        {
            WriteTxt write = new WriteTxt();
            write.WriteTxtByString(Properties.Settings.Default.FailPath + @"\" + DateTime.Now.ToString("HH:mm:ss-dd/MM/yyyy") + "_" + Name + "_" +
            Properties.Settings.Default.NameFail + ".txt", "(" + DateTime.Now.ToString() + ")" + Name + " : " + Response);
        }

        private void PostTraitement()
        {
            
            AddList(string.Format("Post Traitement en cours ! ({0})", Properties.Settings.Default.PosttraitementName), Color.FromName(Properties.Settings.Default.ColorLogNormal));
            MysqlDll Mysql = new MysqlDll(Properties.Settings.Default.hostmysql, Properties.Settings.Default.usermysql,
                   Properties.Settings.Default.mdpmysql, Properties.Settings.Default.database);
            AddList(string.Format("SQL CONNECT : {0}", Mysql.getCo()), Mysql.etacColor());

            Dossier DossierPost = new Dossier();
            DossierPost.setMyFile(Properties.Settings.Default.PosttraitementPath);
            foreach (FileInfo MesFichiers in DossierPost.getFilesSql())
            {
                Mysql.SendRequestSql(MesFichiers.FullName, MesFichiers.Name);
            }
            if (Mysql.getQuery() != "OK")
            {
                writeFail(Properties.Settings.Default.PosttraitementName, Mysql.getQuery());
            }
            AddList(string.Format("Retour SQL post traitement : {0}", Mysql.getQuery()), Mysql.getColorQuery());
        }

        private void ZipFile()
        {
            FastZip Zip = new FastZip();
            Zip.Password = Properties.Settings.Default.mdp;
            AddList(string.Format("Mot de passe par defaut : {0}", Properties.Settings.Default.mdp), Color.FromName(Properties.Settings.Default.ColorLogNormal));
            int nbFichiersSD = Directory.GetFiles(Properties.Settings.Default.traitementPath, "*.*", SearchOption.TopDirectoryOnly).Length;
            if (nbFichiersSD != 0)
            {
                Zip.CreateZip(Properties.Settings.Default.SavePath + @"\" + Properties.Settings.Default.NameZip + DateTime.Now.ToString("HH:mm:ss-dd/MM/yyyy")
                    + ".zip", Properties.Settings.Default.traitementPath, false, null);
            }
        }

        private void DeletAllFile()
        {
            MonDossier.setMyFile(Properties.Settings.Default.traitementPath);
            string NameFichier = "";
            foreach (FileInfo MesFichiers in MonDossier.getFiles())
            {
                NameFichier += MesFichiers.Name + Properties.Settings.Default.LogSeparation;
                File.Delete(MesFichiers.FullName);
            }
            if (NameFichier == ""){
                AddList(string.Format("Aucune suppression"),
                    Color.FromName(Properties.Settings.Default.ColorLogNormal));
            }
            else
            {
                AddList(string.Format("Suppression de : {0}", NameFichier.Substring(0, NameFichier.Length - Properties.Settings.Default.LogSeparation.Length)),
                Color.FromName(Properties.Settings.Default.ColorLogNormal));
            }
            
            AddProgress(100);
        }

        private void AddList(string log, Color color)
        {
            ReportExecu.Add(new[] { log, DateTime.Now.ToString() }, color);
            GestionLog();
        }

        private void AddProgress(int progress)
        {
            Progress = progress;
            Thread.Sleep(1000);
            GestionLog();
        }

        private void GestionLog()
        {
            Meslogs.addLog(getReportExecu());
            worker.ReportProgress(getProgress());
            for (int y = 1; (y <= Meslogs.count()); y++)
            {
                if (Meslogs.LogInProcess().ContainsKey(y))
                {
                    worker.ReportProgress(getProgress(), Meslogs.LogInProcess()[y]);
                }
                    
            }
            Meslogs.RemovLog();
        }

        public int getProgress()
        {
            return Progress;
        }

        public void setProgress(int progress)
        {
            Progress = progress;
        }

        public Dictionary<string[], Color> getReportExecu()
        {
            Dictionary<string[], Color> Save = ReportExecu;
            ReportExecu = new Dictionary<string[], Color>();
            return Save;
        }


        private void setReportExecu(string[] Report, Color color)
        {
            ReportExecu.Add(Report, color);
        }
    }
}
