using System;
using System.Windows.Input;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Commands
{
    public class UpdateWorkspaceNameCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private BaseViewModel _viewModel;
        private IWorkspaceService _service;
        private Workspace _workspace;

        public UpdateWorkspaceNameCommand(BaseViewModel viewModel, IWorkspaceService service, Workspace workspace)
        {
            _workspace = workspace;
            _service = service;
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is string)
            {
                string name = parameter as string;

                if (name != null)
                    _service.UpdateWorkspaceName(_workspace, name);
            }

            _viewModel.RefreshView();
        }
    }
}
