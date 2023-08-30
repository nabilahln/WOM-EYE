using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.Interfaces.AddUser;
using WOM_EYE.Interfaces.Users;
using WOM_EYE.Models.AddUser;
using WOM_EYE.Models.User;

namespace WOM_EYE.Controllers
{
    public class AddUserController : Controller
    {

        private string myUserId;
        private string myMUserId;
        private string myUsername;

        private readonly IDbConnection _dbConnection;
        private AddUserModel _AddUserModel;
        private IAddUserProvider _AddUserProvider;
        private IUserProvider _userProvider;
        private UserModel _userLogin;

        public AddUserController(IDbConnection dbConnection,
                                AddUserModel AddUserModel,
                                IAddUserProvider AddUserProvider,
                                IUserProvider userProvider,
                                UserModel userLogin)
        {
            this._dbConnection = dbConnection;
            this._AddUserModel = AddUserModel;
            this._AddUserProvider = AddUserProvider;
            this._userProvider = userProvider;
            this._userLogin = userLogin;

        }
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
                if (_userLogin.USER_POSITION == "IT Division Head" || _userLogin.USER_POSITION == "IT Solution Deputy Division Head")
                {
                    var theMenu = JsonConvert.DeserializeObject(HttpContext.Session.GetString("MENU"));
                    ViewBag.Menu = theMenu;
                    ViewBag.UserId = myUserId;
                    ViewBag.MUserId = myMUserId;
                    ViewBag.MUserName = myUsername;
                    ViewBag.UserLogin = _userLogin;
                }
                else
                {
                    TempData["MyResponseCode"] = "404";
                    TempData["MyResponseMessage"] = "Not Found";
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            #endregion


            _AddUserModel.listAddUser = _AddUserProvider.getAllAddUser();
            _AddUserModel.responseCodeAddUser = TempData["MyResponseCode"] as string;
            _AddUserModel.responseMessageAddUser = TempData["MyResponseMessage"] as string;

            return View(_AddUserModel);
        }

        [HttpGet]
        public IActionResult Create()
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
                if (_userLogin.USER_POSITION == "IT Division Head" || _userLogin.USER_POSITION == "IT Solution Deputy Division Head")
                {
                    var theMenu = JsonConvert.DeserializeObject(HttpContext.Session.GetString("MENU"));
                    ViewBag.Menu = theMenu;
                    ViewBag.UserId = myUserId;
                    ViewBag.MUserId = myMUserId;
                    ViewBag.MUserName = myUsername;
                    ViewBag.UserLogin = _userLogin;
                }
                else
                {
                    TempData["MyResponseCode"] = "404";
                    TempData["MyResponseMessage"] = "Not Found";
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            #endregion

            // _AddUserModel.ddlUser = _AddUserProvider.ddlUser();
            _AddUserModel.ddlPosition = _AddUserProvider.ddlPosition();
            return View(_AddUserModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] AddUserModel form)
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
                if (_userLogin.USER_POSITION == "IT Division Head" || _userLogin.USER_POSITION == "IT Solution Deputy Division Head")
                {
                    var theMenu = JsonConvert.DeserializeObject(HttpContext.Session.GetString("MENU"));
                    ViewBag.Menu = theMenu;
                    ViewBag.UserId = myUserId;
                    ViewBag.MUserId = myMUserId;
                    ViewBag.MUserName = myUsername;
                    ViewBag.UserLogin = _userLogin;
                }
                else
                {
                    TempData["MyResponseCode"] = "404";
                    TempData["MyResponseMessage"] = "Not Found";
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            #endregion

            #region Validation Mandatory

            if (string.IsNullOrEmpty(form.USER_ID))
            {
                ModelState.AddModelError("USER_ID", "User ID is required");
            }
            if (!string.IsNullOrEmpty(form.USER_ID))
            {
                if ((form.USER_ID).Length > 12)
                {
                    ModelState.AddModelError("USER_ID", "User ID max have 12 character");
                }
            }
            if (string.IsNullOrEmpty(form.USER_NAME))
            {
                ModelState.AddModelError("USER_NAME", "User Name is required");
            }
            if (string.IsNullOrEmpty(form.USER_NIK_EMP))
            {
                ModelState.AddModelError("USER_NIK_EMP", "Nik user is required");
            }
            if (string.IsNullOrEmpty(form.GENDER))
            {
                ModelState.AddModelError("GENDER", "Gender is required");
            }
            if (string.IsNullOrEmpty(form.USER_POSITION))
            {
                ModelState.AddModelError("USER_POSITION", "User Position is required");
            }
            if (string.IsNullOrEmpty(form.USER_PASS))
            {
                ModelState.AddModelError("USER_PASS", "Password is required");
            }
            if (string.IsNullOrEmpty(form.DIVISION))
            {
                ModelState.AddModelError("DIVISION", "Departemen is required");
            }
            
            #endregion

            #region Insert

            //int noProject = Int32.Parse(form.PROJECT_ID);

            if (ModelState.IsValid)
            {
                var resp = _AddUserProvider.InsertAddUser(form);

                if (resp.responseCodeAddUser == "200")
                {
                    TempData["MyResponseCode"] = resp.responseCodeAddUser;
                    TempData["MyResponseMessage"] = resp.responseMessageAddUser;
                    _AddUserModel.listAddUser = _AddUserProvider.getAllAddUser();
                    _AddUserModel.ddlPosition = _AddUserProvider.ddlPosition();
                    return RedirectToAction("Index");
                }
                else
                {
                    _AddUserModel.responseCodeAddUser = resp.responseCodeAddUser;
                    _AddUserModel.responseMessageAddUser = resp.responseMessageAddUser;
                    _AddUserModel.listAddUser = _AddUserProvider.getAllAddUser();
                    _AddUserModel.ddlPosition = _AddUserProvider.ddlPosition();
                    return View(_AddUserModel);
                }
            }
            else
            {
                _AddUserModel.responseCodeAddUser = "400";
                _AddUserModel.responseMessageAddUser = "Validation Error";
                _AddUserModel.listAddUser = _AddUserProvider.getAllAddUser();
                _AddUserModel.ddlPosition = _AddUserProvider.ddlPosition();
                return View(_AddUserModel);
            }
            #endregion

        }

