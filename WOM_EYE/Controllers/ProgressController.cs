using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.Interfaces.Progress;
using WOM_EYE.Interfaces.Projects;
using WOM_EYE.Interfaces.Users;
using WOM_EYE.Models.Progress;
using WOM_EYE.Models.Projects;
using WOM_EYE.Models.User;

namespace WOM_EYE.Controllers
{
	public class ProgressController : Controller
	{
	
		private string myUserId;
		private string myMUserId;
		private string myUsername;
		private bool access;

		private readonly IDbConnection _dbConnection;
		private ProgressModel _progressModel;
		private IProgressProvider _progressProvider;
		private IUserProvider _userProvider;
		private UserModel _userLogin;

		private ProjectModel _projectModel;
		private IProjectProvider _projectProvider;

        public ProgressController(IDbConnection dbConnection, ProgressModel progressModel, IProgressProvider progressProvider, IUserProvider userProvider, UserModel userLogin, ProjectModel projectModel, IProjectProvider projectProvider)
        {
            _dbConnection = dbConnection;
            _progressModel = progressModel;
            _progressProvider = progressProvider;
            _userProvider = userProvider;
            _userLogin = userLogin;
            _projectModel = projectModel;
            _projectProvider = projectProvider;
        }

        [HttpPost]
		public IActionResult Create(string uid)
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

			_projectModel = _projectProvider.getDataProjectByUID(uid, myUserId);
			if (_projectModel.IS_CREATE != "TRUE")
			{
				TempData["MyResponseCode"] = "203";
				TempData["MyResponseMessage"] = "You don't have access";
				return RedirectToAction("Index", "Project");
			}

			_progressModel.PROJECT_ID = _projectModel.T_WOMEYE_PROJECT_ID.ToString();
			_progressModel.USR_CRT = myUserId;
			return View(_progressModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateValidation([Bind] ProgressModel form)
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

			#region Validation Mandatory

			if (string.IsNullOrEmpty(form.DESKRIPSI))
            {
                ModelState.AddModelError("DESKRIPSI", "DESKRIPSI is required");
            }
			if (!string.IsNullOrEmpty(form.DESKRIPSI))
			{
				if (form.DESKRIPSI.Length > 100)
				{
					ModelState.AddModelError("NOTES", "Notes just can have 200 character");
				}
			}

			#endregion

			#region Insert

			int noProject = Int32.Parse(form.PROJECT_ID);

			if (ModelState.IsValid)
			{
				var resp = _progressProvider.InsertDataProgress(form);

				if(resp.responseCodeProgress == "200")
                {
					TempData["MyResponseCode"] = resp.responseCodeProgress;
					TempData["MyResponseMessage"] = resp.responseMessageProgress;
					//_progressModel.ListProgress = _progressProvider.getAllProgress(noProject);
					return RedirectToAction("Index","Project");
				}
                else
                {
					_progressModel.responseCodeProgress = resp.responseCodeProgress;
					_progressModel.responseMessageProgress = resp.responseMessageProgress;
					_progressModel.ListProgress = _progressProvider.getAllProgress(noProject);
					return View("Create", _progressModel);
				}
			}
			else
			{
				_progressModel.responseCodeProgress = "400";
				_progressModel.responseMessageProgress = "Validation Error";
				_progressModel.ListProgress = _progressProvider.getAllProgress(noProject);
				return View("Create", _progressModel);
			}
			#endregion
			
		}
	
		[HttpPost]
		public IActionResult Edit(int id)
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

			_progressModel = _progressProvider.getDataProgressById(id.ToString(), myUserId);
			if (_progressModel.IS_EDIT != "TRUE") {
				TempData["MyResponseCode"] = "203";
				TempData["MyResponseMessage"] = "You don't have access";
				return RedirectToAction("Index", "Project");
			}

			_progressModel.USR_UPD = myUserId;

			return View(_progressModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditValidation([Bind] ProgressModel form)
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

			_progressModel = _progressProvider.getDataProgressById(form.T_WOMEYE_PROGRESS_ID.ToString(), myUserId);
			if (_progressModel.IS_EDIT != "TRUE")
			{
				TempData["MyResponseCode"] = "203";
				TempData["MyResponseMessage"] = "You don't have access";
				return RedirectToAction("Index", "Project");
			}

			#region Validasi

			if (string.IsNullOrEmpty(form.DESKRIPSI))
            {
                ModelState.AddModelError("DESKRIPSI", "DESKRIPSI is required");
            }
			if (!string.IsNullOrEmpty(form.DESKRIPSI))
			{
				if (form.DESKRIPSI.Length > 100)
				{
					ModelState.AddModelError("NOTES", "Notes just can have 200 character");
				}
			}
			#endregion

			#region Edit

			int noProject = Int32.Parse(form.PROJECT_ID);
			if (ModelState.IsValid)
			{
				var resp = _progressProvider.UpdateProgress(form);

				if (resp.responseCodeProgress == "200")
				{
					TempData["MyResponseCode"] = resp.responseCodeProgress;
					TempData["MyResponseMessage"] = resp.responseMessageProgress;
					//_progressModel.ListProgress = _progressProvider.getAllProgress(noProject);
					return RedirectToAction("Index", "Project");
				}
				else
				{
					_progressModel.responseCodeProgress = resp.responseCodeProgress;
					_progressModel.responseMessageProgress = resp.responseMessageProgress;
					_progressModel.ListProgress = _progressProvider.getAllProgress(noProject);
					return View("Edit", _progressModel);
				}
				
			}
			else
			{
				_progressModel.responseCodeProgress = "400";
				_progressModel.responseMessageProgress = "Validation Error";
				_progressModel.ListProgress = _progressProvider.getAllProgress(noProject);
				return View("Edit", _progressModel);
			}

			#endregion

			

		}

		[HttpPost]
		public IActionResult Delete(int id)
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

			_progressModel = _progressProvider.getDataProgressById(id.ToString(), myUserId);

			if (_progressModel.IS_DELETE != "TRUE")
			{
				TempData["MyResponseCode"] = "203";
				TempData["MyResponseMessage"] = "You don't have access";
				return RedirectToAction("Index", "Project");
			}

			var resp = _progressProvider.DeleteProgress(id);

			TempData["MyResponseCode"] = resp.responseCodeProgress;
			TempData["MyResponseMessage"] = resp.responseMessageProgress;
			//_progressModel.ListProgress = _progressProvider.getAllProgress(noProject);
			return RedirectToAction("Index", "Project");

			//return Json(new { response = resp });

		}

	}
}
