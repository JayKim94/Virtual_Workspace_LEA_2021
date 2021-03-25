using System;
using System.Windows;
using System.Windows.Input;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Commands
{
    public class RemoveWorkspaceItemCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private IWorkspaceService _service;

        private BaseViewModel _viewModel;

        public RemoveWorkspaceItemCommand(BaseViewModel viewModel, IWorkspaceService service)
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
                if (MessageBox.Show(
                    "Wollen Sie diese Datei aus der Workspace entfernen?",
                    "Hollow Station: Workspace",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information) != MessageBoxResult.Yes) return;

                _service.RemoveItem((int)parameter);
                _viewModel.RefreshView();
            }
        }
    }
}
