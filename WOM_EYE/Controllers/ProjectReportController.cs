using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WOM_EYE.Helpers;
using WOM_EYE.Interfaces.Progress;
using WOM_EYE.Interfaces.Projects;
using WOM_EYE.Interfaces.Users;
using WOM_EYE.Models.Projects;
using WOM_EYE.Models.User;

namespace WOM_EYE.Controllers
{
    public class ProjectReportController : Controller
    {
        private string myUserId;
        private string myMUserId;
        private string myUsername;
        private bool access;


        private IProjectProvider _projectProvider;
        private IDetailProjectProvider _detailProjectProvider;
        private IProgressProvider _progressProvider;
        private IUserProvider _userProvider;
        private ProjectModel _projectModel;
        private UserModel _userLogin;
        private List<ProjectModel> _listProject;

        public ProjectReportController(IProjectProvider projectProvider, IDetailProjectProvider detailProjectProvider, IProgressProvider progressProvider, IUserProvider userProvider, ProjectModel projectModel, UserModel userLogin, List<ProjectModel> listProject)
        {
            _projectProvider = projectProvider;
            _detailProjectProvider = detailProjectProvider;
            _progressProvider = progressProvider;
            _userProvider = userProvider;
            _projectModel = projectModel;
            _userLogin = userLogin;
            _listProject = listProject;
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


            ViewBag.ddlUser = _projectProvider.ddlUser();
            ViewBag.ddlStatus = _projectProvider.ddlStatus();
            ViewBag.ddlJenis = _projectProvider.ddlJenis();

            return View();

        }

        [HttpGet]
        public IActionResult GenerateExcelAllProjects()
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


            var dataProject = new List<ProjectModel>(_projectProvider.getAllProject(myUserId));
            var workbookContent = ExportExcelAllProjects.CreateWorkBook(dataProject);
            var xlsName = "Report All Project_" + DateTime.Now.ToString("yyyyMMdd") + " (Downloaded by " + myUsername + " )";


            return File(workbookContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{xlsName}.xlsx");
        }


        [HttpPost]
        public IActionResult GenerateExcelAllProjects(string downloadSolutionLeader, string downloadProjectOwner, string downloadFunctionAnalyst, string downloadProgrammer, string downloadNoProject, string downloadJenisProject, string downloadStatus, string downloadStartDate, string downloadEndDate)
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


            List<ProjectModel> dataProject = new List<ProjectModel>();

            dataProject = _projectProvider.getAllProject(myUserId, downloadSolutionLeader, downloadProjectOwner, downloadFunctionAnalyst, downloadProgrammer, downloadNoProject, downloadJenisProject, downloadStatus, downloadStartDate, downloadEndDate);

            var workbookContent = ExportExcelAllProjects.CreateWorkBook(dataProject);
            var xlsName = "Report All Project_" + DateTime.Now.ToString("yyyyMMdd");

            return File(workbookContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{xlsName}.xlsx");

        }

        [HttpPost]
        public IActionResult PreviewData(string solutionLeader, string projectOwner, string functionAnalyst, string programmer, string noProject, string jenisProject, string status, string startDate, string endDate)
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

            if (!string.IsNullOrEmpty(startDate))
            {
                if (DateTime.Parse(startDate) > DateTime.Parse(endDate))
                {
                   
                }
            }

            List<ProjectModel> dataProjects = new List<ProjectModel>();

            dataProjects = _projectProvider.getAllProject(myUserId, solutionLeader, projectOwner, functionAnalyst, programmer, noProject, jenisProject, status, startDate, endDate);



            return Json(new
            {
                data = dataProjects
            }); 
        }

    }
}
