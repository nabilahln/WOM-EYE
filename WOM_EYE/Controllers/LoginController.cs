using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.Interfaces.Login;
using WOM_EYE.Models.Login;
using Microsoft.AspNetCore.Http;
using WOM_EYE.DTOs.Auth;
using WOM_EYE.Interfaces.Users;
using WOM_EYE.Models.User;
using System.Text.RegularExpressions;
using WOM_EYE.DTOs.Login;
using WOM_EYE.Interfaces.Menu;
using WOM_EYE.Models.Menu;
using Newtonsoft.Json;

namespace WOM_EYE.Controllers
{
	public class LoginController : Controller
	{
		private string myUserId;
		private string myMUserId;
		private string myUsername;
		

		private ILogin _loginProvider;
		private LoginModel _loginModel;
		private IUserProvider _userProvider;
		private UserModel _userModel;
		private List<SelectListValidasi> _selectListValidasi;
		private ChangePasswordDTO _changePassModel;
		private ForgetPasswordModel _forgetPassModel;
		private List<SelectListPositionPass> _selectListPosisi;
		private MenuModel _menuModel;
		private List<MenuModel> _listMenu;
		private IMenuProvider _menuProvider;


		public LoginController(ILogin loginProvider, LoginModel loginModel, IUserProvider userProvider, UserModel userModel, List<SelectListValidasi> selectListValidasi, ChangePasswordDTO changePassModel, ForgetPasswordModel forgetPassModel, List<SelectListPositionPass> selectListPosisi, MenuModel menuModel, List<MenuModel> listMenu, IMenuProvider menuProvider)
		{
			_loginProvider = loginProvider;
			_loginModel = loginModel;
			_userProvider = userProvider;
			_userModel = userModel;
			_selectListValidasi = selectListValidasi;
			_changePassModel = changePassModel;
			_forgetPassModel = forgetPassModel;
			_selectListPosisi = selectListPosisi;
			_menuModel = menuModel;
			_listMenu = listMenu;
			_menuProvider = menuProvider;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var user_id = HttpContext.Session.GetString("USER_ID");
			if (user_id == null)
			{
				if (TempData["MyResponseCode"] == null)
				{
					_loginModel.responseCode = "xx";
					_loginModel.responseMessage = "xx";
				}
				else
				{
					_loginModel.responseCode = TempData["MyResponseCode"] as string;
					_loginModel.responseMessage = TempData["MyResponseMessage"] as string;

				}
				return View(_loginModel);
			}
			else
			{
				return RedirectToAction("Index", "Dashboard");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Index([Bind] LoginModel form)
		{
			#region Validation Mandatory

			if (string.IsNullOrEmpty(form.USER_ID))
			{
				ModelState.AddModelError("USER_ID", "User ID is required");
			}
			if (string.IsNullOrEmpty(form.USER_PASS))
			{
				ModelState.AddModelError("USER_PASS", "User Pass is required");
			}
			#endregion
			if (ModelState.IsValid)
			{
				var resp = _loginProvider.getDataUser(form);

				if (resp.responseCode == "404")
				{
					_loginModel.responseCode = resp.responseCode;
					_loginModel.responseMessage = resp.responseMessage;
					return View(_loginModel);
				}
				else if (resp.responseCode == "200")
				{
					_userModel = _userProvider.getDataUser(form.USER_ID);
					_listMenu = _menuProvider.ddlMenu(_userModel.USER_ID);

					HttpContext.Session.SetString("MENU", JsonConvert.SerializeObject(_listMenu));
					HttpContext.Session.SetString("M_WOMEYE_USER_ID", _userModel.M_WOMEYE_USER_ID.ToString());
					HttpContext.Session.SetString("USER_ID", _userModel.USER_ID);
					HttpContext.Session.SetString("USER_NIK_EMP", _userModel.USER_NIK_EMP);
					//HttpContext.Session.SetString("USER_NIK_KTP", _userModel.USER_NIK_KTP);
					HttpContext.Session.SetString("USER_NAME", _userModel.USER_NAME);
					//HttpContext.Session.SetString("USER_POSITION", _userModel.USER_POSITION);
					HttpContext.Session.SetString("LAST_USER_LOGIN", _userModel.LAST_USER_LOGIN);
					HttpContext.Session.SetString("LAST_USER_UPDATE", _userModel.LAST_USER_UPDATE);
					TempData["MyResponseCode"] = resp.responseCode;
					TempData["MyResponseMessage"] = resp.responseMessage;
					return RedirectToAction("Index", "Dashboard");
				}
				else if (resp.responseCode == "500")
				{
					_loginModel.responseCode = resp.responseCode;
					_loginModel.responseMessage = resp.responseMessage;
					return View(_loginModel);
				}
				else if (resp.responseCode == "501")
				{
					_changePassModel.userId = form.USER_ID;
					_changePassModel.responseMessage = resp.responseMessage;
					_changePassModel.responseCode = resp.responseCode;
					return View("ChangePassLogin", _changePassModel);
				}
				else if (resp.responseCode == "502")
				{
					_forgetPassModel.responseMessage = resp.responseMessage;
					_forgetPassModel.responseCode = resp.responseCode;
					_forgetPassModel.ddlPositionPass = _loginProvider.ddlPositionPass();
					return View("ForgetPassword", _forgetPassModel);
				}

			}
			_loginModel.responseCode = "xx";
			_loginModel.responseMessage = "xx";
			return View(_loginModel);
		}

		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			TempData["MyResponseCode"] = "200";
			TempData["MyResponseMessage"] = "See you Later!";
			return RedirectToAction("Index", "Login");
		}

		[HttpGet]
		public IActionResult ChangePassword()
		{
			#region CheckSession

			myUserId = HttpContext.Session.GetString("USER_ID");
			myMUserId = HttpContext.Session.GetString("M_WOMEYE_USER_ID");
			myUsername = HttpContext.Session.GetString("USER_NAME");
			_userModel = _userProvider.getDataUser(myUserId);
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
				ViewBag.UserLogin = _userModel;
			}

			#endregion

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ChangePassword([Bind] ChangePasswordDTO form)
		{
			#region CheckSession

			myUserId = HttpContext.Session.GetString("USER_ID");
			myMUserId = HttpContext.Session.GetString("M_WOMEYE_USER_ID");
			myUsername = HttpContext.Session.GetString("USER_NAME");
			_userModel = _userProvider.getDataUser(myUserId);
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
				ViewBag.UserLogin = _userModel;
			}

			#endregion

			#region Validation
			if (string.IsNullOrEmpty(form.passOld))
			{
				ModelState.AddModelError("passOld", "Password Lama is required");
			}
			if (string.IsNullOrEmpty(form.passNew))
			{
				ModelState.AddModelError("passNew", "Password Baru is required");
			}
			if (string.IsNullOrEmpty(form.passConfirm))
			{
				ModelState.AddModelError("passConfirm", "Password Confirm is required");

			}
			if (!string.IsNullOrEmpty(form.passConfirm))
			{
				if (form.passNew != form.passConfirm)
				{
					ModelState.AddModelError("passConfirm", "Password Comfirm must same with password Baru");

				}
			}
			if (!string.IsNullOrEmpty(form.passNew))
			{
				if (form.passNew == form.passOld)
				{
					ModelState.AddModelError("passNew", "Password Baru tidak boleh sama dengan Password Lama");

				}
			}
			if (!string.IsNullOrEmpty(form.passNew))
			{

				List<string> errorMessages = new List<string>();

				_selectListValidasi = _loginProvider.ddlValidasi();

				Regex jenisValidasi = null;

				foreach (var validasi in _selectListValidasi)
				{
					jenisValidasi = new Regex(@"" + validasi.ISI_VALIDASI);
					if (!jenisValidasi.IsMatch(form.passNew))
					{
						errorMessages.Add("" + validasi.PESAN_ERROR);
					}
				}
				if (errorMessages.Any())
				{
					String allerror = "";
					// Password validation failed. Return error messages.
					foreach (var errorMessage in errorMessages)
					{
						allerror = string.Join(", ", errorMessages);
					}

					ModelState.AddModelError("passNew", allerror);

					return View();
				}



			}
			#endregion

			if (ModelState.IsValid)
			{

				if (form.passNew.Equals(form.passConfirm))
				{
					var resp = _loginProvider.changePassword(form);
					if (resp.responseCode == "200")
					{
						TempData["MyResponseCode"] = resp.responseCode;
						TempData["MyResponseMessage"] = resp.responseMessage;
						return RedirectToAction("Index", "Dashboard");

					}
					else if (resp.responseCode == "500")
					{
						ViewBag.errorMessage = resp.errorMessage;
						return View();

					}
				}
			}
			return View();
		}

