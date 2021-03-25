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

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Controls
{
    /// <summary>
    /// Interaktionslogik für SpotifyPlayer.xaml
    /// </summary>
    public partial class SpotifyPlayer : UserControl
    {
        public SpotifyPlayer()
        {
            InitializeComponent();
        }

        private void OnPlay_Clicked(object sender, RoutedEventArgs e)
        {
            (sender as Button).Visibility = Visibility.Collapsed;
            btnPause.Visibility = Visibility.Visible;
        }
        
        private void OnPause_Clicked(object sender, RoutedEventArgs e)
        {
            (sender as Button).Visibility = Visibility.Collapsed;
            btnPlay.Visibility = Visibility.Visible;
        }
    }
}
