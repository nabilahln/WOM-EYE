using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.Models.Projects;

namespace WOM_EYE.Interfaces.Projects
{
	public interface IDetailProjectProvider
	{
		List<DetailProjectModel> getAllDetailProject(int id);
		List<DetailProjectModel> getAllDetailProjectNew(string projectId, string userName);
		DetailProjectModel getDetailProjectById(int id);
		//DetailProjectModel getDetailProjectByUID(string UID);
		DetailProjectModel getDetailProjectByUID(string uid, string userId);
		ResponseMessage UpdateDetailProject(DetailProjectModel form);
		ResponseMessage UpdateDetailProjectUID(DetailProjectModel form);
		ResponseMessage AddDetailProject(DetailProjectModel form);

		List<SelectListTahap> ddlTahap();

	}
}