		[HttpGet]
		public IActionResult ChangePassLogin()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ChangePassLogin([Bind] ChangePasswordDTO form)
		{

			#region Validation
			if (string.IsNullOrEmpty(form.passOld))
			{
				ModelState.AddModelError("passOld", "Password Lama is required");
			}
			if (string.IsNullOrEmpty(form.passNew))
			{
				ModelState.AddModelError("passNew", "Password Baru is required");
			}
			if (string.IsNullOrEmpty(form.passConfirm))
			{
				ModelState.AddModelError("passConfirm", "Password Confirm is required");

			}
			if (!string.IsNullOrEmpty(form.passConfirm))
			{
				if (form.passNew != form.passConfirm)
				{
					ModelState.AddModelError("passConfirm", "Password Comfirm must same with password Baru");

				}
			}
			if (!string.IsNullOrEmpty(form.passNew))
			{
				if (form.passNew == form.passOld)
				{
					ModelState.AddModelError("passNew", "Password Baru tidak boleh sama dengan Password Lama");

				}
			}
			if (!string.IsNullOrEmpty(form.passNew))
			{

				List<string> errorMessages = new List<string>();

				_selectListValidasi = _loginProvider.ddlValidasi();

				Regex jenisValidasi = null;

				foreach (var validasi in _selectListValidasi)
				{
					jenisValidasi = new Regex(@"" + validasi.ISI_VALIDASI);
					if (!jenisValidasi.IsMatch(form.passNew))
					{
						errorMessages.Add("" + validasi.PESAN_ERROR);
					}


				}

				if (errorMessages.Any())
				{
					String allerror = "";
					// Password validation failed. Return error messages.
					foreach (var errorMessage in errorMessages)
					{
						allerror = string.Join(", ", errorMessages);
					}

					ModelState.AddModelError("passNew", allerror);

					return View();
				}



			}
			#endregion

			if (ModelState.IsValid)
			{

				if (form.passNew.Equals(form.passConfirm))
				{
					var resp = _loginProvider.changePassLogin(form);
					if (resp.responseCode == "200")
					{
						_loginModel.responseCode = resp.responseCode;
						_loginModel.responseMessage = resp.responseMessage;
						return View("Index", _loginModel);

					}
					else if (resp.responseCode == "500")
					{
						_loginModel.responseCode = resp.responseCode;
						_loginModel.responseMessage = resp.responseMessage;
						return View();
					}
				}
			}
			return View(_changePassModel);
		}

