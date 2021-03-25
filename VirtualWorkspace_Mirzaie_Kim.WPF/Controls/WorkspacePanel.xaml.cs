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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Controls
{
    /// <summary>
    /// Interaktionslogik für WorkspacePanel.xaml
    /// </summary>
    public partial class WorkspacePanel : UserControl
    {
        public WorkspacePanel()
        {
            InitializeComponent();
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FocusManager.SetFocusedElement(FocusManager.GetFocusScope(tbxSearch), null);
                Keyboard.ClearFocus();
            }

            e.Handled = true;
            (DataContext as WorkspaceViewModel).FilterWorkspaces(tbxSearch.Text);
        }
    }
}
