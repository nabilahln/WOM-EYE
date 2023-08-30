using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WOM_EYE.Models.Login
{
	//testes
	public class LoginModel : SelectListValidasiViewModel
	{
		public int M_WOMEYE_USER_ID { get; set; }

		[DisplayName("User ID")]
		public string USER_ID { get; set; }

		[DisplayName("Password")]
		public string USER_PASS { get; set; }

	}

	public class ResponseMessage
	{
		public string responseCode { get; set; }
		public string responseMessage { get; set; }
		public string errorMessage { get; set; }

    }

    public class SelectListValidasi
    { 
		public string JENIS_VALIDASI { get; set; }
		public string ISI_VALIDASI { get; set; }
		public string PESAN_ERROR { get; set; }
	}

	public class SelectListValidasiViewModel : ResponseMessage
	{
		public List<SelectListValidasi> ddlValidasi { get; set; }
		public List<SelectListPositionPass> ddlPositionPass { get; set; }
	}

	public class SelectListPositionPass
	{
		public string key { get; set; }
		public string value { get; set; }
	}
	public class ForgetPasswordModel : SelectListValidasiViewModel
    {
		[DisplayName("User ID")]
		public string USER_ID { get; set;}

		[DisplayName("NIK Employee")]
		public string NIK_EMP { get; set; }

		[DisplayName("Position")]
		public string POSISI { get; set; }


		[DisplayName("Phone Number")]
		public string NO_TELP { get; set; }

		[DisplayName("Birth Date")]
		public string BOD { get; set; }

		[DisplayName("New Pass")]
		public string USER_PASS { get; set; }

		public string M_WOMEYE_USER_ID { get; set; }

		[DisplayName("Confirm Password")]
		public string ConfirmPass { get; set; }
		public List<ForgetPasswordModel> listforgetPassword { get; set; }
		[DisplayName("Code Unique")]
		public string CODE_UNIQUE { get; set; }

	}

}
