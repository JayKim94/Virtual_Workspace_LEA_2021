using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;
using VirtualWorkspace_Mirzaie_Kim.WPF.Models;
using VirtualWorkspace_Mirzaie_Kim.WPF.Controls.Subcontrols;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Controls
{
    /// <summary>
    /// Interaction logic for FileManager.xaml
    /// </summary>
    public partial class FileManager : UserControl
    {
        public FileManager()
        {
            InitializeComponent();
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FocusManager.SetFocusedElement(FocusManager.GetFocusScope(tbxSearchItem), null);
                Keyboard.ClearFocus();
            }

            e.Handled = true;
            (DataContext as WorkspaceViewModel).FilterItems(tbxSearchItem.Text);
        }

        private void Handle_Drop(object sender, DragEventArgs e)
        {
            e.Handled = true;
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string path = files[0];

            (DataContext as WorkspaceViewModel).HandleFileDropCommand.Execute(path);
        }

        private void Item_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
            ListView view = sender as ListView;

            if (view.SelectedValue == null) return;

            (DataContext as WorkspaceViewModel).FocusWorkspaceItem((view.SelectedItem as WorkspaceItem).WorkspaceItemId);
        }

        private void OpenExportDialog_Clicked(object sender, RoutedEventArgs e)
        {
            //(DataContext as WorkspaceViewModel).IsBusy = true;
        }
    }
}

