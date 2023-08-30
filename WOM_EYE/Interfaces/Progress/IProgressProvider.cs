using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.Models.Progress;

namespace WOM_EYE.Interfaces.Progress
{
	public interface IProgressProvider
	{
		List<ProgressModel> getAllProgress(int id);
		List<ProgressModel> getAllProgress(string uid, string userId);

		ResponseMessage InsertDataProgress(ProgressModel form);

		ResponseMessage UpdateProgress(ProgressModel form);

		ProgressModel getDataProgressById(int id);
		ProgressModel getDataProgressById(string id, string userId);

		ResponseMessage DeleteProgress(int id);

	}
}
