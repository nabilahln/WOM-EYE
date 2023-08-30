using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.Interfaces.Notes;
using WOM_EYE.Interfaces.Projects;
using WOM_EYE.Interfaces.Users;
using WOM_EYE.Models.Notes;
using WOM_EYE.Models.Projects;
using WOM_EYE.Models.User;

namespace WOM_EYE.Controllers
{
	public class NoteController : Controller
	{
		private string myUserId;
		private string myMUserId;
		private string myUsername;
		private bool access;

		private NoteModel _noteModel;
		private UserModel _userLogin;
		private ProjectModel _projectModel;
		private DetailProjectModel _detailProjectModel;

		private List<NoteModel> _listNote;

		private INoteProvider _noteProvider;
		private IUserProvider _userProvider;
		private IProjectProvider _projectProvider;
		private IDetailProjectProvider _detailProjectProvider;

		public NoteController(INoteProvider noteProvider, NoteModel noteModel, List<NoteModel> listNote, IUserProvider userProvider, UserModel userLogin, IProjectProvider projectProvider, ProjectModel projectModel, DetailProjectModel detailProjectModel, IDetailProjectProvider detailProjectProvider)
		{
			_noteProvider = noteProvider;
			_noteModel = noteModel;
			_listNote = listNote;
			_userProvider = userProvider;
			_userLogin = userLogin;
			_projectProvider = projectProvider;
			_projectModel = projectModel;
			_detailProjectModel = detailProjectModel;
			_detailProjectProvider = detailProjectProvider;
		}

		[HttpPost]
		public IActionResult Index(string uid, string tahap)
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

			_detailProjectModel = _detailProjectProvider.getDetailProjectByUID(uid, myUserId);

			if(_detailProjectModel.IS_ACCESS != "TRUE")
            {
				TempData["MyResponseCode"] = "203";
				TempData["MyResponseMessage"] = "You don't have access";
				return RedirectToAction("Index","Project");
			}

			int projectId;
			ViewBag.Tahap = tahap;

			_noteModel.ListCatatan = _noteProvider.getAllCatatan(uid,myUserId);
			Int32.TryParse(_detailProjectModel.PROJECT_ID, out projectId);
			_projectModel = _projectProvider.getDataProjectById(projectId);


			string programmerString = _projectModel.PROGRAMMER;
			string faString = _projectModel.FUNCTION_ANALYST;
			string[] programmerSplit = String.IsNullOrEmpty(programmerString) ? new string[] { } : programmerString.Split(new char[] { ',' });
			string[] faSplit = String.IsNullOrEmpty(faString) ? new string[] { } : faString.Split(new char[] { ',' });

			ViewBag.detailProjectId = _detailProjectModel.T_WOMEYE_DETAIL_PROJECT_ID;
			ViewBag.detailProjectUID= _detailProjectModel.T_WOMEYE_DETAIL_PROJECT_UID;
			ViewBag.detailProject = _detailProjectModel;
			ViewBag.namaTahap = tahap;
			ViewBag.project = _projectModel;
			ViewBag.listProgrammer = programmerSplit;
			ViewBag.listFa = faSplit;

			return View(_noteModel);
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

			_detailProjectModel = _detailProjectProvider.getDetailProjectByUID(uid, myUserId);

			if (_detailProjectModel.IS_ACCESS != "TRUE")
			{
				TempData["MyResponseCode"] = "203";
				TempData["MyResponseMessage"] = "You don't have access";
				return RedirectToAction("Index", "Project");
			}

