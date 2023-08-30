using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.Interfaces.Projects;
using WOM_EYE.Interfaces.Users;
using WOM_EYE.Models.Projects;
using WOM_EYE.Models.User;

namespace WOM_EYE.Controllers
{
    public class DetailProjectController : Controller
    {
        private string myUserId;
        private string myMUserId;
        private string myUsername;
        private bool access;

        private IDetailProjectProvider _detailProjectProvider;
        private IProjectProvider _projectProvider;
        private DetailProjectModel _detailProjectModel;
        private IUserProvider _userProvider;
        private UserModel _userLogin;

        public DetailProjectController(IDetailProjectProvider detailProjectProvider, IProjectProvider projectProvider, DetailProjectModel detailProjectModel, IUserProvider userProvider)
        {
            this._detailProjectProvider = detailProjectProvider;
            this._projectProvider = projectProvider;
            this._detailProjectModel = detailProjectModel;
            this._userProvider = userProvider;
        }

        [HttpPost]
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
                var theMenu = JsonConvert.DeserializeObject(HttpContext.Session.GetString("MENU"));
                ViewBag.Menu = theMenu;
                ViewBag.UserId = myUserId;
                ViewBag.MUserId = myMUserId;
                ViewBag.MUserName = myUsername;
                ViewBag.UserLogin = _userLogin;
            }

            #endregion

