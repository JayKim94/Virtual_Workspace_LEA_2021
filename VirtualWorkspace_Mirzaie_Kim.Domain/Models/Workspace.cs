using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWorkspace_Mirzaie_Kim.Domain.Models
{
    public class Workspace
    {
        public int WorkspaceId { get; set; }
        public string WorkspaceName { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<WorkspaceItem> Items { get; private set; } = new ObservableCollection<WorkspaceItem>();
    }
}