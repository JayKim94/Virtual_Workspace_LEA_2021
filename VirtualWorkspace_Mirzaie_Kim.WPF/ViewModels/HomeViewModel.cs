using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;
using VirtualWorkspace_Mirzaie_Kim.WPF.Commands;
using VirtualWorkspace_Mirzaie_Kim.WPF.State;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Private

        private ICollectionView _loadedWorkspaces;
        private INavigator _navigator;
        private IWorkspaceService _workspaceService;

        #endregion

        #region Observable Properties

        public ICollectionView LoadedWorkspaces
        {
            get 
            { 
                return _loadedWorkspaces;
            }
            set 
            { 
                _loadedWorkspaces = value;
                OnPropertyChanged("LoadedWorkspaces");
            }
        }

        #endregion

        #region Commands

        public ICommand SetCurrentWorkspaceCommand { get => new SetCurrentWorkspaceCommand(_navigator, _workspaceService); }
        
        public ICommand RemoveWorkspaceCommand { get => new RemoveWorkspaceCommand(this, _workspaceService); }
        
        public ICommand CreateNewWorkspaceCommand { get => new CreateNewWorkspaceCommand(_workspaceService, _navigator); }

        #endregion

        public HomeViewModel(IWorkspaceService workspaceService, INavigator navigator)
        {
            _workspaceService = workspaceService;
            _navigator = navigator;

            LoadedWorkspaces = CollectionViewSource.GetDefaultView(_workspaceService.GetAllWorkspaces());
        }

        public override void RefreshView()
        {
            LoadedWorkspaces.Refresh();
        }
    }
}