            ViewBag.projectUID = Request.Form["projectUID"];
            _detailProjectModel.USR_CRT = myUserId;
            _detailProjectModel.T_WOMEYE_PROJECT_UID = Request.Form["projectUID"];
            _detailProjectModel.ddlStatus = _projectProvider.ddlStatus();
            _detailProjectModel.ddlTahap = _detailProjectProvider.ddlTahap();
            _detailProjectModel.responseCodeProject = TempData["MyResponseCode"] as string;
            _detailProjectModel.responseMessageProject = TempData["MyResponseMessage"] as string;
            return View(_detailProjectModel);
        }

        [HttpPost]
        public IActionResult CreateValidation([Bind] DetailProjectModel form)
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
            if (string.IsNullOrEmpty(form.TAHAP))
            {
                ModelState.AddModelError("TAHAP", "Phase is required");
            }
            if (string.IsNullOrEmpty(form.DOKUMEN))
            {
                ModelState.AddModelError("DOKUMEN", "G-Suite Link is required");
            }
            if (string.IsNullOrEmpty(form.START_DT))
            {
                ModelState.AddModelError("START_DT", "Start Date is required");
            }
            if (string.IsNullOrEmpty(form.END_DT))
            {
                ModelState.AddModelError("END_DT", "End Date is required");
            }
            if (string.IsNullOrEmpty(form.STATUS_PROJECT))
            {
                ModelState.AddModelError("STATUS_PROJECT", "Status Project is required");
            }
            if (string.IsNullOrEmpty(form.KETERANGAN))
            {
                ModelState.AddModelError("KETERANGAN", "Info is required");
            }
            if (!string.IsNullOrEmpty(form.START_DT))
            {
                if (DateTime.Parse(form.START_DT) > DateTime.Parse(form.END_DT))
                {
                    ModelState.AddModelError("START_DT", "Start date must be earlier than end date");
                }
            }
            if (!string.IsNullOrEmpty(form.END_DT))
            {
                if (DateTime.Parse(form.START_DT) == DateTime.Parse(form.END_DT))
                {
                    ModelState.AddModelError("END_DT", "End date can't be the same as start date");
                }
            }
            if (!string.IsNullOrEmpty(form.DOKUMEN))
            {
                if (form.DOKUMEN.Length > 100)
                {
                    ModelState.AddModelError("DOKUMEN", "G-Suite Link just can have 100 character");
                }
            }


            #endregion

            if (ModelState.IsValid)
            {
                var resp = _detailProjectProvider.AddDetailProject(form);

                if (resp.responseCodeProject == "200")
                {
                    TempData["MyResponseCode"] = resp.responseCodeProject;
                    TempData["MyResponseMessage"] = resp.responseMessageProject;
                    _detailProjectModel.ddlStatus = _projectProvider.ddlStatus();
                    _detailProjectModel.ddlUser = _projectProvider.ddlUser();
                    return RedirectToAction("Index", "Project");
                }
                else
                {
                    TempData["MyResponseCode"] = resp.responseCodeProject;
                    TempData["MyResponseMessage"] = resp.responseMessageProject;
                    _detailProjectModel.ddlStatus = _projectProvider.ddlStatus();
                    _detailProjectModel.ddlUser = _projectProvider.ddlUser();
                    return View(_detailProjectModel);

                }
            }
            else
            {
                _detailProjectModel.responseCodeProject = "400";
                _detailProjectModel.responseMessageProject = "Validation Error"; ;
                _detailProjectModel.T_WOMEYE_PROJECT_UID = Request.Form["projectUID"];
                _detailProjectModel.ddlStatus = _projectProvider.ddlStatus();
                _detailProjectModel.ddlTahap = _detailProjectProvider.ddlTahap();
                return View("Create", _detailProjectModel);
            }
        }

        //[HttpGet]
        //public IActionResult Edit(string id)
        //{
        //	#region CheckSession
        //	myUserId = HttpContext.Session.GetString("USER_ID");
        //	myMUserId = HttpContext.Session.GetString("M_WOMEYE_USER_ID");

        //	if (_userProvider.checkUserSession(myUserId, myMUserId))
        //	{
        //		ViewBag.UserId = myUserId;
        //		ViewBag.MUserId = myMUserId;
        //	}
        //	else
        //	{
        //		return RedirectToAction("Index", "Login");
        //	}
        //	#endregion

        //	int detailProjectId = Convert.ToInt32(id);
        //	_detailProjectModel = _detailProjectProvider.getDetailProjectById(detailProjectId);
        //	_detailProjectModel.ddlStatus = _projectProvider.ddlStatus();
        //	_detailProjectModel.responseCodeProject = TempData["MyResponseCode"] as string;
        //	_detailProjectModel.responseMessageProject = TempData["MyResponseMessage"] as string;
        //	return View(_detailProjectModel);
        //}

        [HttpPost]
        public IActionResult Edit(string uid)
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

            if (_detailProjectModel == null)
            {
                TempData["MyResponseCode"] = "404";
                TempData["MyResponseMessage"] = "Project Not Found";
                return RedirectToAction("Index", "Project");
            }

            _detailProjectModel.T_WOMEYE_DETAIL_PROJECT_UID = Encrypt(_detailProjectModel.T_WOMEYE_DETAIL_PROJECT_UID);
            _detailProjectModel.ddlStatus = _projectProvider.ddlStatus();
            _detailProjectModel.responseCodeProject = TempData["MyResponseCode"] as string;
            _detailProjectModel.responseMessageProject = TempData["MyResponseMessage"] as string;
            return View(_detailProjectModel);
        }

        [HttpPost]
        public IActionResult EditValidation([Bind] DetailProjectModel form)
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
            if (string.IsNullOrEmpty(form.TAHAP))
            {
                ModelState.AddModelError("TAHAP", "Phase is required");
            }
            if (string.IsNullOrEmpty(form.DOKUMEN))
            {
                ModelState.AddModelError("DOKUMEN", "G-Suite is required");
            }
            if (string.IsNullOrEmpty(form.START_DT))
            {
                ModelState.AddModelError("START_DT", "Start Date is required");
            }
            if (string.IsNullOrEmpty(form.END_DT))
            {
                ModelState.AddModelError("END_DT", "End Date is required");
            }
            if (string.IsNullOrEmpty(form.STATUS_PROJECT))
            {
                ModelState.AddModelError("STATUS_PROJECT", "Status Project is required");
            }
            else
            {
                if (form.STATUS_PROJECT == "4")
                {
                    if (string.IsNullOrEmpty(form.CATATAN)) ModelState.AddModelError("CATATAN", "Note is required");
                }
            }
            if (string.IsNullOrEmpty(form.KETERANGAN))
            {
                ModelState.AddModelError("KETERANGAN", "Info is required");
            }
            if (!string.IsNullOrEmpty(form.START_DT))
            {
                if (DateTime.Parse(form.START_DT) > DateTime.Parse(form.END_DT))
                {
                    ModelState.AddModelError("START_DT", "Start date must be earlier than end date");
                }
            }
            if (!string.IsNullOrEmpty(form.END_DT))
            {
                if (DateTime.Parse(form.START_DT) == DateTime.Parse(form.END_DT))
                {
                    ModelState.AddModelError("END_DT", "End date can't be the same as start date");
                }
            }
            if (!string.IsNullOrEmpty(form.REALIZATION_START_DT))
            {
                if (DateTime.Parse(form.REALIZATION_START_DT) > DateTime.Parse(form.REALIZATION_END_DT))
                {
                    ModelState.AddModelError("REALIZATION_START_DT", "Start date must be earlier than end date");
                }
            }
            if (!string.IsNullOrEmpty(form.REALIZATION_END_DT))
            {
                if (DateTime.Parse(form.REALIZATION_START_DT) == DateTime.Parse(form.REALIZATION_END_DT))
                {
                    ModelState.AddModelError("REALIZATION_END_DT", "End date can't be the same as start date");
                }
            }
            if (!string.IsNullOrEmpty(form.DOKUMEN))
            {
                if (form.DOKUMEN.Length > 255)
                {
                    ModelState.AddModelError("DOKUMEN", "G-Suite Link just can have 255 character");
                }
            }

            if (!string.IsNullOrEmpty(form.STATUS) && (form.STATUS == "4" || form.STATUS == "5"))
            {
                if (string.IsNullOrEmpty(form.CATATAN))
                {
                    ModelState.AddModelError("CATATAN", "Note is required");
                }
            }

            string checkHold = "Hold " + form.TAHAP;
            _detailProjectModel.ddlStatus = _projectProvider.ddlStatus();
            string statusName = "";
            foreach (var status in _detailProjectModel.ddlStatus)
            {
                if (status.key.Equals(form.STATUS_PROJECT))
                {
                    statusName = status.value;
                }
            }

            if (statusName == checkHold)
            {
                if (string.IsNullOrEmpty(form.CATATAN))
                {
                    ModelState.AddModelError("CATATAN", "Note is required");
                }

                if (!string.IsNullOrEmpty(form.CATATAN))
                {
                    if (form.CATATAN.Length > 50)
                    {
                        ModelState.AddModelError("CATATAN", "Note Max have 50 character, Use the notes feature for more detail");
                    }
                }
            }



            #endregion

            #region Edit

            if (ModelState.IsValid)
            {
                form.T_WOMEYE_DETAIL_PROJECT_UID = Decrypt(form.T_WOMEYE_DETAIL_PROJECT_UID);
                var resp = _detailProjectProvider.UpdateDetailProject(form);

                if (resp.responseCodeProject == "200")
                {
                    TempData["MyResponseCode"] = resp.responseCodeProject;
                    TempData["MyResponseMessage"] = resp.responseMessageProject;
                    _detailProjectModel.ddlStatus = _projectProvider.ddlStatus();
                    _detailProjectModel.ddlUser = _projectProvider.ddlUser();
                    return RedirectToAction("Index", "Project");
                }
                else
                {
                    TempData["MyResponseCode"] = resp.responseCodeProject;
                    TempData["MyResponseMessage"] = resp.responseMessageProject;
                    _detailProjectModel.ddlStatus = _projectProvider.ddlStatus();
                    _detailProjectModel.ddlUser = _projectProvider.ddlUser();
                    //return Edit(form.T_WOMEYE_DETAIL_PROJECT_ID.ToString());
                    return RedirectToAction("Index", "Project");
                }

            }
            else
            {
                _detailProjectModel.responseCodeProject = "400";
                _detailProjectModel.responseMessageProject = "Validation Error tes";
                _detailProjectModel.TAHAP = form.TAHAP;
                return View("Edit", _detailProjectModel);
            }

            #endregion


        }

        #region Encrypt & Decrypty

        static string Encrypt(string plainText)
        {
            string encrypted = "";
            string key = "WOMEYE";
            int keyIndex = 0;

            for (int i = 0; i < plainText.Length; i++)
            {
                int xorValue = plainText[i] ^ key[keyIndex];
                encrypted += (char)xorValue;
                keyIndex++;

                if (keyIndex == key.Length)
                    keyIndex = 0;
            }

            return encrypted;
        }

        static string Decrypt(string encryptedText)
        {
            string decrypted = "";
            string key = "WOMEYE";
            int keyIndex = 0;

            for (int i = 0; i < encryptedText.Length; i++)
            {
                int xorValue = encryptedText[i] ^ key[keyIndex];
                decrypted += (char)xorValue;
                keyIndex++;

                if (keyIndex == key.Length)
                    keyIndex = 0;
            }

            return decrypted;
        }

        #endregion

    }
}
