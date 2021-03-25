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

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Controls
{
    /// <summary>
    /// Interaktionslogik für DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public string SubtitleText
        {
            get { return (string)GetValue(SubtitleTextProperty); }
            set { SetValue(SubtitleTextProperty, value); }
        }

        public static readonly DependencyProperty SubtitleTextProperty =
            DependencyProperty.Register("SubtitleText", typeof(string), typeof(DialogWindow), new PropertyMetadata(null));

        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }

        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register("TitleText", typeof(string), typeof(DialogWindow), new PropertyMetadata(null));

        public DialogWindow(string title, string subtitle)
        {
            Owner = Application.Current.MainWindow;
            InitializeComponent();
            TitleText = title;
            SubtitleText = subtitle;
        }
    }
}
