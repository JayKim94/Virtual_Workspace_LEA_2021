using System.Collections.Generic;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;

namespace VirtualWorkspace_Mirzaie_Kim.Domain.Services
{
    public interface IResourceDirectoryService
    {
        ResourceDirectory AddResourceDirectory(ResourceDirectory resource);
        ResourceDirectory GetResourceDirectory(int id);
        ICollection<ResourceDirectory> GetAllResourceDirectories();
        void RemoveResourceDirectory(int id);
        ResourceDirectory UpdateResourceDirectory(ResourceDirectory resource);
    }
}
