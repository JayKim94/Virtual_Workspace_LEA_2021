using System;
using System.Windows;
using System.Windows.Input;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;
using VirtualWorkspace_Mirzaie_Kim.WPF.Controls;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Commands
{
    public class RemoveWorkspaceItemCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private IWorkspaceService _service;

        private WorkspaceViewModel _viewModel;

        public RemoveWorkspaceItemCommand(WorkspaceViewModel viewModel, IWorkspaceService service)
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
            if (parameter is int)
            {
                int id = (int)parameter;
                WorkspaceItem item = _service.GetItem(id);

                if (item == null) return;

                _viewModel.ToggleDialogOpened();
                if (new DialogWindow(
                    "Wollen Sie die Datei entfernen?",
                    $"{App.CurrentWorkspace.WorkspaceName}",
                    "/Images/warning.png",
                    true).ShowDialog() != true) return;

                _service.RemoveItem((int)parameter);
                _viewModel.RefreshView();
            }
        }
    }
}
