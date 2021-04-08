using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.WPF.Commands;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Controls
{
    /// <summary>
    /// Interaktionslogik für FileInfoWindow.xaml
    /// </summary>
    public partial class FileInfoWindow : Window
    {
        public WorkspaceItem SelectedItem
        {
            get { return (WorkspaceItem)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(WorkspaceItem), typeof(FileInfoWindow), new PropertyMetadata(null));

        public ICommand CloseCommand { get => new GeneralCommand(OnClose); }

        public FileInfoWindow(WorkspaceItem selectedItem)
        {
            Owner = Application.Current.MainWindow;
            InitializeComponent();

            SelectedItem = selectedItem;
        }

        private void OnClose(object parameter)
        {
            DialogResult = true;
            Close();
        }
    }
}
