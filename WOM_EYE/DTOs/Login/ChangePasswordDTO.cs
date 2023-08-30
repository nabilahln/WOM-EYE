using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.Models;

namespace WOM_EYE.DTOs.Auth
{
	public class ChangePasswordDTO : ResponseMessange
	{
		
		public string userId { get; set; }

		
		public string passOld { get; set; }

		public string passNew { get; set; }

		public string passConfirm { get; set; }
	}

	public class ResponseMessange
	{

		public string responseCode { get; set; }
		public string responseMessage { get; set; }
		public string errorMessage { get; set; }

	}
}
