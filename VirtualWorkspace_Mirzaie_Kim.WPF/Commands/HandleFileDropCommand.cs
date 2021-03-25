using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;
using VirtualWorkspace_Mirzaie_Kim.WPF.Controls;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Commands
{
    public class HandleFileDropCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private BaseViewModel _viewModel;
        private IWorkspaceService _workspaceService;
        private Workspace _workspace;
        private IResourceDirectoryService _resourceDirectoryService;

        public HandleFileDropCommand(BaseViewModel viewModel, Workspace workspace, IWorkspaceService wsService, IResourceDirectoryService rdService)
        {
            _viewModel = viewModel;
            _workspaceService = wsService;
            _resourceDirectoryService = rdService;
            _workspace = workspace;
        }
        
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is string && (string)parameter != string.Empty)
            {
                string path = parameter as string;

                // Get file information
                FileInfo file = new FileInfo(path);

                // Is workspace item
                if (file.Extension != string.Empty)
                {
                    _workspaceService.AddItem(new WorkspaceItem()
                    {
                        WorkspaceId = _workspace.WorkspaceId,
                        Name = Path.GetFileNameWithoutExtension(path),
                        Extension = file.Extension,
                        PathToOriginal = file.FullName,
                        LastAccessed = DateTime.Now
                    });
                    new DialogWindow("Datei gespeichert!", file.FullName).ShowDialog();
                }
                // Is resource directory
                else
                {
                    _resourceDirectoryService.AddResourceDirectory(new ResourceDirectory()
                    {
                        Name = file.Name,
                        Path = file.FullName,
                        LastAccessed = DateTime.Now
                    });
                    new DialogWindow("Ressource gespeichert!", file.Name).ShowDialog();
                }

                _viewModel.RefreshView();
            }
        }
    }
}
