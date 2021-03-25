using System;

namespace VirtualWorkspace_Mirzaie_Kim.Domain.Models
{
    public class WorkspaceItem
    {
        public int WorkspaceItemId { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string PathToOriginal { get; set; }
        public DateTime LastAccessed { get; set; }

        public int WorkspaceId { get; set; }
        public virtual Workspace Workspace { get; set; }
    }
}
