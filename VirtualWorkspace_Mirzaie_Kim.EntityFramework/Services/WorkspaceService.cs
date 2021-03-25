using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;

namespace VirtualWorkspace_Mirzaie_Kim.EntityFramework.Services
{
    public class WorkspaceService : IWorkspaceService
    {
        private readonly VirtualWorkspaceDbContext _context;

        public WorkspaceService()
        {
            _context = new VirtualWorkspaceDbContext();
            _context.Database.EnsureCreated();

            _context.Workspaces.Load();
            _context.WorkspaceItems.Load();
        }

        public ICollection<Workspace> GetAllWorkspaces()
        {
            return _context.Workspaces.Local.ToObservableCollection();
        }

        public Workspace GetWorkspace(int id)
        {
            return _context.Workspaces.Find(id);
        }

        public void DeleteWorkspace(int id)
        {
            _context.Workspaces.Remove(GetWorkspace(id));
            _context.SaveChanges();
        }

        public void UpdateWorkspaceName(Workspace workspace, string name)
        {
            _context.Workspaces
                .Find(workspace.WorkspaceId)
                .WorkspaceName = name;

            _context.SaveChanges();
        }

        public Workspace AddWorkspace(string name)
        {
            Workspace workspace = _context.Workspaces.Add(new Workspace()
            {
                WorkspaceName = name,
                CreatedAt = DateTime.Now
            }).Entity;

            _context.SaveChanges();

            return workspace;
        }

        #region Workspace Item Methods

        public WorkspaceItem AddItem(WorkspaceItem item)
        {
            WorkspaceItem entity = _context.WorkspaceItems.Add(item).Entity;
            entity.LastAccessed = DateTime.Now;
            _context.SaveChanges();

            return entity;
        }

        public ICollection<WorkspaceItem> GetAllItems(int workspaceId)
        {
            return _context.Workspaces.Find(workspaceId).Items;
        }

        public WorkspaceItem GetItem(int id)
        {
            WorkspaceItem entity = _context.WorkspaceItems.Find(id);
            entity.LastAccessed = DateTime.Now;

            return entity;
        }

        public void RemoveItem(int id)
        {
            WorkspaceItem target = GetItem(id);
            _context.WorkspaceItems.Remove(target);
            _context.SaveChanges();
        }

        public WorkspaceItem UpdateItem(WorkspaceItem item)
        {
            WorkspaceItem entity = _context.WorkspaceItems.Update(item).Entity;
            entity.LastAccessed = DateTime.Now;

            _context.SaveChanges();

            return entity;
        }

        #endregion
    }
}
