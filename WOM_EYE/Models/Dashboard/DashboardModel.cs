using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.Models.Projects;

namespace WOM_EYE.Models.Dashboard
{
    public class DashboardModel : ResponseModel
    {
        //public int M_WOMEYE_STATUS_PROJECT_ID { get; set; }
        public string GROUP_NAME { get; set; }
        public string JUMLAH { get; set; }
        public string SOL_LEADER { get; set; }
        public List<DashboardModel> listDashboard { get; set; }
        public List<ProjectModel> listProject { get; set; }
        public List<ProjectModel> listProjectAssign { get; set; }
        public List<ProjectModel> listProjectInProgress { get; set; }
        public List<ProjectModel> listProjectDone { get; set; }
        public List<ProjectModel> listProjectHold { get; set; }
        public List<ProjectModel> listProjectTBD { get; set; }
        public List<ProjectModel> listProjectDeploy { get; set; }
        public List<ProjectModel> listProjectDrop { get; set; }
        public List<SelectListUser> ddlUser { get; set; }
    }
}
