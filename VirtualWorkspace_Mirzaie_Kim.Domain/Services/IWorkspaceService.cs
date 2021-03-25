using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;

namespace VirtualWorkspace_Mirzaie_Kim.Domain.Services
{
    public interface IWorkspaceService
    {
        Workspace GetWorkspace(int id);
        ICollection<Workspace> GetAllWorkspaces();
        void DeleteWorkspace(int id);
        void UpdateWorkspaceName(Workspace workspace, string name);
        Workspace AddWorkspace(string name);

        WorkspaceItem AddItem(WorkspaceItem item);
        WorkspaceItem GetItem(int id);
        ICollection<WorkspaceItem> GetAllItems(int workspaceId);
        WorkspaceItem UpdateItem(WorkspaceItem item);
        void RemoveItem(int id);
    }
}
