using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using WOM_EYE.Interfaces.Progress;
using WOM_EYE.Interfaces.Projects;
using WOM_EYE.Interfaces.Users;
using WOM_EYE.Models.Progress;
using WOM_EYE.Models.Projects;
using WOM_EYE.Models.User;

namespace WOM_EYE.Controllers
{
    public class ProjectController : Controller
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

        public ProjectController(IProjectProvider projectProvider
            , IDetailProjectProvider detailProjectProvider
            , ProjectModel projectModel
            , List<ProjectModel> listProject
            , IUserProvider userProvider
            , IProgressProvider progressProvider
            , UserModel userLogin)

        {
            this._projectProvider = projectProvider;
            this._detailProjectProvider = detailProjectProvider;
            this._projectModel = projectModel;
            this._listProject = listProject;
            this._userProvider = userProvider;
            this._progressProvider = progressProvider;
            this._userLogin = userLogin;
        }

        [HttpGet]
        public ActionResult Index()
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

            _projectModel.ListProject = _projectProvider.getAllProject(myUserId);

            #region Check Access
            bool createProject = false;
            bool editProject = false;
            bool[] editProjects = new bool[_projectModel.ListProject.Count()];

            if (_userLogin.USER_POSITION == "IT Division Head" || _userLogin.USER_POSITION == "IT Solution Deputy Division Head" || _userLogin.USER_POSITION == "IT Solution Leader" || _userLogin.USER_POSITION == "Function Analyst" || _userLogin.USER_POSITION == "Programmer / Function Analyst")
            {
                createProject = true;
                editProject = true;

            }

            for (int i = 0; i < _projectModel.ListProject.Count(); i++)
            {
                if (_userLogin.USER_NAME == _projectModel.ListProject[i].PROJECT_LEADER) editProjects[i] = true;
            }


            ViewBag.CreateProject = createProject;
            ViewBag.EditProject = editProject;
            ViewBag.EditProjects = editProjects;
            #endregion


            _projectModel.ddlUser = _projectProvider.ddlUser();
            _projectModel.ddlStatus = _projectProvider.ddlStatus();
            _projectModel.responseCodeProject = TempData["MyResponseCode"] as string;
            _projectModel.responseMessageProject = TempData["MyResponseMessage"] as string;


