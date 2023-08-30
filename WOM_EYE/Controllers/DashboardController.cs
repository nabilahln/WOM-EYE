using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.Interfaces.Dashboard;
using WOM_EYE.Interfaces.Projects;
using WOM_EYE.Interfaces.Users;
using WOM_EYE.Models.Dashboard;
using WOM_EYE.Models.User;

namespace WOM_EYE.Controllers
{
	public class DashboardController : Controller
	{
		private string myUserId;
		private string myMUserId;
		private string myUsername;

		//private UserModel _userModel;
		private IDbConnection _dbConnection;
		private IUserProvider _userProvider;
		private IDashboardProvider _dashboardProvider;
		private List<DashboardModel> _listDashboard;
		private DashboardModel _dashboardModel;
		private IProjectProvider _projectProvider;
		private UserModel _userLogin;
        public DashboardController(IDbConnection dbConnection, IUserProvider userProvider, IDashboardProvider dashboardProvider, List<DashboardModel> listDashboard, DashboardModel dashboardModel, IProjectProvider projectProvider, UserModel userLogin)
        {
            _dbConnection = dbConnection;
            _userProvider = userProvider;
            _dashboardProvider = dashboardProvider;
            _listDashboard = listDashboard;
            _dashboardModel = dashboardModel;
            _projectProvider = projectProvider;
            _userLogin = userLogin;
        }

        [HttpGet]
		public IActionResult Index()
		{

			#region CheckSession

			myUserId = HttpContext.Session.GetString("USER_ID");
			myMUserId = HttpContext.Session.GetString("M_WOMEYE_USER_ID");
			myUsername = HttpContext.Session.GetString("USER_NAME");
			_userLogin = _userProvider.getDataUser(myUserId);
			if (!(_userProvider.checkUserSession(myUserId, myMUserId)))
			{
				TempData["MyResponseCode"] = "401";
				TempData["MyResponseMessage"] = "Please Login!";
				return RedirectToAction("Index", "Login");
			}
			else
			{
				var theMenu = JsonConvert.DeserializeObject(HttpContext.Session.GetString("MENU"));
				ViewBag.Menu = theMenu;
				ViewBag.UserId = myUserId;
				ViewBag.MUserId = myMUserId;
				ViewBag.MUserName = myUsername;
				ViewBag.UserLogin = _userLogin;
			}

			#endregion

			_dashboardModel.listDashboard = _dashboardProvider.getAllDashboard();
			_dashboardModel.listProject = _projectProvider.getAllProject();
			if (_dashboardProvider.getListProjectAssign() != null) _dashboardModel.listProjectAssign = _dashboardProvider.getListProjectAssign();
			if (_dashboardProvider.getListProjectDeploy() != null) _dashboardModel.listProjectDeploy = _dashboardProvider.getListProjectDeploy();
			if (_dashboardProvider.getListProjectDone() != null) _dashboardModel.listProjectDone = _dashboardProvider.getListProjectDone();
			if (_dashboardProvider.getListProjectHold() != null) _dashboardModel.listProjectHold = _dashboardProvider.getListProjectHold();
			if (_dashboardProvider.getListProjectTBD() != null) _dashboardModel.listProjectTBD = _dashboardProvider.getListProjectTBD();
			if (_dashboardProvider.getListProjectInProgress() != null) _dashboardModel.listProjectInProgress = _dashboardProvider.getListProjectInProgress();
			if (_dashboardProvider.getListProjectDrop() != null) _dashboardModel.listProjectDrop = _dashboardProvider.getListProjectDrop();
			_dashboardModel.ddlUser = _projectProvider.ddlUser();
			_dashboardModel.responseCode = TempData["MyResponseCode"] as string;
			_dashboardModel.responseMessage = TempData["MyResponseMessage"] as string;
			return View(_dashboardModel);
			//return View(_dashboardModel);
		}

	}
}
