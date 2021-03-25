using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.IO.Compression;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Reflection;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Controls
{
    /// <summary>
    /// Interaction logic for DropDownMenu.xaml
    /// </summary>
    public partial class DropDownMenu : UserControl
    {
        public DropDownMenu()
        {
            InitializeComponent();
        }

        public string directoryPath { get; private set; }
        public object newDirectoryPath { get; private set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            menuList.IsOpen = true;
        }

        private void CreateZipFile(object sender, RoutedEventArgs e)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            List<WorkspaceItem> workspaceItems = new List<WorkspaceItem>();
            workspaceItems.Add(new WorkspaceItem() {PathToOriginal = @"C:\Users\ryan\Desktop\text.txt" });
            var tempPath = path + @"\temp";
            if (!Directory.Exists(tempPath))
            {
                DirectoryInfo di = Directory.CreateDirectory(tempPath);
            }

            
            foreach (var item in workspaceItems)
            {
                if (!File.Exists(tempPath + @"\" + item.Name))
                {
                    File.Copy(item.PathToOriginal, tempPath + @"\" + item.Name);
                }
            }
            

        }
        private void MoveToFolder(object sender, RoutedEventArgs e)
        {
            string[] filePaths = Directory.GetFiles(@"C:\Users\ryan\Desktop");
            foreach (var filename in filePaths)
            {
                string file = filename.ToString();
 
                string str = @"C:\Users\ryan\Desktop" + file.ToString();
                if (!File.Exists(str))
                {
                    File.Copy(file, str);
                }
            }
            //if (Directory.Exists(directoryPath))
            //{
            //    foreach (var file in new DirectoryInfo(directoryPath).GetFiles())
            //    {
            //        file.MoveTo($@"{newDirectoryPath}\{file.Name}");
            //    }
            //}
        }
    }
}
