﻿using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;
using VirtualWorkspace_Mirzaie_Kim.WPF.Commands;
using VirtualWorkspace_Mirzaie_Kim.WPF.Controls;
using VirtualWorkspace_Mirzaie_Kim.WPF.Models;
using VirtualWorkspace_Mirzaie_Kim.WPF.State;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels
{
    public class WorkspaceViewModel : BaseViewModel
    {
        #region Private

        private Workspace _currentWorkspace;
        private WorkspaceTabType _currentTab = WorkspaceTabType.Current;
        private string _searchedWorkspaceName;
        private bool _isDialogOpen = false;
        private bool _isBusy = false;
        private ICollectionView _loadedWorkspaces;
        private ICollectionView _loadedItems;
        private ICollectionView _loadedResources;
        private WorkspaceItem _currentSelectedItem;
        private SpotifyAuthState _spotifyAuthState;

        // Dependencies
        private readonly IWorkspaceService _workspaceService;
        private readonly IResourceDirectoryService _resourceService;
        private readonly INavigator _navigator;
        private readonly ISpotifyControllerService _spotify;

        #endregion

        #region Observable Properties

        public Workspace CurrentWorkspace
        {
            get { return _currentWorkspace; }
            set 
            { 
                _currentWorkspace = value;
                OnPropertyChanged(nameof(CurrentWorkspace));
            }
        }

        public WorkspaceItem CurrentSelectedItem
        {
            get { return _currentSelectedItem; }
            set 
            { 
                _currentSelectedItem = value;
                OnPropertyChanged(nameof(CurrentSelectedItem));
            }
        }

        public SpotifyAuthState SpotifyAuthState 
        {
            get => _spotifyAuthState;
            set
            {
                _spotifyAuthState = value;
                OnPropertyChanged(nameof(SpotifyAuthState));
            }
        }

        public WorkspaceTabType CurrentTab
        {
            get
            {
                return _currentTab;
            }
            set
            {
                _currentTab = value;
                OnPropertyChanged(nameof(CurrentTab));
            }
        }

        public ICollectionView LoadedWorkspaces
        {
            get
            {
                return _loadedWorkspaces;
            }
            set
            {
                _loadedWorkspaces = value;
                OnPropertyChanged(nameof(LoadedWorkspaces));
            }
        }

        public ICollectionView LoadedItems 
        { 
            get
            {
                return _loadedItems;
            }
            set
            {
                _loadedItems = value;
                OnPropertyChanged(nameof(LoadedItems));
            }
        }

        public ICollectionView LoadedResources 
        { 
            get
            {
                return _loadedResources;
            }
            set
            {
                _loadedResources = value;
                OnPropertyChanged(nameof(LoadedResources));
            }
        }

        public bool IsDialogOpen
        {
            get { return _isDialogOpen; }
            set
            {
                _isDialogOpen = value;
                OnPropertyChanged(nameof(IsDialogOpen));
            }
        }
        
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        #endregion

        #region ICommands

        #region WORKSPACE

        public ICommand CreateNewWorkspaceCommand { get => new CreateNewWorkspaceCommand(this, _workspaceService); }

        public ICommand RemoveWorkspaceCommand { get => new RemoveWorkspaceCommand(this, _workspaceService); }

        public ICommand UpdateWorkspaceNameCommand { get => new UpdateWorkspaceNameCommand(this, _workspaceService, CurrentWorkspace); }
        
        public ICommand SetCurrentWorkspaceCommand { get => new SetCurrentWorkspaceCommand(_navigator, _workspaceService); }

        #endregion

        #region WORKSPACE ITEMS

        public ICommand RemoveWorkspaceItemCommand { get => new RemoveWorkspaceItemCommand(this, _workspaceService); }
        
        public ICommand HandleFileDropCommand { get => new HandleFileDropCommand(this, _workspaceService, _resourceService); }

        public ICommand OpenFileBrowserCommand { get => new OpenFileBrowserCommand(this, _workspaceService); }

        #endregion

        #region RESOURCES

        public ICommand OpenDirectoryBrowserCommand { get => new OpenDirectoryBrowserCommand(this, _resourceService); }
        
        public ICommand RemoveResourceCommand { get => new RemoveResourceCommand(this, _resourceService); }

        #endregion

        public ICommand UpdateWorkspaceTabCommand { get => new UpdateWorkspaceTabCommand(this); }

        public ICommand UpdateViewModelCommand { get => new UpdateViewModelCommand(_navigator); }

        public ICommand ImportResourceCommand { get => new ImportResourceCommand(this, _workspaceService, _resourceService); }

        public ICommand OpenExportDialogCommand { get => new OpenExportDialogCommand(this, _workspaceService); }

        public ICommand OpenFileInfoCommand { get => new GeneralCommand(OpenFileInfo); }

        #region SPOTIFY

        public ICommand PauseSpotifyCommand { get => new GeneralCommand(PauseTrack); }
        
        public ICommand PlaySpotifyCommand { get => new GeneralCommand(PlayTrack); }

        public ICommand NextSpotifyCommand { get => new GeneralCommand(NextTrack); }
        
        public ICommand PreviousSpotifyCommand { get => new GeneralCommand(PreviousTrack); }
        
        public ICommand AuthentificateSpotifyCommand { get => new GeneralCommand(Authentificate); }

        #endregion

        #endregion

        public WorkspaceViewModel(
            IWorkspaceService workspaceService,
            IResourceDirectoryService resourceService,
            INavigator navigator,
            ISpotifyControllerService spotify)
        {
            // Get services
            _navigator = navigator;
            _workspaceService = workspaceService;
            _resourceService = resourceService;
            _spotify = spotify;

            if (App.CurrentWorkspace == null) throw new Exception();
            // Loads data and builds the view
            CurrentWorkspace = App.CurrentWorkspace;

            LoadedWorkspaces = CollectionViewSource.GetDefaultView(_workspaceService.GetAllWorkspaces());
            LoadedItems = CollectionViewSource.GetDefaultView(_workspaceService.GetAllItems(CurrentWorkspace.WorkspaceId));
            LoadedResources = CollectionViewSource.GetDefaultView(_resourceService.GetAllResourceDirectories());

            SpotifyAuthState = new SpotifyAuthState();
        }

        #region UI-Event Handlers

        public void FilterWorkspaces(object param)
        {
            if (param is not string) return;

            string name = param as string;
            if (name != null && name != string.Empty)
            {
                LoadedWorkspaces.Filter = (workspace) =>
                {
                    return (workspace as Workspace).WorkspaceName.Contains(name, StringComparison.InvariantCultureIgnoreCase);
                };
            }
            else
            {
                LoadedWorkspaces.Filter = null;
            }

            LoadedWorkspaces.Refresh();
        }

        public void FilterItems(object param)
        {
            if (param is not string) return;

            string name = param as string;
            if (name != null && name != string.Empty)
            {
                LoadedItems.Filter = (item) =>
                {
                    WorkspaceItem workspaceItem = item as WorkspaceItem;

                    return workspaceItem.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)
                    || workspaceItem.Extension.Contains(name, StringComparison.InvariantCultureIgnoreCase);
                };
            }
            else
            {
                LoadedItems.Filter = null;
            }

            LoadedItems.Refresh();
        }

        public void FocusWorkspaceItem(int id)
        {
            WorkspaceItem item = _workspaceService.GetItem(id);

            CurrentSelectedItem = item;
        }

        #endregion

        private async void Authentificate(object parameter)
        {
            IsBusy = true;

            SpotifyAuthState.Token = await _spotify.Authentificate();
            if (SpotifyAuthState.Token == null) return;

            SpotifyAuthState.PlayerInfo = _spotify.GetPlayerInfo();
            SpotifyAuthState.TrackInfo = _spotify.GetCurrentTrackInfo();
            
            if (SpotifyAuthState.PlayerInfo == null || SpotifyAuthState.TrackInfo == null)
            {
                SpotifyAuthState.Token = null;
                new DialogWindow(
                    "Etwas ist schiefgegangen...",
                    $"Versuchen Sie bitte erneut",
                    @"/Images/warning.png")
                    .ShowDialog();
            }
            else
            {
                new DialogWindow(
                    "Ihr Konto ist jetzt verbunden!",
                    $"spielt gerade auf {SpotifyAuthState.PlayerInfo.Device.Name}")
                    .ShowDialog();
            }

            IsBusy = false;
        }

        private void PauseTrack(object parameter)
        {
            if (SpotifyAuthState.IsNotSigned) return;

            SpotifyPlayerInfo info = _spotify.Pause();
            SpotifyAuthState.PlayerInfo = info;
        }

        private void PlayTrack(object parameter)
        {
            if (SpotifyAuthState.IsNotSigned) return;

            SpotifyPlayerInfo info = _spotify.Play();
            SpotifyAuthState.PlayerInfo = info;
        }

        private void NextTrack(object parameter)
        {
            if (SpotifyAuthState.IsNotSigned) return;

            SpotifyTrackInfo info = _spotify.NextTrack();
            SpotifyAuthState.TrackInfo = info;
        }

        private void PreviousTrack(object parameter)
        {
            if (SpotifyAuthState.IsNotSigned) return;

            SpotifyTrackInfo info = _spotify.PreviousTrack();
            SpotifyAuthState.TrackInfo = info;
        }

        private void OpenFileInfo(object parameter)
        {
            new FileInfoWindow(CurrentSelectedItem).ShowDialog();
        }

        public void ToggleSpinner()
        {
            IsBusy = !IsBusy;
        }

        public void ToggleDialogOpened()
        {
            IsDialogOpen = !IsDialogOpen;
        }

        public override void RefreshView()
        {
            IsBusy = false;
            IsDialogOpen = false;

            CurrentWorkspace = App.CurrentWorkspace;
            LoadedItems.Refresh();
            LoadedWorkspaces.Refresh();
            LoadedResources.Refresh();
        }
    }
}
