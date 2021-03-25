using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;

namespace VirtualWorkspace_Mirzaie_Kim.EntityFramework.Services
{
    public class ResourceDirectoryService : IResourceDirectoryService
    {
        private readonly VirtualWorkspaceDbContext _context;

        public ResourceDirectoryService()
        {
            _context = new VirtualWorkspaceDbContext();
            _context.Database.EnsureCreated();

            _context.ResourceDirectories.Load();
        }

        public ResourceDirectory AddResourceDirectory(ResourceDirectory resource)
        {
            ResourceDirectory target = _context.ResourceDirectories.Add(resource).Entity;
            _context.SaveChanges();

            return target;
        }

        public ICollection<ResourceDirectory> GetAllResourceDirectories()
        {
            return _context.ResourceDirectories.Local.ToObservableCollection();
        }

        public ResourceDirectory GetResourceDirectory(int id)
        {
            return _context.ResourceDirectories.Find(id);
        }

        public void RemoveResourceDirectory(int id)
        {
            ResourceDirectory target = GetResourceDirectory(id);
            _context.ResourceDirectories.Remove(target);
            _context.SaveChanges();
        }

        public ResourceDirectory UpdateResourceDirectory(ResourceDirectory resource)
        {
            ResourceDirectory target = _context.ResourceDirectories.Update(resource).Entity;
            _context.SaveChanges();

            return target;
        }
    }
}