			_noteModel.ddlStatusCatatan = _noteProvider.ddlStatusCatatan();
			_noteModel.DETAIL_PROJECT_ID = _detailProjectModel.T_WOMEYE_DETAIL_PROJECT_ID.ToString();
			_noteModel.USR_CRT = myUserId;
			return View(_noteModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult CreateValidation([Bind] NoteModel form)
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

			#region Validation

			if (string.IsNullOrEmpty(form.STATUS_ID))
			{
				ModelState.AddModelError("STATUS_ID", "Status is required");
			}
			if (string.IsNullOrEmpty(form.NOTES))
			{
				ModelState.AddModelError("NOTES", "NOTES is required");
			}
			if (!string.IsNullOrEmpty(form.NOTES))
			{
				if (form.NOTES.Length > 200)
				{
					ModelState.AddModelError("NOTES", "Notes just can have 200 character");
				}
			}
			#endregion

			#region Insert Note

			if (ModelState.IsValid)
			{
				var resp = _noteProvider.InsertCatatan(form);
				//var idProject = HttpContext.Session.GetString("idProject");

				if (resp.responseCode == "200")
				{
					TempData["MyResponseCode"] = resp.responseCode;
					TempData["MyResponseMessage"] = resp.responseMessage;
					//_noteModel.ListCatatan = _noteProvider.getAllCatatan(Int32.Parse(form.DETAIL_PROJECT_ID));
					//_noteModel.ddlStatusCatatan = _noteProvider.ddlStatusCatatan();

					return RedirectToAction("Index", "Project");

				}
				else
				{
					TempData["MyResponseCodeNote"] = resp.responseCode;
					TempData["MyResponseMessageNote"] = resp.responseMessage;
					_noteModel.ListCatatan = _noteProvider.getAllCatatan(Int32.Parse(form.DETAIL_PROJECT_ID));
					_noteModel.ddlStatusCatatan = _noteProvider.ddlStatusCatatan();

					return View("Create",_noteModel);
				}
			}
			else
			{
				_noteModel.responseCode = "400";
				_noteModel.responseMessage = "Validation Error";
				_noteModel.ListCatatan = _noteProvider.getAllCatatan(Int32.Parse(form.DETAIL_PROJECT_ID));
				_noteModel.ddlStatusCatatan = _noteProvider.ddlStatusCatatan();
				return View("Create", _noteModel);
			}

			#endregion


		}

		[HttpPost]
		public IActionResult Edit(int id, string tahap)
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

			ViewBag.namaTahap = tahap;
			_noteModel = _noteProvider.getDataCatatanById(id);
			_noteModel.ddlStatusCatatan = _noteProvider.ddlStatusCatatan();
			_noteModel.USR_UPD = myUserId;

			return View(_noteModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditValidation([Bind] NoteModel form)
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

			#region Validation
			if (string.IsNullOrEmpty(form.STATUS_ID))
			{
				ModelState.AddModelError("STATUS_ID", "STATUS_ID tidak boleh kosong");
			}
			if (string.IsNullOrEmpty(form.NOTES))
			{
				ModelState.AddModelError("NOTES", "NOTES tidak boleh kosong");
			}
			if (!string.IsNullOrEmpty(form.NOTES))
			{
				if (form.NOTES.Length > 200)
				{
					ModelState.AddModelError("NOTES", "Notes just can have 200 character");
				}
			}

			#endregion

			#region Edit

			if (ModelState.IsValid)
			{
				var resp = _noteProvider.UpdateCatatan(form);

				var idProject = HttpContext.Session.GetString("idProject");

				if (resp.responseCode == "200")
				{
					TempData["MyResponseCode"] = resp.responseCode;
					TempData["MyResponseMessage"] = resp.responseMessage;
					//_noteModel.ListCatatan = _noteProvider.getAllCatatan(Int32.Parse(form.DETAIL_PROJECT_ID));
					//_noteModel.ddlStatusCatatan = _noteProvider.ddlStatusCatatan();

					return RedirectToAction("Index", "Project");
				}
				else
				{
					_noteModel.responseCode = resp.responseCode;
					_noteModel.responseMessage = resp.responseMessage;
					_noteModel.ListCatatan = _noteProvider.getAllCatatan(Int32.Parse(form.DETAIL_PROJECT_ID));
					_noteModel.ddlStatusCatatan = _noteProvider.ddlStatusCatatan();
					return View("Edit", _noteModel);
				}

			}
			else
			{
				_noteModel.responseCode = "400";
				_noteModel.responseMessage = "Validation Error";
				_noteModel.ListCatatan = _noteProvider.getAllCatatan(Int32.Parse(form.DETAIL_PROJECT_ID));
				_noteModel.ddlStatusCatatan = _noteProvider.ddlStatusCatatan();
				return View("Edit", _noteModel);
			}

			#endregion


		}

	}
}
