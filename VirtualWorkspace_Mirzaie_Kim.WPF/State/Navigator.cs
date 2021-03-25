using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using VirtualWorkspace_Mirzaie_Kim.WPF.Commands;
using VirtualWorkspace_Mirzaie_Kim.WPF.Models;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.State
{
    public class Navigator : ObservableObject, INavigator
    {
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel 
        { 
            get
            {
                if (_currentViewModel == null)
                    UpdateViewModelCommand.Execute(ViewType.Home);

                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
                OnPropertyChanged(nameof(CurrentViewType));
                OnPropertyChanged(nameof(BackgroundBrush));
            }
        }

        public ViewType? CurrentViewType
        {
            get 
            {
                if (CurrentViewModel is HomeViewModel)
                {
                    return ViewType.Home;
                }
                else if (CurrentViewModel is WorkspaceViewModel)
                {
                    return ViewType.Workspaces;
                }
                else
                    return null;
            }
        }

        public SolidColorBrush BackgroundBrush 
        { 
            get
            {
                if (CurrentViewModel is HomeViewModel)
                {
                    return new SolidColorBrush(Color.FromRgb(48, 48, 48));
                }
                else
                {
                    return new SolidColorBrush(Color.FromRgb(239, 239, 239));
                }
            }
        }

        public ICommand UpdateViewModelCommand => new UpdateViewModelCommand(this);
    }
}
