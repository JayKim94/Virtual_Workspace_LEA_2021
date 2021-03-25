using System;
using System.Windows;
using System.Windows.Input;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;
using VirtualWorkspace_Mirzaie_Kim.WPF.Models;
using VirtualWorkspace_Mirzaie_Kim.WPF.State;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Commands
{
    public class SetCurrentWorkspaceCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private INavigator _navigator;
        private WorkspaceViewModel _viewModel;
        private IWorkspaceService _service;

        public SetCurrentWorkspaceCommand(WorkspaceViewModel viewModel, INavigator navigator, IWorkspaceService service)
        {
            _service = service;
            _viewModel = viewModel;
            _navigator = navigator;
        }

        public SetCurrentWorkspaceCommand(INavigator navigator, IWorkspaceService service)
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
            if (parameter is int && (int)parameter >= 0)
            {
                Workspace workspace = _service.GetWorkspace((int)parameter);
                
                if (App.CurrentWorkspace == null || App.CurrentWorkspace.WorkspaceId != workspace.WorkspaceId)
                {
                    App.CurrentWorkspace = workspace;
                }

                if (_viewModel != null)
                {
                    _viewModel.CurrentWorkspace = workspace;
                    _viewModel.CurrentTab = WorkspaceTabType.Current;
                }
                
                _navigator.UpdateViewModelCommand.Execute(ViewType.Workspaces);
            }
        }
    }
}
