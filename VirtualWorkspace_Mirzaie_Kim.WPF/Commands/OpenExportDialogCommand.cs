using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;
using VirtualWorkspace_Mirzaie_Kim.WPF.Controls;
using VirtualWorkspace_Mirzaie_Kim.WPF.Models;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Commands
{
    public class OpenExportDialogCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private IList<WorkspaceItem> _items = new List<WorkspaceItem>();
        private string _outputPath;
        private string _outputFolderName;
        private ExportOptions _option;

        private readonly WorkspaceViewModel _viewModel;

        private readonly IWorkspaceService _workspaceService;

        private readonly BackgroundWorker worker = new BackgroundWorker();

        public OpenExportDialogCommand(
            WorkspaceViewModel viewModel, 
            IWorkspaceService workspaceService)
        {
            _viewModel = viewModel;
            _workspaceService = workspaceService;

            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (parameter is int)
            {
                int workspaceId = (int)parameter;

                ExportDialogWindow dialog = new ExportDialogWindow(_workspaceService.GetAllItems(workspaceId).ToList());

                _viewModel.ToggleDialogOpened();
                bool? result = await Dispatcher.CurrentDispatcher.InvokeAsync(() => dialog.ShowDialog());

                if (result != true)
                {
                    _viewModel.ToggleDialogOpened();
                    return;
                }
                else
                {
                    _items = dialog.SelectedItems.ToList();
                    _outputFolderName = dialog.OutputFolderName;
                    _outputPath = dialog.OutputPath;
                    _option = dialog.SelectedExportOption;

                    _viewModel.ToggleSpinner();
                    worker.RunWorkerAsync();
                }
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_items.Count == 0) return;
            
            // ExportOptions.Folder
            if (_option == ExportOptions.Folder)
            {
                string path = Path.Combine(_outputPath, _outputFolderName);
                
                if (Directory.Exists(path))
                {
                    int duplicateCount = 2;
                    path += duplicateCount.ToString("00");
                    while (Directory.Exists(path))
                    {
                        duplicateCount++;
                        path = path.Substring(0, path.Length - 2) + duplicateCount.ToString("00");
                    }
                }

                Directory.CreateDirectory(path);

                foreach (var item in _items)
                {
                    string itemPath = Path.Combine(path, item.Name + item.Extension);
                    File.Copy(item.PathToOriginal, itemPath, true);
                }
            }
            // ExportOptions.Zip
            else
            {
                string zipPath = Path.Combine(_outputPath, _outputFolderName + @".zip");
                
                if (File.Exists(zipPath))
                {
                    int duplicateCount = 2;
                    zipPath += duplicateCount.ToString("00");
                    while (File.Exists(zipPath))
                    {
                        duplicateCount++;
                        zipPath = zipPath.Substring(0, zipPath.Length - 6) + duplicateCount.ToString("00");
                    }
                }

                using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
                {
                    foreach (var item in _items)
                    {
                        archive.CreateEntryFromFile(item.PathToOriginal, item.Name + item.Extension);
                    }
                }
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _viewModel.RefreshView();
        }
    }
}
