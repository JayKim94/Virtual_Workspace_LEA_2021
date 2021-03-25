using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Models
{
    public class FileDropEventArgs : RoutedEventArgs
    {
        private string _path;

        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        internal FileDropEventArgs(string path, string name)
        {
            _path = path;
            _name = name;
        }
    }
}
