using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
namespace automateDRH
{
    class Dossier
    {
        private DirectoryInfo MyFile;

        public bool CheckExist()
        {
            if (getFiles().Length != 0)
                return true;
            return false;
        }

        public bool CheckExistZip()
        {
            if (getFilesByExt(".zip").Length != 0)
                return true;
            return false;
        }

        public void setMyFile(string path)
        {
            MyFile = new DirectoryInfo(path);
        }

        public bool CheckIsValid(string path)
        {
            DirectoryInfo check = new DirectoryInfo(path);
            if (check.Exists)
            {
                return true;
            }
            return false;
        }

        public FileInfo[] getFiles()
        {
            if (Properties.Settings.Default.ExtFile.Contains(".")) return MyFile.GetFiles("*" + Properties.Settings.Default.ExtFile);
            return MyFile.GetFiles("*."+Properties.Settings.Default.ExtFile);
        }

        public FileInfo[] getFilesSql()
        {
            if (Properties.Settings.Default.ExtFile.Contains(".")) return MyFile.GetFiles("*.sql");
            return MyFile.GetFiles("*.sql");
        }

        public FileInfo[] getFilesByExt(string ext)
        {
            if (Properties.Settings.Default.ExtFile.Contains(".")) return MyFile.GetFiles("*" + ext);
            return MyFile.GetFiles("*." + ext);
        }



        public DirectoryInfo getDossier()
        {
            return MyFile;
        }

        public string getDesti()
        {
            return Properties.Settings.Default.traitementPath + @"\";
        }

        public int getLenghtFile()
        {
            return getFiles().Length;
        }

        public int getLenghtFileZip()
        {
            return getFilesByExt(".zip").Length;
        }
    }
}