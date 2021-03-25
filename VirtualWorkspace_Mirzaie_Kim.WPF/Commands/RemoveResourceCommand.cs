using System;
using System.Windows;
using System.Windows.Input;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Commands
{
    public class RemoveResourceCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private IResourceDirectoryService _service;

        private BaseViewModel _viewModel;

        public RemoveResourceCommand(BaseViewModel viewModel, IResourceDirectoryService service)
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
            if (MessageBox.Show(
                "Wollen Sie das Verzeichnis entfernen?",
                "Hollow Station: Workspace",
                MessageBoxButton.YesNo,
                MessageBoxImage.Information) != MessageBoxResult.Yes) return;

            if (parameter is int && (int)parameter >= 0)
            {
                _service.RemoveResourceDirectory((int)parameter);
                
                if (_viewModel != null)
                {
                    _viewModel.RefreshView();
                }
            }
        }
    }
}
