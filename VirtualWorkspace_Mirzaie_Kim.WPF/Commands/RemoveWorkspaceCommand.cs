﻿using System;
using System.Windows;
using System.Windows.Input;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;
using VirtualWorkspace_Mirzaie_Kim.WPF.Controls;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Commands
{
    public class RemoveWorkspaceCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private IWorkspaceService _service;

        private BaseViewModel _viewModel;

        public RemoveWorkspaceCommand(BaseViewModel viewModel, IWorkspaceService service)
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
                if (new DialogWindow(
                    "Wollen Sie das Verzeichnis entfernen?",
                    $"{App.CurrentWorkspace.WorkspaceName}",
                    "/Images/warning.png",
                    true).ShowDialog() != true) return;

                _service.DeleteWorkspace((int)parameter);
                
                _viewModel.RefreshView();
            }
        }
    }
}