            return View(_projectModel);

        }

        [HttpGet]
        public IActionResult Detail(string uid)
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

            _projectModel = getProjectUID(uid, myUserId);

            if (_projectModel == null)
            {
                TempData["MyResponseCode"] = "404";
                TempData["MyResponseMessage"] = "Project Not Found";
                return RedirectToAction("Index");
            }

            if (_projectModel.IS_DETAIL == "TRUE")
            {

                #region Split Programmer & FA

                string programmerString = _projectModel.PROGRAMMER;
                string faString = _projectModel.FUNCTION_ANALYST;
                string[] programmerSplit = String.IsNullOrEmpty(programmerString) ? new string[] { } :
         programmerString.Split(new char[] { ',' });
                string[] faSplit = String.IsNullOrEmpty(faString) ? new string[] { } :
         faString.Split(new char[] { ',' });

                #endregion

                #region Check Access
                _userLogin = _userProvider.getDataUser(myUserId);
                if (_userLogin.USER_NAME == _projectModel.SOL_LEADER || _userLogin.USER_NAME == _projectModel.PROJECT_LEADER)
                {
                    access = true;
                }
                else if (_userLogin.USER_POSITION == "IT Division Head" || _userLogin.USER_POSITION == "IT Solution Deputy Division Head")
                {
                    access = true;
                }
                else
                {
                    foreach (var programmer in programmerSplit)
                    {
                        if (_userLogin.USER_NAME == programmer) access = true;
                    }

                    foreach (var function_analyst in faSplit)
                    {
                        if (_userLogin.USER_NAME == function_analyst) access = true;
                    }
                }


                if (access == false)
                {
                    TempData["MyResponseCode"] = "203";
                    TempData["MyResponseMessage"] = "You don't have access";
                    return RedirectToAction("Index");
                }

                TempData["MyDetailAccess"] = access;
                #endregion

                dynamic myModel = new ExpandoObject();
                myModel.project = getProjectUID(uid, myUserId);

                //myModel.detailProject = getListDetailProject(_projectModel.T_WOMEYE_PROJECT_ID);
                myModel.detailProject = getListDetailProjectNew(_projectModel.T_WOMEYE_PROJECT_ID.ToString(), myUserId);
                myModel.progress = getListProgress(_projectModel.T_WOMEYE_PROJECT_UID, myUserId);
                myModel.listProgrammer = programmerSplit;
                myModel.listFA = faSplit;
                myModel.responseCodeProgress = TempData["MyResponseCodeProgress"] as string;
                myModel.responseMessageProgress = TempData["MyResponseMessageProgress"] as string;
                myModel.responseCodeNote = TempData["MyResponseCodeNote"] as string;
                myModel.responseMessageNote = TempData["MyResponseMessageNote"] as string;
                return View(myModel);
            }
            else
            {
                {
                    TempData["MyResponseCode"] = "203";
                    TempData["MyResponseMessage"] = "You don't have access";
                    return RedirectToAction("Index");
                }
            }
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
                var theMenu = JsonConvert.DeserializeObject(HttpContext.Session.GetString("MENU"));
                ViewBag.Menu = theMenu;
                ViewBag.UserId = myUserId;
                ViewBag.MUserId = myMUserId;
                ViewBag.MUserName = myUsername;
                ViewBag.UserLogin = _userLogin;
            }

            #endregion

            #region Check Access

            if (_userLogin.USER_POSITION == "IT Division Head" || _userLogin.USER_POSITION == "IT Solution Deputy Division Head" || _userLogin.USER_POSITION == "IT Solution Leader" || _userLogin.USER_POSITION == "Programmer / Function Analyst" || _userLogin.USER_POSITION == "Function Analyst") 
            {
                access = true;
            }


            if (access == false)
            {
                TempData["MyResponseCode"] = "203";
                TempData["MyResponseMessage"] = "You don't have access";
                return RedirectToAction("Index");
            }
            #endregion

            _projectModel.ddlUser = _projectProvider.ddlUser();
            _projectModel.ddlStatus = _projectProvider.ddlStatus();
            _projectModel.ddlJenis = _projectProvider.ddlJenis();
            return View(_projectModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] ProjectModel form)
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

            #region Check Access

            if (_userLogin.USER_POSITION == "IT Division Head" || _userLogin.USER_POSITION == "IT Solution Deputy Division Head" || _userLogin.USER_POSITION == "IT Solution Leader" || _userLogin.USER_POSITION == "Programmer / Function Analyst" || _userLogin.USER_POSITION == "Function Analyst")
            {
                access = true;
            }


            if (access == false)
            {
                TempData["MyResponseCode"] = "203";
                TempData["MyResponseMessage"] = "You don't have access";
                return RedirectToAction("Index");
            }
            #endregion

            #region Validation
            if (string.IsNullOrEmpty(form.SOL_LEADER))
            {
                ModelState.AddModelError("SOL_LEADER", "Sol Leader is required");
            }
            if (string.IsNullOrEmpty(form.PROJECT_LEADER))
            {
                ModelState.AddModelError("PROJECT_LEADER", "Project Leader is required");
            }
            if (string.IsNullOrEmpty(form.JENIS_PROJECT))
            {
                ModelState.AddModelError("JENIS_PROJECT", "Project Type is required");
            }
            if (string.IsNullOrEmpty(form.NO_PROJECT))
            {
                ModelState.AddModelError("NO_PROJECT", "Project Number is required");
            }
            if (string.IsNullOrEmpty(form.DESKRIPSI))
            {
                ModelState.AddModelError("DESKRIPSI", "Description is required");
            }
            if (string.IsNullOrEmpty(Request.Form["checkFA"]))
            {
                ModelState.AddModelError("FUNCTION_ANALYST", "Function Analyst is required");
            }
            if (string.IsNullOrEmpty(Request.Form["checkProgrammer"]))
            {
                ModelState.AddModelError("PROGRAMMER", "Programmer is required");
            }
            if (!string.IsNullOrEmpty(form.NO_PROJECT))
            {
                if (form.NO_PROJECT.Length > 50)
                {
                    ModelState.AddModelError("NO_PROJECT", "No Project just can have 50 character");
                }
            }

            if (!string.IsNullOrEmpty(form.DESKRIPSI))
            {
                if (form.DESKRIPSI.Length > 255)
                {
                    ModelState.AddModelError("DESKRIPSI", "Description just can have 255 character");
                }

            }


            #endregion

            #region Insert

            string[] programmerChecked = Request.Form["checkProgrammer"];
            string[] faChecked = Request.Form["checkFA"];
            string delimiter = ",";

            form.PROGRAMMER = string.Join(delimiter, programmerChecked);
            form.FUNCTION_ANALYST = string.Join(delimiter, faChecked);

            if (ModelState.IsValid)
            {
                var resp = _projectProvider.InsertProject(form);

                if (resp.responseCodeProject == "200")
                {
                    TempData["MyResponseCode"] = resp.responseCodeProject;
                    TempData["MyResponseMessage"] = resp.responseMessageProject;
                    _projectModel.ListProject = _projectProvider.getAllProject();
                    _projectModel.ddlStatus = _projectProvider.ddlStatus();
                    _projectModel.ddlUser = _projectProvider.ddlUser();
                    _projectModel.ddlJenis = _projectProvider.ddlJenis();
                    return RedirectToAction("Index");
                }
                else
                {
                    _projectModel.responseCodeProject = resp.responseCodeProject;
                    _projectModel.responseMessageProject = resp.responseMessageProject;
                    _projectModel.errorMessageProject = resp.errorMessageProject;
                    _projectModel.ListProject = _projectProvider.getAllProject();
                    _projectModel.ddlStatus = _projectProvider.ddlStatus();
                    _projectModel.ddlJenis = _projectProvider.ddlJenis();
                    _projectModel.ddlUser = _projectProvider.ddlUser();
                    return View(_projectModel);
                }


            }
            else
            {
                _projectModel.responseCodeProject = "400";
                _projectModel.responseMessageProject = "Validation Error";
                _projectModel.ddlUser = _projectProvider.ddlUser();
                _projectModel.ddlJenis = _projectProvider.ddlJenis();
                _projectModel.ddlStatus = _projectProvider.ddlStatus();
                return View(_projectModel);
            }
            #endregion
        }

        [HttpPost]
        public IActionResult Edit(String uid)
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

            //uid = Decrypt(uid);
            _projectModel = _projectProvider.getDataProjectByUIDEdit(myUserId, uid);

            #region Check Access

            if (_projectModel.IS_EDIT == "TRUE") access = true;
            else access = false;


            if (access == false)
            {
                TempData["MyResponseCode"] = "203";
                TempData["MyResponseMessage"] = "You do not have access";
                return RedirectToAction("Index");
            }

            #endregion



            if (_projectModel == null)
            {
                TempData["MyResponseCode"] = "404";
                TempData["MyResponseMessage"] = "Project Not Found";
                return RedirectToAction("Index");
            }


            #region Split Programmer & FA
            string programmerString = _projectModel.PROGRAMMER;
            string faString = _projectModel.FUNCTION_ANALYST;
            string[] programmerSplit = String.IsNullOrEmpty(programmerString) ? new string[] { } :
     programmerString.Split(new char[] { ',' });
            string[] faSplit = String.IsNullOrEmpty(faString) ? new string[] { } :
     faString.Split(new char[] { ',' });

            ViewBag.listProgrammer = programmerSplit;
            ViewBag.listFA = faSplit;
            #endregion

            _projectModel.T_WOMEYE_PROJECT_UID = uid;
            _projectModel.ddlUser = _projectProvider.ddlUser();
            _projectModel.ddlStatus = _projectProvider.ddlStatus();
            _projectModel.ddlJenis = _projectProvider.ddlJenis();
            return View(_projectModel);


        }


        [HttpPost]
        public IActionResult EditValidation([Bind] ProjectModel form)
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
            //if (string.IsNullOrEmpty(form.SOL_LEADER))
            //{
            //    ModelState.AddModelError("SOL_LEADER", "Sol Leader tidak boleh kosong");
            //}
            //if (string.IsNullOrEmpty(form.PROJECT_LEADER))
            //{
            //    ModelState.AddModelError("PROJECT_LEADER", "Project Leader tidak boleh kosong");
            //}
            //if (string.IsNullOrEmpty(form.NO_PROJECT))
            //{
            //    ModelState.AddModelError("NO_PROJECT", "No Project tidak boleh kosong");
            //}
            if (string.IsNullOrEmpty(form.DESKRIPSI))
            {
                ModelState.AddModelError("DESKRIPSI", "Description is required");
            }
            if (string.IsNullOrEmpty(Request.Form["checkFA"]))
            {
                ModelState.AddModelError("FUNCTION_ANALYST", "Function Analyst is required");
            }
            if (string.IsNullOrEmpty(Request.Form["checkProgrammer"]))
            {
                ModelState.AddModelError("PROGRAMMER", "Programmer is required");
            }
            if (string.IsNullOrEmpty(form.STATUS))
            {
                ModelState.AddModelError("STATUS", "Status is required");
            }
            if (!string.IsNullOrEmpty(form.STATUS) && (form.STATUS == "4" || form.STATUS == "5")) 
            {
                if (string.IsNullOrEmpty(form.CATATAN))
                {
                    ModelState.AddModelError("CATATAN", "Note is required");
                }
            }
            if (!string.IsNullOrEmpty(form.NO_PROJECT))
            {
                if (form.NO_PROJECT.Length > 50)
                {
                    ModelState.AddModelError("NO_PROJECT", "No Project just can have 50 character");
                }
            }
            if (!string.IsNullOrEmpty(form.DESKRIPSI))
            {
                if (form.DESKRIPSI.Length > 255)
                {
                    ModelState.AddModelError("DESKRIPSI", "Description just can have 255 character");
                }

            }


            #endregion

            #region Edit

            string[] programmerChecked = Request.Form["checkProgrammer"];
            string[] faChecked = Request.Form["checkFA"];
            string delimiter = ",";

            form.PROGRAMMER = string.Join(delimiter, programmerChecked);
            form.FUNCTION_ANALYST = string.Join(delimiter, faChecked);
            form.USR_UPD = myUserId;
            form.T_WOMEYE_PROJECT_UID = form.T_WOMEYE_PROJECT_UID;

            if (ModelState.IsValid)
            {
                var _ddlStatus = _projectModel.ddlStatus = _projectProvider.ddlStatus();
                string projectId = form.T_WOMEYE_PROJECT_ID.ToString();
                string formStatusKey = form.STATUS;
                string checkStatusKey = "";
                string checkStatusValue = "";

                for (int i = 1; i < _ddlStatus.Count; i++)
                {
                    checkStatusKey = _ddlStatus[i].key;
                    checkStatusValue = _ddlStatus[i].value;

                    if (checkStatusValue == "DROP")
                    {
                        if (checkStatusKey.Equals(formStatusKey))
                        {
                            return RedirectToAction("Create", "Drop", new { uid = form.T_WOMEYE_PROJECT_UID });
                        }
                    }

                }

                var resp = _projectProvider.UpdateProjectUID(form);

                TempData["MyResponseCode"] = resp.responseCodeProject;
                TempData["MyResponseMessage"] = resp.responseMessageProject;
                _projectModel.ListProject = _projectProvider.getAllProject();
                _projectModel.ddlStatus = _projectProvider.ddlStatus();
                _projectModel.ddlJenis = _projectProvider.ddlJenis();
                _projectModel.ddlUser = _projectProvider.ddlUser();
                return RedirectToAction("Index");
            }
            else
            {
                _projectModel = _projectProvider.getDataProjectByUIDEdit(myUserId ,form.T_WOMEYE_PROJECT_UID);
                _projectModel.responseCodeProject = "400";
                _projectModel.responseMessageProject = "Validation Error";
                _projectModel.ddlUser = _projectProvider.ddlUser();
                _projectModel.ddlJenis = _projectProvider.ddlJenis();
                _projectModel.ddlStatus = _projectProvider.ddlStatus();

                string programmerString = _projectModel.PROGRAMMER;
                string faString = _projectModel.FUNCTION_ANALYST;
                string[] programmerSplit = String.IsNullOrEmpty(programmerString) ? new string[] { } :
         programmerString.Split(new char[] { ',' });
                string[] faSplit = String.IsNullOrEmpty(faString) ? new string[] { } :
         faString.Split(new char[] { ',' });

                ViewBag.listProgrammer = programmerSplit;
                ViewBag.listFA = faSplit;


                return View("Edit", _projectModel);
            }

            #endregion

        }

        #region GET DYNAMIC MODEL DATA
        public ProjectModel getProject(int id)
        {
            var DProjectModel = _projectProvider.getDataProjectById(id);
            return DProjectModel;
        }

        public ProjectModel getProjectUID(string uid, string userId)
        {
            var DProjectModel = _projectProvider.getDataProjectByUID(uid, userId);
            return DProjectModel;
        }

        public List<DetailProjectModel> getListDetailProject(int id)
        {
            var LDetailProjectModel = _detailProjectProvider.getAllDetailProject(id);
            return LDetailProjectModel;
        }

        public List<DetailProjectModel> getListDetailProjectNew(string projectId, string userName)
        {
            var LDetailProjectModel = _detailProjectProvider.getAllDetailProjectNew(projectId, userName);
            return LDetailProjectModel;
        }

        public List<ProgressModel> getListProgress(string uid, string userId)
        {
            var LProgressModel = _progressProvider.getAllProgress(uid, userId);
            return LProgressModel;
        }
        #endregion


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
