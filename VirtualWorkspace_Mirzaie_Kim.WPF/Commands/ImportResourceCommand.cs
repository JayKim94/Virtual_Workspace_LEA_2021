using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;
using VirtualWorkspace_Mirzaie_Kim.WPF.Controls;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Commands
{
    public class ImportResourceCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private WorkspaceViewModel _viewModel;
        private IWorkspaceService _workspaceService;
        private IResourceDirectoryService _resourceService;

        public ImportResourceCommand(WorkspaceViewModel viewModel, IWorkspaceService workspaceService, IResourceDirectoryService resourceService)
        {
            _viewModel = viewModel;
            _workspaceService = workspaceService;
            _resourceService = resourceService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is int)
            {
                ResourceDirectory resource = _resourceService.GetResourceDirectory((int)parameter);
                if (resource == null) return;

                var result = new ObservableCollection<WorkspaceItem>();
                string[] files = Directory.GetFiles(resource.Path);

                foreach (var file in files)
                {
                    result.Add(new WorkspaceItem()
                    {
                        WorkspaceId = App.CurrentWorkspace.WorkspaceId,
                        Name = Path.GetFileNameWithoutExtension(file),
                        Extension = Path.GetExtension(file),
                        PathToOriginal = file
                    });
                }

                SelectionDialogWindow dialog = new SelectionDialogWindow("Importieren", "Wählen Sie die Dateien, die Sie importieren wollen.", result);
                dialog.SubmitButtonText = "Importieren";

                _viewModel.ToggleDialogOpened();
                if (dialog.ShowDialog() != true)
                {
                    _viewModel.ToggleDialogOpened();
                    return;
                }
                else
                {
                    foreach (var item in dialog.SelectedItems)
                    {
                        _workspaceService.AddItem(item);
                    }
                    _viewModel.RefreshView();
                }
            }
        }
    }
}
