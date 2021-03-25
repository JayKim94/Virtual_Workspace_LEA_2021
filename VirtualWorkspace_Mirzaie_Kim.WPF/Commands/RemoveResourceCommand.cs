using System;
using System.Windows;
using System.Windows.Input;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;
using VirtualWorkspace_Mirzaie_Kim.WPF.Controls;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Commands
{
    public class RemoveResourceCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private IResourceDirectoryService _service;

        private WorkspaceViewModel _viewModel;

        public RemoveResourceCommand(WorkspaceViewModel viewModel, IResourceDirectoryService service)
        {
            _viewModel = viewModel;
            _service = service;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is int && (int)parameter >= 0)
            {
                int id = (int)parameter;
                
                _viewModel.ToggleDialogOpened();
                
                ResourceDirectory directory = _service.GetResourceDirectory(id);
                if (directory == null)
                {
                    _viewModel.RefreshView();
                    return;
                }

                if (new DialogWindow(
                    "Wollen Sie das Verzeichnis entfernen?",
                    $"{directory.Name}",
                    "/Images/warning.png",
                    true).ShowDialog() != true) return;

                _service.RemoveResourceDirectory((int)parameter);
                
                _viewModel.RefreshView();
            }
        }
    }
}
