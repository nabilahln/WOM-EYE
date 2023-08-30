using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WOM_EYE.Models.Projects
{
	public class DetailProjectModel : DetailProjectAddSaveViewModel
	{
		#region Atribut

		public int T_WOMEYE_DETAIL_PROJECT_ID { get; set; }
		public string T_WOMEYE_DETAIL_PROJECT_UID { get; set; }

		[DisplayName("Project")]
		public string PROJECT_ID { get; set; }

		[DisplayName("Phase")]
		public string TAHAP { get; set; }

		[DisplayName("Start Date")]
		public string START_DT { get; set; }

		[DisplayName("End Date")]
		public string END_DT { get; set; }

		[Required(ErrorMessage = "The URL field is required")]
		[Url(ErrorMessage = "Please enter a valid URL")]
		[DisplayName("G-Suite Link")]
		public string DOKUMEN { get; set; }

		[DisplayName("Status")]
		public string STATUS_PROJECT { get; set; }

		[DisplayName("Info")]
		public string KETERANGAN { get; set; }

		[DisplayName("Date Create")]
		public string DTM_CRT { get; set; }

		[DisplayName("Date Update")]
		public string DTM_UPD { get; set; }

		[DisplayName("Realization Start Date")]
		public string REALIZATION_START_DT { get; set; }

		[DisplayName("Realization End Date")]
		public string REALIZATION_END_DT { get; set; }

		public string IS_ACCESS { get; set; }

		#endregion

		public List<DetailProjectModel> ListDetailProject { get; set; }

	}

	public class DetailProjectAddSaveViewModel : ProjectModel
	{
		public List<SelectListTahap> ddlTahap { get;set; }
    }

	public class SelectListTahap 
	{
		public string key { get; set; }
		public string value { get; set; }

	}

}
