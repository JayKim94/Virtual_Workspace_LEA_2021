using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.WPF.Commands;
using VirtualWorkspace_Mirzaie_Kim.WPF.State;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        private Workspace _currentWorkspace;

        public Workspace CurrentWorkspace
        {
            get { return _currentWorkspace; }
            set 
            { 
                _currentWorkspace = value;
                OnPropertyChanged(nameof(CurrentWorkspace));
            }
        }

        public ICommand UpdateViewModelCommand { get => new UpdateViewModelCommand(_navigator); }

        public SettingsViewModel(INavigator navigator)
        {
            _navigator = navigator;
            CurrentWorkspace = App.CurrentWorkspace;
        }

        public override void RefreshView()
        {
            CurrentWorkspace = App.CurrentWorkspace;
        }
    }
}
