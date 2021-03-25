using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VirtualWorkspace_Mirzaie_Kim.WPF.Models;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Commands
{
    public class UpdateWorkspaceTabCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private WorkspaceViewModel _viewModel;

        public UpdateWorkspaceTabCommand(WorkspaceViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is WorkspaceTabType)
            {
                switch ((WorkspaceTabType)parameter)
                {
                    case WorkspaceTabType.Current:
                        _viewModel.CurrentTab = WorkspaceTabType.Current;
                        break;
                    case WorkspaceTabType.Switch:
                        _viewModel.CurrentTab = WorkspaceTabType.Switch;
                        break;
                    default:
                        break;
                }

                _viewModel.RefreshView();
            }
        }
    }
}
