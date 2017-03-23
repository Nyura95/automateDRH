using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace automateDRH
{
    class WriteTxt
    {
        public void WriteTxtByList(string path, ListView liste)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                foreach (ListViewItem line in liste.Items)
                {
                    file.WriteLine(line.Text);
                }
            }
        }

        public void WriteTxtByString(string path, string text)
        {
            System.IO.File.WriteAllText(path, text);
        }
    }
}
