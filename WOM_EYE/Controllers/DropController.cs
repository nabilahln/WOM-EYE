using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.Interfaces.Drop;
using WOM_EYE.Interfaces.Projects;
using WOM_EYE.Interfaces.Users;
using WOM_EYE.Models.Drop;
using WOM_EYE.Models.Projects;
using WOM_EYE.Models.User;

namespace WOM_EYE.Controllers
{
    public class DropController : Controller
    {
        private string myUserId;
        private string myMUserId;
        private string myUsername;
        private bool access;

        private IDropProvider _dropProvider;
        private DropModel _dropModel;
        private List<DropModel> _listDrop;

        private IUserProvider _userProvider;
        private UserModel _userLogin;

        private IProjectProvider _projectProvider;
        private ProjectModel _projectModel;

        public DropController(IDropProvider dropProvider, DropModel dropModel, List<DropModel> listDrop, IUserProvider userProvider, UserModel userLogin, IProjectProvider projectProvider, ProjectModel projectModel)
        {
            _dropProvider = dropProvider;
            _dropModel = dropModel;
            _listDrop = listDrop;
            _userProvider = userProvider;
            _userLogin = userLogin;
            _projectProvider = projectProvider;
            _projectModel = projectModel;
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
                var theMenu = JsonConvert.DeserializeObject(HttpContext.Session.GetString("MENU"));
                ViewBag.Menu = theMenu;
                ViewBag.UserId = myUserId;
                ViewBag.MUserId = myMUserId;
                ViewBag.MUserName = myUsername;
                ViewBag.UserLogin = _userLogin;
            }

            #endregion

            _dropModel.ListDrop = _dropProvider.getAllDrop(myUserId);

            return View(_dropModel);
        }

        [HttpPost]
        public IActionResult Undrop(string dropId)
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
                ViewBag.UserId = myUserId;
                ViewBag.MUserId = myMUserId;
                ViewBag.MUserName = myUsername;
                ViewBag.UserLogin = _userLogin;
            }

            #endregion

            #region UnDrop

            if (ModelState.IsValid)
            {
                var resp = _dropProvider.UnDropProject(dropId);

                if (resp.responseCodeDrop == "200")
                {
                    TempData["MyResponseCode"] = resp.responseCodeDrop;
                    TempData["MyResponseMessage"] = resp.responseMessageDrop;
                    //_dropModel.responseCodeDrop = resp.responseCodeDrop;
                    //_dropModel.responseMessageDrop = resp.responseMessageDrop;
                    _dropModel.ListDrop = _dropProvider.getAllDrop();
                    //return RedirectToAction("Index", "Drop", _dropModel);
                    return RedirectToAction("Index", "Drop");
                }
                else
                {
                    TempData["MyResponseCode"] = resp.responseCodeDrop;
                    TempData["MyResponseMessage"] = resp.responseMessageDrop;
                    _dropModel.responseCodeDrop = resp.responseCodeDrop;
                    _dropModel.responseMessageDrop = resp.responseMessageDrop;
                    _dropModel.errorMessageDrop = resp.errorMessageDrop;
                    return View("Drop", _dropModel);
                }
                //_dropModel.responseCodeDrop = resp.responseCodeDrop;
                //_dropModel.responseMessageDrop = resp.responseMessageDrop;
                //_dropModel.ListDrop = _dropProvider.getAllDrop();

            }
            else
            {
                _dropModel.responseCodeDrop = "400";
                _dropModel.responseMessageDrop = "Validation Error";
                _dropModel.ListDrop = _dropProvider.getAllDrop();

            }

            #endregion

            return View("Index", _dropModel);
        }


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
                return RedirectToAction("Index", "Drop");
            }

            _dropModel.PROJECT_ID = _projectModel.T_WOMEYE_PROJECT_ID.ToString();

            return View(_dropModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] DropModel form)
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
            if (string.IsNullOrEmpty(form.ALASAN))
            {
                ModelState.AddModelError("ALASAN", "Reason is required");
            }
            #endregion

            #region InsertDrop

            if (ModelState.IsValid)
            {
                var resp = _dropProvider.DropProject(form);

                if (resp.responseCodeDrop == "200")
                {
                    TempData["MyResponseCode"] = resp.responseCodeDrop;
                    TempData["MyResponseMessage"] = resp.responseMessageDrop;
                    //_dropModel.responseCodeDrop = resp.responseCodeDrop;
                    //_dropModel.responseMessageDrop = resp.responseMessageDrop;
                    _dropModel.ListDrop = _dropProvider.getAllDrop();
                    //return RedirectToAction("Index", "Drop", _dropModel);
                    return RedirectToAction("Index", "Drop");
                }
                else
                {
                    TempData["MyResponseCode"] = resp.responseCodeDrop;
                    TempData["MyResponseMessage"] = resp.responseMessageDrop;
                    _dropModel.responseCodeDrop = resp.responseCodeDrop;
                    _dropModel.responseMessageDrop = resp.responseMessageDrop;
                    _dropModel.errorMessageDrop = resp.errorMessageDrop;
                    _dropModel.PROJECT_ID = form.PROJECT_ID;
                    return View("Create", _dropModel);
                }
            }
            else
            {
                _dropModel.responseCodeDrop = "400";
                _dropModel.responseMessageDrop = "Validation Error";
                _dropModel.ListDrop = _dropProvider.getAllDrop();
                return View("Create", _dropModel);
            }

            #endregion



        }

    }
}
