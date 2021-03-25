using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.WPF.Commands;
using VirtualWorkspace_Mirzaie_Kim.WPF.Models;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Controls
{
    /// <summary>
    /// Interaktionslogik für ExportDialogWindow.xaml
    /// </summary>
    public partial class ExportDialogWindow : Window, INotifyPropertyChanged
    {
        public ICommand ExportToZipCommand
        {
            get { return (ICommand)GetValue(ExportToZipCommandProperty); }
            set { SetValue(ExportToZipCommandProperty, value); }
        }

        public static readonly DependencyProperty ExportToZipCommandProperty =
            DependencyProperty.Register("ExportToZipCommand", typeof(ICommand), typeof(ExportDialogWindow), new PropertyMetadata(null));

        public ICommand ExportToFolderCommand
        {
            get { return (ICommand)GetValue(ExportToFolderCommandProperty); }
            set { SetValue(ExportToFolderCommandProperty, value); }
        }

        public static readonly DependencyProperty ExportToFolderCommandProperty =
            DependencyProperty.Register("ExportToFolderCommand", typeof(ICommand), typeof(ExportDialogWindow), new PropertyMetadata(null));

        public IList<WorkspaceItem> WorkspaceItems
        {
            get { return (IList<WorkspaceItem>)GetValue(WorkspaceItemsProperty); }
            set { SetValue(WorkspaceItemsProperty, value); }
        }

        public static readonly DependencyProperty WorkspaceItemsProperty =
            DependencyProperty.Register("WorkspaceItems", typeof(IList<WorkspaceItem>), typeof(ExportDialogWindow), new PropertyMetadata(null));

        public ExportOptions SelectedExportOption
        {
            get { return (ExportOptions)GetValue(SelectedExportOptionProperty); }
            set { SetValue(SelectedExportOptionProperty, value); }
        }

        public static readonly DependencyProperty SelectedExportOptionProperty =
            DependencyProperty.Register("SelectedExportOption", typeof(ExportOptions), typeof(ExportDialogWindow), new PropertyMetadata(ExportOptions.Zip));

        public string OutputPath
        {
            get { return (string)GetValue(OutputPathProperty); }
            set { SetValue(OutputPathProperty, value); }
        }
        
        public static readonly DependencyProperty OutputPathProperty =
            DependencyProperty.Register("OutputPath", typeof(string), typeof(ExportDialogWindow), new PropertyMetadata(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)));

        public string OutputFolderName
        {
            get { return (string)GetValue(OutputFolderNameProperty); }
            set { SetValue(OutputFolderNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OutputFolderName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutputFolderNameProperty =
            DependencyProperty.Register("OutputFolderName", typeof(string), typeof(ExportDialogWindow), new PropertyMetadata("Workspace_Sammlung_Zip"));

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CloseMenuCommand { get => new GeneralCommand(CloseMenu); }

        public ICommand ExportCommand { get => new GeneralCommand(Export); }

        private ICollectionView _workspaceItemView;

        public ICollectionView WorkspaceItemView
        {
            get { return _workspaceItemView; }
            set
            {
                _workspaceItemView = value;
                OnPropertyChanged(nameof(WorkspaceItemView));
            }
        }

        public IEnumerable<WorkspaceItem> SelectedItems { get => workspaceItemsView.SelectedItems.Cast<WorkspaceItem>(); }

        public ExportDialogWindow(ICollection<WorkspaceItem> items)
        {
            Owner = Application.Current.MainWindow;
            InitializeComponent();
            WorkspaceItemView = CollectionViewSource.GetDefaultView(items);
        }

        private void Export(object param)
        {
            DialogResult = true;
            Close();
        }

        private void CloseMenu(object param)
        {
            Close();
        }

        private void OnExportToZip_Clicked(object sender, MouseButtonEventArgs e)
        {
            SelectedExportOption = ExportOptions.Zip;
        }

        private void OnExportToFolder_Clicked(object sender, MouseButtonEventArgs e)
        {
            SelectedExportOption = ExportOptions.Folder;
        }
        
        private void OnOutputPath_Clicked(object sender, MouseButtonEventArgs e)
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                dialog.IsFolderPicker = true;

                if (dialog.ShowDialog() != CommonFileDialogResult.Ok) return;
                
                
                OutputPath = dialog.FileName;
            }
        }

        private void SelectAll(object sender, RoutedEventArgs e)
        {
            workspaceItemsView.SelectAll();
        }

        private void UnselectAll(object sender, RoutedEventArgs e)
        {
            workspaceItemsView.UnselectAll();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
