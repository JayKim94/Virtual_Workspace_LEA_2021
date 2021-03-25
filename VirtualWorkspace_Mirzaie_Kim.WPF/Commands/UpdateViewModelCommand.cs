using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VirtualWorkspace_Mirzaie_Kim.WPF.State;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Commands
{
    public class UpdateViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly INavigator _navigator;

        public UpdateViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType)
            {
                switch ((ViewType)parameter)
                {
                    case ViewType.Home:
                        _navigator.CurrentViewModel = App.ServiceProvider.GetRequiredService<HomeViewModel>();
                        break;
                    case ViewType.Settings:
                        _navigator.CurrentViewModel = App.ServiceProvider.GetRequiredService<SettingsViewModel>();
                        break;
                    case ViewType.Workspaces:
                        _navigator.CurrentViewModel = App.ServiceProvider.GetRequiredService<WorkspaceViewModel>();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
