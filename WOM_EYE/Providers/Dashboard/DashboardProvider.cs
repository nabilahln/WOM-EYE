using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.Interfaces.Dashboard;
using WOM_EYE.Models.Dashboard;
using WOM_EYE.Models.Projects;

namespace WOM_EYE.Providers.Dashboard
{
    public class DashboardProvider : IDashboardProvider
    {
        private IDbConnection _dbConnection;
        private DashboardModel _dashboardModel;
        private List<DashboardModel> _listDashboard;

        public DashboardProvider(IDbConnection dbConnection, DashboardModel dashboardModel, List<DashboardModel> listDashboard)
        {
            _dbConnection = dbConnection;
            _dashboardModel = dashboardModel;
            _listDashboard = listDashboard;
        }
        public List<DashboardModel> getAllDashboard()
        {
            string spName = "spWOMEYE_Dashboard";

            var dataDashboard = _dbConnection.ExecuteReader(spName, commandType: CommandType.StoredProcedure, commandTimeout: 30);

            while (dataDashboard.Read())
            {
                DashboardModel Dashboard = new DashboardModel();
                Dashboard.GROUP_NAME = dataDashboard[0].ToString();
                Dashboard.JUMLAH = dataDashboard[1].ToString();

                _listDashboard.Add(Dashboard);
            }

            dataDashboard.Close();
            return _listDashboard;
        }

        List<ProjectModel> IDashboardProvider.getListProjectAssign()
        {
            List<ProjectModel> listAssign = new List<ProjectModel>();

            string spName = "spWOMEYE_AllProject_Assign";

            var dataProject = _dbConnection.ExecuteReader(spName, commandType: CommandType.StoredProcedure, commandTimeout: 30);

            if (dataProject != null)
            {

                while (dataProject.Read())
                {
                    ProjectModel project = new ProjectModel();
                    project.T_WOMEYE_PROJECT_ID = System.Convert.ToInt32(dataProject[0]);
                    project.SOL_LEADER = dataProject[1].ToString();
                    project.PROJECT_LEADER = dataProject[2].ToString();
                    project.JENIS_PROJECT = dataProject[3].ToString();
                    project.NO_PROJECT = dataProject[4].ToString();
                    project.DESKRIPSI = dataProject[5].ToString();
                    project.FUNCTION_ANALYST = dataProject[6].ToString();
                    project.PROGRAMMER = dataProject[7].ToString();
                    project.STATUS = dataProject[8].ToString();
                    project.CATATAN = dataProject[9].ToString();
                    project.DTM_CRT = dataProject[10].ToString();
                    project.GROUP_NAME = dataProject[11].ToString();
                    listAssign.Add(project);
                }

                dataProject.Close();
            }
            return listAssign;
        }

        List<ProjectModel> IDashboardProvider.getListProjectDeploy()
        {

            List<ProjectModel> listDeploy = new List<ProjectModel>();

            string spName = "spWOMEYE_AllProject_Deploy";


            var dataProject = _dbConnection.ExecuteReader(spName, commandType: CommandType.StoredProcedure, commandTimeout: 30);


            if (dataProject != null)
            {
                while (dataProject.Read())

                {
                    ProjectModel project = new ProjectModel();
                    project.T_WOMEYE_PROJECT_ID = System.Convert.ToInt32(dataProject[0]);
                    project.SOL_LEADER = dataProject[1].ToString();
                    project.PROJECT_LEADER = dataProject[2].ToString();
                    project.JENIS_PROJECT = dataProject[3].ToString();
                    project.NO_PROJECT = dataProject[4].ToString();
                    project.DESKRIPSI = dataProject[5].ToString();
                    project.FUNCTION_ANALYST = dataProject[6].ToString();
                    project.PROGRAMMER = dataProject[7].ToString();
                    project.STATUS = dataProject[8].ToString();
                    project.CATATAN = dataProject[9].ToString();
                    project.DTM_CRT = dataProject[10].ToString();
                    project.GROUP_NAME = dataProject[11].ToString();
                    listDeploy.Add(project);
                }

                dataProject.Close();
            }
            return listDeploy;
        }

        List<ProjectModel> IDashboardProvider.getListProjectDone()
        {
            List<ProjectModel> listDone = new List<ProjectModel>();

            string spName = "spWOMEYE_AllProject_Done";

            var dataProject = _dbConnection.ExecuteReader(spName, commandType: CommandType.StoredProcedure, commandTimeout: 30);

            if (dataProject != null)
            {
                while (dataProject.Read())
                {
                    ProjectModel project = new ProjectModel();
                    project.T_WOMEYE_PROJECT_ID = System.Convert.ToInt32(dataProject[0]);
                    project.SOL_LEADER = dataProject[1].ToString();
                    project.PROJECT_LEADER = dataProject[2].ToString();
                    project.JENIS_PROJECT = dataProject[3].ToString();
                    project.NO_PROJECT = dataProject[4].ToString();
                    project.DESKRIPSI = dataProject[5].ToString();
                    project.FUNCTION_ANALYST = dataProject[6].ToString();
                    project.PROGRAMMER = dataProject[7].ToString();
                    project.STATUS = dataProject[8].ToString();
                    project.CATATAN = dataProject[9].ToString();
                    project.DTM_CRT = dataProject[10].ToString();
                    project.GROUP_NAME = dataProject[11].ToString();
                    listDone.Add(project);
                }

                dataProject.Close();
            }

            return listDone;
        }

