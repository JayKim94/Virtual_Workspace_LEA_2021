using System;

namespace VirtualWorkspace_Mirzaie_Kim.Domain.Models
{
    public class ResourceDirectory
    {
        public int ResourceDirectoryId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime LastAccessed { get; set; }
    }
}
