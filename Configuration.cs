using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace automateDRH
{
    class DescConfiguration
    {

        public string ColorLogStart()
        {
            return "Couleur des log debut/fin (en anglais)";
        }

        public string ColorLogFail()
        {
            return "Couleur des log fail (en anglais)";
        }

        public string ColorLogNormal()
        {
            return "Couleur des log normal (en anglais)";
        }

        public string NameFail()
        {
            return "Comment s'appel le fichier d'erreur";
        }
        public string NameLog()
        {
            return "Comment s'appel le fichier des logs ?";
        }
        public string FailPath()
        {
            return "Où ce trouve le rapport d'erreur ?";
        }

        public string LogPath()
        {
            return "Où ce trouve le fichier des logs";
        }
        public string SqlPath()
        {
            return "Où se trouve les fichier SQL ?";
        }

        public string traitementPath()
        {
            return "Où se trouve le fichier de traitement ?";
        }

        public string CheckPath()
        {
            return "Où se trouve le fichier à surveiller ?";
        }
        public string SavePath()
        {
            return "Où se trouve le fichier des sauvegardes ?";
        }
        public string NameZip()
        {
            return "Le nom des zip ?";
        }
        public string mdp()
        {
            return "Mot de passe des Zip ?";
        }
        public string LogSeparation()
        {
            return "Separateur des logs";
        }
        public string hostmysql(){
            return "Host Mysql";
        }
        public string usermysql(){
            return "User Mysql";
        }
        public string mdpmysql(){
            return "Mdp Mysql";
        }
        public string database()
        {
            return "database";
        }
        public string NameSql()
        {
            return "Nom par defaut des fichiers SQL ?";
        }
        public string ExtFile()
        {
            return "Extension des fichier a traité ?";
        }
        public string APropos()
        {
            return "Automate programmable développer par Nyura \nPour la DRH du siège de l'APHP\n.NET Framework 3.5\nPlus d'information contacter " + ConfigurationManager.AppSettings["NameMaster"];
        }
        public string ErreurPath(string key)
        {
            return string.Format("Chemin spécifier n'est pas valide !\nMerci de modifier le chemin de la valeur '{0}' dans les parametres.", key);
        }
        public string PosttraitementPath()
        {
            return "Où se trouve le fichier POST traitement ?";
        }
        public string PosttraitementTable()
        {
            return "Après qu'elle fichier executer le POST traitement ?";
        }
        public string PosttraitementName()
        {
            return "Quelle est le nom du fichier POST traitement ?";
        }
        public string FileFailPath()
        {
            return "Quelle est le chemin pour les fichiers qui n'ont pas été traité";
        }
        public string ConnectMySQLFail()
        {
            return "impossible de se connecter à MySQL";
        }
        public string ColorLogTraitement()
        {
            return "Couleur des logs de traitement";
        }
    }
}
