using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF.Commands
{
    public class OpenFileBrowserCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private WorkspaceViewModel _viewModel;
        private IWorkspaceService _service;

        public OpenFileBrowserCommand(WorkspaceViewModel viewModel, IWorkspaceService service)
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
            _viewModel.ToggleDialogOpened();

            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Title = "Wählen Sie die Datei(en), die Sie importieren wollen.";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (dialog.ShowDialog() != true) { _viewModel.ToggleDialogOpened(); return; }

            FileInfo info = new FileInfo(dialog.FileName);
            
            _service.AddItem(new Domain.Models.WorkspaceItem()
            {
                WorkspaceId = App.CurrentWorkspace.WorkspaceId,
                PathToOriginal = info.FullName,
                Extension = info.Extension,
                Name = info.Name,
                LastAccessed = DateTime.Now
            });

            _viewModel.RefreshView();
        }
    }
}
