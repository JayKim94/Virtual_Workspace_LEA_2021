using System.Windows;
using System.Windows.Controls;
using VirtualWorkspace_Mirzaie_Kim.WPF.Models;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Controls
{
    /// <summary>
    /// Interaction logic for ResourcesPanel.xaml
    /// </summary>
    public partial class ResourceManager : UserControl
    {
        public ResourceManager()
        {
            InitializeComponent();
        }

        private void Handle_Drop(object sender, DragEventArgs e)
        {
            e.Handled = true;
            ResourceDropZone.Opacity = 1;

            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string path = files[0];

            (DataContext as WorkspaceViewModel).HandleFileDropCommand.Execute(path);
        }
    }
}
