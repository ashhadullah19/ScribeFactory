using Scribe_Factory_Core.Common;
using Scribe_Factory_Core.Models;
using Scribe_Factory_Core.Repositories.Interfaces;
using Scribe_Factory_Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scribe_Factory_Core.Repositories
{
    public class ProjectManagementRepository : IProjectManagementRepository
    {
        ScribeFactoryContext scribeFactoryContext = null;
        public ProjectManagementRepository(ScribeFactoryContext scribeFactoryContext)
        {
            this.scribeFactoryContext = scribeFactoryContext;
        }

        public List<Project> GetProjects(UserLoginViewModel currentuser)
        {
            var Allprojects = scribeFactoryContext.Projects.Where(x => x.ApplicationUserId == currentuser.User.Id).ToList();
            return Allprojects;
        }
        public List<Project> GetProjectsByFilter(UserLoginViewModel currentuser, string projectname, string type, string date)
        {
            var Allprojects = scribeFactoryContext.Projects.Where(x => x.ApplicationUserId == currentuser.User.Id).ToList();
            if (projectname != null)
            {
                Allprojects = Allprojects.Where(x => x.ProjectName == projectname).ToList();
            }
            if (type != null)
            {
                Allprojects = Allprojects.Where(x => x.ProjectType == type).ToList();
            }
            if (date != null)
            {
                Allprojects = Allprojects.Where(x => x.CreatedOn.Date.ToString("yyyy-MM-dd") == date).ToList();
            }
            return Allprojects;
        }
    public List<DashbaordProjectCountViewModel> GetProjectsByCategoryCount(UserLoginViewModel currentuser)
    {
        var data = scribeFactoryContext.Projects.Where(x => x.ApplicationUserId == currentuser.User.Id).GroupBy(info => info.ProjectType)
                    .Select(group => new DashbaordProjectCountViewModel()
                    {
                        ProjectType = group.Key,
                        Count = group.Count()
                    }).ToList();
        return data;
    }
    public List<ProjectFiles> GetProjectFiles(int projectId)
    {
        var projectFiles = scribeFactoryContext.ProjectFiles.Where(x => x.ProjectId == projectId).ToList();
        return projectFiles;
    }
    public int SaveProject(string projectName, string projectType, long tenantId)
    {
        try
        {
            Project Newproject = new Project();
            Newproject.ProjectName = projectName;
            Newproject.ProjectType = projectType;
            Newproject.ApplicationUserId = tenantId;
            //add time of creation in future (need to update db)
            scribeFactoryContext.Projects.Add(Newproject);
            scribeFactoryContext.SaveChanges();
            return Newproject.Id;
        }
        catch
        {
            return -1;
        }
    }
    public int SaveProjectFile(string projectFile, long projectId)
    {
        try
        {
            ProjectFiles projectFiles = new ProjectFiles();
            projectFiles.ProjectId = Convert.ToInt32(projectId);
            projectFiles.FileUrl = projectFile;
            //add time of creation in future (need to update db)
            scribeFactoryContext.ProjectFiles.Add(projectFiles);
            scribeFactoryContext.SaveChanges();
            return projectFiles.Id;
        }
        catch
        {
            return -1;
        }
    }
    public ProjectFiles getProjectFile(int projectId)
    {
        return scribeFactoryContext.ProjectFiles.Where(x => x.ProjectId == projectId).FirstOrDefault();
    }
    public List<Languages> GetLanguagesFromDB()
    {
        return scribeFactoryContext.Languages.ToList();
        //return new List<Languages>();
    }
}
}
