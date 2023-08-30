using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.Models.Projects;

namespace WOM_EYE.Interfaces.Projects
{
	public interface IProjectProvider
	{
		List<ProjectModel> getAllProject();
		List<ProjectModel> getAllProject(string userId);
        List<ProjectModel> getAllProject(string userId, string solutionLeader, string projectOwner, string functionAnalyst, string programmer, string noProject, string jenisProject, string status, string startDate, string endDate);
        ProjectModel getDataProjectById(int id);

		//ProjectModel getDataProjectByUID(string uid);
		ProjectModel getDataProjectByUID(string uid, string userId);
		ProjectModel getDataProjectByIdEdit(int id);
		ProjectModel getDataProjectByUIDEdit(string userId, string uid);
		List<SelectListUser> ddlUser();
		List<SelectListStatus> ddlStatus();
		List<SelectListJenis> ddlJenis();

		ResponseMessage InsertProject(ProjectModel form);
		ResponseMessage UpdateProject(ProjectModel form);
		ResponseMessage UpdateProjectUID(ProjectModel form);
	}
}
