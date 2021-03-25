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
using VirtualWorkspace_Mirzaie_Kim.WPF.Commands;

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



        public string IconSource
        {
            get { return (string)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("IconSource", typeof(string), typeof(DialogWindow), new PropertyMetadata(null));

        public bool IsYesOrNo
        {
            get { return (bool)GetValue(IsYesOrNoProperty); }
            set { SetValue(IsYesOrNoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsYesOrNo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsYesOrNoProperty =
            DependencyProperty.Register("IsYesOrNo", typeof(bool), typeof(DialogWindow), new PropertyMetadata(false));

        public ICommand DeleteCommand { get => new GeneralCommand(DeleteAndClose); }

        public DialogWindow(string title, string subtitle, string iconSource = "/Images/checked.png", bool isYesOrNo = false)
        {
            Owner = Application.Current.MainWindow;
            InitializeComponent();
            TitleText = title;
            SubtitleText = subtitle;
            IconSource = iconSource;
            IsYesOrNo = isYesOrNo;
        }

        private void DeleteAndClose(object parameter)
        {
            DialogResult = true;
            Close();
        }
    }
}
