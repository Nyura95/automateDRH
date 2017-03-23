using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing;
using automateDRH;
using System.Data;
using System.Configuration;

namespace MysqlConnector
{
    public class MysqlDll
    {
        private MySqlConnection conn = null;
        private MySqlDataReader rdr = null;
        private string etatCo = "";
        private string query = "";
        
        public MysqlDll(string server, string user, string pass, string database)
        {
            string cs = string.Format(@"server={0};userid={1};password={2};database={3}", server, user, pass, database);

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
                etatCo = "Connection effectué";
            }
            catch (MySqlException ex)
            {
                etatCo = ex.ToString();
            }
        }
        public bool isConnect()
        {
            if (etatCo == "Connection effectué")
            {
                return true;
            }
            return false;
        }

        public string getCo()
        {
            return etatCo;
        }
        public string getQuery()
        {
            return query;
        }

        public Color getColorQuery()
        {
            if (query == "OK")
            {
                return Color.FromName(automateDRH.Properties.Settings.Default.ColorLogNormal);
            }
            return Color.FromName(automateDRH.Properties.Settings.Default.ColorLogFail);
        }

        public Color etacColor()
        {
            if (conn.State.ToString() == "Open")
            {
                return Color.FromName(automateDRH.Properties.Settings.Default.ColorLogNormal);
            }
            return Color.FromName(automateDRH.Properties.Settings.Default.ColorLogFail);
        }
        public MySqlDataReader SendQuery(string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            return rdr = cmd.ExecuteReader();
        }

        public void ExecutQuery(string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

        public void SendRequestSql(string rep, string fichier)
        {
         
            try
            {
                Dictionary<string, string> Natif = new Dictionary<string, string> ();
                FileInfo file = new FileInfo(rep);
                if (!file.Exists)
                {
                    if (fichier.Contains("EXP_REPOS_DUS__AN_"))
                    {
                        string yearfile = getBetString(fichier, "__").Replace("AN", "").Replace("_", "");
                        Natif.Add("%yearfile%", yearfile);
                        if (!File.Exists(rep))
                        {
                            query = "FICHIER SQL GESTIME MANQUANT OU MAUVAIS CHEMIN !";
                            return;
                        }
                    }
                    else
                    {
                        query = "FICHIER SQL MANQUANT OU MAUVAIS CHEMIN !";
                        return;
                    }
                }
                string strscript = getrequete(rep);
                //Les natifs de l'automate
                Natif.Add("%pathfile%", automateDRH.Properties.Settings.Default.traitementPath + @"\\" + fichier);
                Natif.Add("%yearnow%", DateTime.Now.Year.ToString());

                foreach (KeyValuePair<string, string> item in Natif)
                {
                    strscript = strscript.Replace(item.Key, item.Value);
                }

                string[] stringSeparators = new string[] { "\r\n" };
                foreach (string GetParams in strscript.Split(stringSeparators, StringSplitOptions.None))
                {
                    if (GetParams.Contains("%"))
                    {
                        string getFichier = "";
                        bool checkgetfichier = false;
                        string key = getBetString(GetParams, "%");
                        string value = getBetString(GetParams, "(", ")").Replace(" ", "");

                        if (value != "")
                        {
                            foreach (string getParamsInFile in value.Split(','))
                            {
                                string[] splitparams = getParamsInFile.Split(':');
                                if (splitparams.Length == 3)
                                {
                                    splitparams[1] = getBetString(getParamsInFile, "\"");
                                }
                                if(splitparams[0] == "file"){
                                    if (File.Exists(splitparams[1]))
                                    {
                                        getFichier = getrequete(splitparams[1]);
                                        checkgetfichier = true;
                                    }
                                    else
                                    {
                                        query = "Le fichier du parametre 'file' n'est pas correcte !";
                                        return;
                                    }
                                }
                                else
                                {
                                    if (!checkgetfichier)
                                    {
                                        query = "Il manque le parametre 'file' !";
                                        return;
                                    }
                                    getFichier = getFichier.Replace(splitparams[0], splitparams[1].Replace("\"", ""));
                                }
                            }
                            getFichier = getFichier.Replace("%", "");
                        }
                        strscript = strscript.Replace(GetParams, getFichier);
                    }
                }
  
                MySqlCommand cmd = new MySqlCommand(strscript, conn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
                query = "OK";
               
            }
            catch (MySqlException ex)
            {
                query =  ex.Message;
            }
            catch (Exception e)
            {
                query = e.Message;
            }
        }

        private string getBetString(string str, string @char)
        {
            return str.Substring(str.IndexOf(@char), str.LastIndexOf(@char) - str.IndexOf(@char) + 1).Replace(@char, "");
        }
        private string getBetString(string str, string char1, string char2)
        {
            return str.Substring(str.IndexOf(char1) + 1, str.IndexOf(char2) - str.IndexOf(char1) - 1);
        } 

        private string getrequete(string path)
        {

            //
            StreamReader sr = null;
            //
            String line = null;

            try
            {
                sr = new StreamReader(path);
                line = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return line;// cette variable stocke le contenu de ton fichier .sql
        }

        public string CloseMysql()
        {
            try
            {
                conn.Close();
                return "Fermeture ok !";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            
        }
    }
}
