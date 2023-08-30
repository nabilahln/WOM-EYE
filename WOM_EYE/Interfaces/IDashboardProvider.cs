using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.Models.Dashboard;
using WOM_EYE.Models.Projects;

namespace WOM_EYE.Interfaces.Dashboard
{
    public interface IDashboardProvider
    {
        List<DashboardModel> getAllDashboard();
        List<ProjectModel> getListProjectAssign();
        List<ProjectModel> getListProjectInProgress();
        List<ProjectModel> getListProjectDone();
        List<ProjectModel> getListProjectHold();
        List<ProjectModel> getListProjectTBD();
        List<ProjectModel> getListProjectDeploy();
        List<ProjectModel> getListProjectDrop();
    }
}
