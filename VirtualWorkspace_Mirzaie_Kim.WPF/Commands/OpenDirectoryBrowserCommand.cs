using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Commands
{
    public class OpenDirectoryBrowserCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly WorkspaceViewModel _viewModel;
        private readonly IResourceDirectoryService _service;

        public OpenDirectoryBrowserCommand(WorkspaceViewModel viewModel, IResourceDirectoryService service)
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
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.InitialDirectory = "C:\\";
                dialog.IsFolderPicker = true;

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    string path = dialog.FileName;
                    string name = path.Split('\\').Last();
                    _service.AddResourceDirectory(new Domain.Models.ResourceDirectory()
                    {
                        Name = name,
                        Path = path,
                        LastAccessed = DateTime.Now
                    });

                    _viewModel.RefreshView();
                }
            }
        }
    }
}