        List<ProjectModel> IDashboardProvider.getListProjectDrop()
        {
            List<ProjectModel> listDrop = new List<ProjectModel>();

            string spName = "spWOMEYE_AllProject_Drop";

            var dataProject = _dbConnection.ExecuteReader(spName, commandType: CommandType.StoredProcedure, commandTimeout: 30);

            if (dataProject != null)
            {
                while (dataProject.Read())
                {
                    ProjectModel project = new ProjectModel();
                    project.T_WOMEYE_PROJECT_ID = System.Convert.ToInt32(dataProject[0]);
                    project.SOL_LEADER = dataProject[1].ToString();
                    project.PROJECT_LEADER = dataProject[2].ToString();
                    project.JENIS_PROJECT = dataProject[3].ToString();
                    project.NO_PROJECT = dataProject[4].ToString();
                    project.DESKRIPSI = dataProject[5].ToString();
                    project.FUNCTION_ANALYST = dataProject[6].ToString();
                    project.PROGRAMMER = dataProject[7].ToString();
                    project.STATUS = dataProject[8].ToString();
                    project.CATATAN = dataProject[9].ToString();
                    project.DTM_CRT = dataProject[10].ToString();
                    project.GROUP_NAME = dataProject[11].ToString();
                    listDrop.Add(project);
                }

                dataProject.Close();
            }

            return listDrop;
        }

        List<ProjectModel> IDashboardProvider.getListProjectHold()
        {
            List<ProjectModel> listHold = new List<ProjectModel>();

            string spName = "spWOMEYE_AllProject_Hold";

            var dataProject = _dbConnection.ExecuteReader(spName, commandType: CommandType.StoredProcedure, commandTimeout: 30);

            if (dataProject != null)
            {
                while (dataProject.Read())
                {
                    ProjectModel project = new ProjectModel();
                    project.T_WOMEYE_PROJECT_ID = System.Convert.ToInt32(dataProject[0]);
                    project.SOL_LEADER = dataProject[1].ToString();
                    project.PROJECT_LEADER = dataProject[2].ToString();
                    project.JENIS_PROJECT = dataProject[3].ToString();
                    project.NO_PROJECT = dataProject[4].ToString();
                    project.DESKRIPSI = dataProject[5].ToString();
                    project.FUNCTION_ANALYST = dataProject[6].ToString();
                    project.PROGRAMMER = dataProject[7].ToString();
                    project.STATUS = dataProject[8].ToString();
                    project.CATATAN = dataProject[9].ToString();
                    project.DTM_CRT = dataProject[10].ToString();
                    project.GROUP_NAME = dataProject[11].ToString();
                    listHold.Add(project);
                }

                dataProject.Close();
            }

            return listHold;
        }

        List<ProjectModel> IDashboardProvider.getListProjectInProgress()
        {
            List<ProjectModel> listInProgress = new List<ProjectModel>();

            string spName = "spWOMEYE_AllProject_InProgress";

            var dataProject = _dbConnection.ExecuteReader(spName, commandType: CommandType.StoredProcedure, commandTimeout: 30);

            if (dataProject != null)
            {
                while (dataProject.Read())
                {
                    ProjectModel project = new ProjectModel();
                    project.T_WOMEYE_PROJECT_ID = System.Convert.ToInt32(dataProject[0]);
                    project.SOL_LEADER = dataProject[1].ToString();
                    project.PROJECT_LEADER = dataProject[2].ToString();
                    project.JENIS_PROJECT = dataProject[3].ToString();
                    project.NO_PROJECT = dataProject[4].ToString();
                    project.DESKRIPSI = dataProject[5].ToString();
                    project.FUNCTION_ANALYST = dataProject[6].ToString();
                    project.PROGRAMMER = dataProject[7].ToString();
                    project.STATUS = dataProject[8].ToString();
                    project.CATATAN = dataProject[9].ToString();
                    project.DTM_CRT = dataProject[10].ToString();
                    project.GROUP_NAME = dataProject[11].ToString();
                    listInProgress.Add(project);
                }

                dataProject.Close();
            }

            return listInProgress;
        }

        List<ProjectModel> IDashboardProvider.getListProjectTBD()
        {
            List<ProjectModel> listTBD = new List<ProjectModel>();

            string spName = "spWOMEYE_AllProject_TBD";

            var dataProject = _dbConnection.ExecuteReader(spName, commandType: CommandType.StoredProcedure, commandTimeout: 30);

            if (dataProject != null)
            {
                while (dataProject.Read())
                {
                    ProjectModel project = new ProjectModel();
                    project.T_WOMEYE_PROJECT_ID = System.Convert.ToInt32(dataProject[0]);
                    project.SOL_LEADER = dataProject[1].ToString();
                    project.PROJECT_LEADER = dataProject[2].ToString();
                    project.JENIS_PROJECT = dataProject[3].ToString();
                    project.NO_PROJECT = dataProject[4].ToString();
                    project.DESKRIPSI = dataProject[5].ToString();
                    project.FUNCTION_ANALYST = dataProject[6].ToString();
                    project.PROGRAMMER = dataProject[7].ToString();
                    project.STATUS = dataProject[8].ToString();
                    project.CATATAN = dataProject[9].ToString();
                    project.DTM_CRT = dataProject[10].ToString();
                    project.GROUP_NAME = dataProject[11].ToString();
                    listTBD.Add(project);
                }

                dataProject.Close();
            }

            return listTBD;
        }
    }
}
