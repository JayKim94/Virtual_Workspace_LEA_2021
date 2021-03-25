using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;
using VirtualWorkspace_Mirzaie_Kim.WPF.State;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Commands
{
    public class CreateNewWorkspaceCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private IWorkspaceService _service;
        private WorkspaceViewModel _viewModel;
        private INavigator _navigator;
        private string defaultName = "Neuer Arbeitsbereich*";

        public CreateNewWorkspaceCommand(WorkspaceViewModel viewModel, IWorkspaceService service)
        {
            _viewModel = viewModel;
            _service = service;
        }

        public CreateNewWorkspaceCommand(IWorkspaceService service, INavigator navigator)
        {
            _service = service;
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (MessageBox.Show(
                "Wollen Sie eine neue Workspace erstellen?",
                "Hollow Station: Workspace",
                MessageBoxButton.YesNo,
                MessageBoxImage.Information) != MessageBoxResult.Yes) return;

            App.CurrentWorkspace = _service.AddWorkspace(defaultName);

            if (_viewModel == null)
            {
                _navigator.UpdateViewModelCommand.Execute(ViewType.Workspaces);
            }
            else
            {
                _viewModel.SetCurrentWorkspaceCommand.Execute(App.CurrentWorkspace.WorkspaceId);
            }
        }
    }
}
