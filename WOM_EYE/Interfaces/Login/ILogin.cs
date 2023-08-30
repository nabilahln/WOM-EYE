using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.DTOs.Auth;
using WOM_EYE.DTOs.Login;
using WOM_EYE.Models.Login;

namespace WOM_EYE.Interfaces.Login
{
	public interface ILogin
	{
		LoginModel getDataUser(LoginModel form);
		ResponseMessage changePassword(ChangePasswordDTO form);

		List<SelectListValidasi> ddlValidasi();
		ResponseMessage changePassLogin(ChangePasswordDTO form);

		ResponseMessage ForgetPassword(ForgetPasswordModel form);

		List<SelectListPositionPass> ddlPositionPass();
	}
}
