using Scribe_Factory_Core.Models;
using Scribe_Factory_Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scribe_Factory_Core.Repositories.Interfaces
{
    public interface IProjectManagementRepository
    {
        public List<Project> GetProjects(UserLoginViewModel currentuser);
        public List<ProjectFiles> GetProjectFiles(int projectId);
        public int SaveProject(string projectName, string projectType, long tenantId);
        public List<Languages> GetLanguagesFromDB();
        public int SaveProjectFile(string projectFile, long projectId);
        public ProjectFiles getProjectFile(int projectId);

    }
}