		[HttpGet]
		public IActionResult ForgetPassword()
		{
			_forgetPassModel.ddlPositionPass = _loginProvider.ddlPositionPass();
			return View(_forgetPassModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult ForgetPassword([Bind] ForgetPasswordModel form)
		{
			#region Validation
			if (string.IsNullOrEmpty(form.USER_ID))
			{
				ModelState.AddModelError("USER_ID", "USER ID is required");
			}
			if (string.IsNullOrEmpty(form.NIK_EMP))
			{
				ModelState.AddModelError("NIK_EMP", "NIK EMPLOYEE is required");
			}
			if (string.IsNullOrEmpty(form.CODE_UNIQUE))
			{
				ModelState.AddModelError("CODE_UNIQUE", "CODE UNIQUE is required");
			}
			if (!string.IsNullOrEmpty(form.USER_PASS))
			{

				List<string> errorMessages = new List<string>();

				_selectListValidasi = _loginProvider.ddlValidasi();

				Regex jenisValidasi = null;

				foreach (var validasi in _selectListValidasi)
				{
					jenisValidasi = new Regex(@"" + validasi.ISI_VALIDASI);
					if (!jenisValidasi.IsMatch(form.USER_PASS))
					{
						errorMessages.Add("" + validasi.PESAN_ERROR);
					}
				}
				if (errorMessages.Any())
				{
					String allerror = "";
					// Password validation failed. Return error messages.
					foreach (var errorMessage in errorMessages)
					{
						allerror = string.Join(", ", errorMessages);
					}

					ModelState.AddModelError("USER_PASS", allerror);

					//return View();
				}



			}
			if (string.IsNullOrEmpty(form.ConfirmPass))
			{
				ModelState.AddModelError("ConfirmPass", "Password Confirm is required");

			}
			if (!string.IsNullOrEmpty(form.ConfirmPass))
			{
				if (form.USER_PASS != form.ConfirmPass)
				{
					ModelState.AddModelError("ConfirmPass", "Confirm Password must the same as New Password");

				}
			}
			if (!string.IsNullOrEmpty(form.USER_PASS))
			{

				List<string> errorMessages = new List<string>();

				_selectListValidasi = _loginProvider.ddlValidasi();

				Regex jenisValidasi = null;

				foreach (var validasi in _selectListValidasi)
				{
					jenisValidasi = new Regex(@"" + validasi.ISI_VALIDASI);
					if (!jenisValidasi.IsMatch(form.USER_PASS))
					{
						errorMessages.Add("" + validasi.PESAN_ERROR);
					}

				}
			}
			#endregion

			if (ModelState.IsValid)
			{
				var resp = _loginProvider.ForgetPassword(form);
				if (resp.responseCode == "200")
				{
					_forgetPassModel.ddlPositionPass = _loginProvider.ddlPositionPass();
					TempData["MyResponseCode"] = resp.responseCode;
					TempData["MyResponseMessage"] = resp.responseMessage;
					return RedirectToAction("Index", "Login");
				}
				else if (resp.responseCode == "500")
				{
					_forgetPassModel.ddlPositionPass = _loginProvider.ddlPositionPass();
					_forgetPassModel.responseCode = resp.responseCode;
					_forgetPassModel.responseMessage = resp.responseMessage;
					return View(_forgetPassModel);
				}
			}
			_forgetPassModel.ddlPositionPass = _loginProvider.ddlPositionPass();
			_forgetPassModel.responseCode = "";
			_forgetPassModel.responseMessage = "";
			return View(_forgetPassModel);

		}

	}
}
