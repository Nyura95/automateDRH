using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace automateDRH
{
    class Log
    {
        private Dictionary<int, ListViewItem> Dico = new Dictionary<int, ListViewItem>();
        public void addLog(Dictionary<string[], Color> Add)
        {
            if (!Dico.ContainsKey(Dico.Count + 1))
            {
                if (Add != null)
                {
                    foreach (KeyValuePair<string[], Color> LesLogs in Add)
                    {
                        ListViewItem newList = new ListViewItem(LesLogs.Key);
                        newList.ForeColor = LesLogs.Value;
                        Dico.Add(Dico.Count + 1, newList);
                    }
                }
            }  
        }

        public Dictionary<int, ListViewItem> LogInProcess()
        {
            if (Dico.Count != 0)
                return Dico;
            return null;
        }

        public int count()
        {
            return Dico.Count;
        }


        public void RemovLog()
        {
            List<int> MesElements = new List<int> ();
            foreach (KeyValuePair<int, ListViewItem> kvp in Dico)
            {
                MesElements.Add(kvp.Key);
            }
            foreach (int key in MesElements)
            {
                Dico.Remove(key);
            }
        }
    }
}
