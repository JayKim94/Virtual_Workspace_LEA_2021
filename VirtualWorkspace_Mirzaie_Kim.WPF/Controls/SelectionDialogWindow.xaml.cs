using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Controls
{
    /// <summary>
    /// Interaktionslogik für DialogWindow.xaml
    /// </summary>
    public partial class SelectionDialogWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string SubtitleText
        {
            get { return (string)GetValue(SubtitleTextProperty); }
            set { SetValue(SubtitleTextProperty, value); }
        }

        public static readonly DependencyProperty SubtitleTextProperty =
            DependencyProperty.Register("SubtitleText", typeof(string), typeof(SelectionDialogWindow), new PropertyMetadata(null));

        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }

        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register("TitleText", typeof(string), typeof(SelectionDialogWindow), new PropertyMetadata(null));

        public string SubmitButtonText
        {
            get { return (string)GetValue(SubmitButtonTextProperty); }
            set { SetValue(SubmitButtonTextProperty, value); }
        }

        public static readonly DependencyProperty SubmitButtonTextProperty =
            DependencyProperty.Register("SubmitButtonText", typeof(string), typeof(SelectionDialogWindow), new PropertyMetadata(null));

        private ICollectionView _selectionItems;

        public ICollectionView SelectionItems
        {
            get { return _selectionItems; }
            set 
            { 
                _selectionItems = value;
                OnPropertyChanged(nameof(SelectionItems));
            }
        }

        public IEnumerable<WorkspaceItem> SelectedItems { get => itemsListView.SelectedItems.Cast<WorkspaceItem>(); }

        public SelectionDialogWindow(string title, string subtitle, ICollection<WorkspaceItem> items)
        {
            Owner = Application.Current.MainWindow;
            InitializeComponent();
            TitleText = title;
            SubtitleText = subtitle;

            SelectionItems = CollectionViewSource.GetDefaultView(items);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SelectAll(object sender, RoutedEventArgs e)
        {
            itemsListView.SelectAll();
        }
        
        private void UnselectAll(object sender, RoutedEventArgs e)
        {
            itemsListView.UnselectAll();
        }

        private void Import(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