        [HttpGet]
        public IActionResult Update(String id)
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
                if (_userLogin.USER_POSITION == "IT Division Head" || _userLogin.USER_POSITION == "IT Solution Deputy Division Head")
                {
                    var theMenu = JsonConvert.DeserializeObject(HttpContext.Session.GetString("MENU"));
                    ViewBag.Menu = theMenu;
                    ViewBag.UserId = myUserId;
                    ViewBag.MUserId = myMUserId;
                    ViewBag.MUserName = myUsername;
                    ViewBag.UserLogin = _userLogin;
                }
                else
                {
                    TempData["MyResponseCode"] = "404";
                    TempData["MyResponseMessage"] = "Not Found";
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            #endregion

            int UserId = Convert.ToInt32(id);
            _AddUserModel = _AddUserProvider.getDataUserById(UserId);
            _AddUserModel.ddlPosition = _AddUserProvider.ddlPosition();
            return View(_AddUserModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update([Bind] AddUserModel form)
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
                if (_userLogin.USER_POSITION == "IT Division Head" || _userLogin.USER_POSITION == "IT Solution Deputy Division Head")
                {
                    var theMenu = JsonConvert.DeserializeObject(HttpContext.Session.GetString("MENU"));
                    ViewBag.Menu = theMenu;
                    ViewBag.UserId = myUserId;
                    ViewBag.MUserId = myMUserId;
                    ViewBag.MUserName = myUsername;
                    ViewBag.UserLogin = _userLogin;
                }
                else
                {
                    TempData["MyResponseCode"] = "404";
                    TempData["MyResponseMessage"] = "Not Found";
                    return RedirectToAction("Index", "Dashboard");
                }
            }

            #endregion


            #region Validation Mandatory

            if (string.IsNullOrEmpty(form.USER_ID))
            {
                ModelState.AddModelError("USER_ID", "User ID is required");
            }
            if (!string.IsNullOrEmpty(form.USER_ID))
            {
                if ((form.USER_ID).Length > 12)
                {
                    ModelState.AddModelError("USER_ID", "User ID max have 12 character");
                }
            }
            if (string.IsNullOrEmpty(form.USER_NAME))
            {
                ModelState.AddModelError("USER_NAME", "User Name is required");
            }
            if (string.IsNullOrEmpty(form.USER_NIK_EMP))
            {
                ModelState.AddModelError("USER_NIK_EMP", "Nik user is required");
            }
            if (string.IsNullOrEmpty(form.GENDER))
            {
                ModelState.AddModelError("GENDER", "Gender is required");
            }
            if (string.IsNullOrEmpty(form.USER_POSITION))
            {
                ModelState.AddModelError("USER_POSITION", "User Position is required");
            }
            if (string.IsNullOrEmpty(form.DIVISION))
            {
                ModelState.AddModelError("DIVISION", "Departemen is required");
            }
            if (!string.IsNullOrEmpty(form.PHONE))
            {
                if ((form.PHONE).Length < 10)
                {
                    ModelState.AddModelError("PHONE", "Phone Number minimal have 10 character");
                }
                if ((form.PHONE).Length > 15)
                {
                    ModelState.AddModelError("PHONE", "Phone Number maksimal have 15 character");
                }
            }

            #endregion

            #region Update

            //int noProject = Int32.Parse(form.PROJECT_ID);

            if (ModelState.IsValid)
            {
                var resp = _AddUserProvider.UpdateAddUser(form);

                if (resp.responseCodeAddUser == "200")
                {
                    TempData["MyResponseCode"] = resp.responseCodeAddUser;
                    TempData["MyResponseMessage"] = resp.responseMessageAddUser;
                    _AddUserModel.listAddUser = _AddUserProvider.getAllAddUser();
                    _AddUserModel.ddlPosition = _AddUserProvider.ddlPosition();
                    return RedirectToAction("Index");
                }
                else
                {
                    _AddUserModel.responseCodeAddUser = resp.responseCodeAddUser;
                    _AddUserModel.responseMessageAddUser = resp.responseMessageAddUser;
                    _AddUserModel.listAddUser = _AddUserProvider.getAllAddUser();
                    _AddUserModel.ddlPosition = _AddUserProvider.ddlPosition();
                    return View("Index", _AddUserModel);
                }
            }
            else
            {
                _AddUserModel.responseCodeAddUser = "400";
                _AddUserModel.responseMessageAddUser = "Validation Error";
                //_AddUserModel.listAddUser = _AddUserProvider.getAllAddUser();
                _AddUserModel.ddlPosition = _AddUserProvider.ddlPosition();
                return View(_AddUserModel);
            }
            #endregion

        }
    }
}
