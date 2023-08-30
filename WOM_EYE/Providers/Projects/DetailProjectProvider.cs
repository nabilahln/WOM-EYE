using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.Interfaces.Projects;
using WOM_EYE.Models.Projects;

namespace WOM_EYE.Providers.Projects
{
	public class DetailProjectProvider : IDetailProjectProvider
	{
		private readonly IDbConnection _dbConnection;
		private DetailProjectModel _detailProjectModel;
		private List<DetailProjectModel> _listDetailProjectModel;
		private List<SelectListStatus> _listStatus;
		private List<SelectListTahap> _listTahap;

        public DetailProjectProvider(
            IDbConnection dbConnection,
            DetailProjectModel detailProjectModel,
            List<DetailProjectModel> listDetailProjectModel,
            List<SelectListStatus> listStatus
, List<SelectListTahap> listTahap)
        {
            this._dbConnection = dbConnection;
            this._detailProjectModel = detailProjectModel;
            this._listDetailProjectModel = listDetailProjectModel;
            this._listStatus = listStatus;
            _listTahap = listTahap;
        }

        public List<SelectListTahap> ddlTahap()
		{
			string sp = "spWOMEYE_GetTahapDetail";
			var resp = _dbConnection.ExecuteReader(sp, commandType: CommandType.StoredProcedure, commandTimeout: 30);
			while (resp.Read())
			{
				SelectListTahap model = new SelectListTahap();
				model.key = resp[0].ToString();
				model.value = resp[1].ToString();
				_listTahap.Add(model);
			}
			resp.Close();
			return _listTahap;
		}

		public List<DetailProjectModel> getAllDetailProject(int id)
		{
			string spName = "spWOMEYE_AllDetailProject";

			var
				dataDetailProject = _dbConnection.ExecuteReader(spName, new { @projectId = id }, commandType: CommandType.StoredProcedure, commandTimeout: 30);

			while (dataDetailProject.Read())
			{
				DetailProjectModel detailProject = new DetailProjectModel();
				detailProject.T_WOMEYE_DETAIL_PROJECT_ID = Convert.ToInt32(dataDetailProject[0]);
				detailProject.PROJECT_ID = dataDetailProject[1].ToString();
				detailProject.TAHAP = dataDetailProject[2].ToString();
				detailProject.START_DT = dataDetailProject[3].ToString();
				detailProject.END_DT = dataDetailProject[4].ToString();
				detailProject.REALIZATION_START_DT = dataDetailProject[5].ToString();
				detailProject.REALIZATION_END_DT = dataDetailProject[6].ToString();
				detailProject.DOKUMEN = dataDetailProject[7].ToString();
				detailProject.STATUS_PROJECT = dataDetailProject[8].ToString();
				detailProject.KETERANGAN = dataDetailProject[9].ToString();
				detailProject.DTM_CRT = dataDetailProject[10].ToString();
				detailProject.DTM_UPD = dataDetailProject[11].ToString();
				detailProject.T_WOMEYE_DETAIL_PROJECT_UID = dataDetailProject[12].ToString();
				_listDetailProjectModel.Add(detailProject);
			}
			dataDetailProject.Close();

			return _listDetailProjectModel;
		}

		public List<DetailProjectModel> getAllDetailProjectNew(string projectId, string userName)
		{
			string spName = "spWOMEYE_AllDetailProjectNew";

			var
				dataDetailProject = _dbConnection.ExecuteReader(spName, new { @projectId = projectId, @userName = userName }, commandType: CommandType.StoredProcedure, commandTimeout: 30);

			while (dataDetailProject.Read())
			{
				DetailProjectModel detailProject = new DetailProjectModel();
				detailProject.T_WOMEYE_DETAIL_PROJECT_ID = System.Convert.ToInt32(dataDetailProject[0]);
				//detailProject.T_WOMEYE_DETAIL_PROJECT_ID = ToEncrypt(dataDetailProject[0].ToString());
				detailProject.PROJECT_ID = dataDetailProject[1].ToString();
				detailProject.TAHAP = dataDetailProject[2].ToString();
				detailProject.START_DT = dataDetailProject[3].ToString();
				detailProject.END_DT = dataDetailProject[4].ToString();
				detailProject.REALIZATION_START_DT = dataDetailProject[5].ToString();
				detailProject.REALIZATION_END_DT = dataDetailProject[6].ToString();
				detailProject.DOKUMEN = dataDetailProject[7].ToString();
				detailProject.STATUS_PROJECT = dataDetailProject[8].ToString();
				detailProject.KETERANGAN = dataDetailProject[9].ToString();
				detailProject.DTM_CRT = dataDetailProject[10].ToString();
				detailProject.DTM_UPD = dataDetailProject[11].ToString();
				//detailProject.T_WOMEYE_DETAIL_PROJECT_UID = ToEncrypt(dataDetailProject[12].ToString());
				detailProject.T_WOMEYE_DETAIL_PROJECT_UID = dataDetailProject[12].ToString();
				detailProject.IS_ACCESS = dataDetailProject[13].ToString();
				_listDetailProjectModel.Add(detailProject);
			}
			dataDetailProject.Close();

			return _listDetailProjectModel;
		}

		public DetailProjectModel getDetailProjectById(int id)
		{
			string sp = "spWOMEYE_GetDetailProjectById";

			var resp = _dbConnection.QueryFirstOrDefault<DetailProjectModel>(sp, new { @id = id }, commandType: CommandType.StoredProcedure, commandTimeout: 30);

			return resp;
		}

		//public DetailProjectModel getDetailProjectByUID(string uid)
		//{
		//	string sp = "spWOMEYE_GetDetailProjectByUID";

		//	var resp = _dbConnection.QueryFirstOrDefault<DetailProjectModel>(sp, new { @uid = uid }, commandType: CommandType.StoredProcedure, commandTimeout: 30);

		//	return resp;
		//}
		public DetailProjectModel getDetailProjectByUID(string uid, string userId)
		{
			string sp = "spWOMEYE_GetDetailProjectByUID2";

			var resp = _dbConnection.QueryFirstOrDefault<DetailProjectModel>(sp, new { @uid = uid, @userId = userId }, commandType: CommandType.StoredProcedure, commandTimeout: 30);

			return resp;
		}

		public ResponseMessage UpdateDetailProject(DetailProjectModel form)
		{
			string sp = "spWOMEYE_UpdateDetailProject";
			try
			{
				var resp = _dbConnection.QueryFirstOrDefault<ResponseMessage>(sp, new
				{
					@T_WOMEYE_DETAIL_PROJECT_ID = form.T_WOMEYE_DETAIL_PROJECT_ID,
					@START_DT = form.START_DT,
					@END_DT = form.END_DT,
					@REALIZATION_START_DT = form.REALIZATION_START_DT,
					@REALIZATION_END_DT = form.REALIZATION_END_DT,
					@DOKUMEN = form.DOKUMEN,
					@STATUS = form.STATUS_PROJECT,
					@KETERANGAN = form.KETERANGAN,
					@CATATAN = form.CATATAN
				}, commandType: CommandType.StoredProcedure, commandTimeout: 30);
				return resp;
			}
			catch (Exception e)
			{
				ResponseMessage resp = new ResponseMessage();
				resp.responseCodeProject = "500";
				resp.responseMessageProject = e.Message;
				return resp;
			}
		}

		public ResponseMessage UpdateDetailProjectUID(DetailProjectModel form)
		{
			string sp = "spWOMEYE_UpdateDetailProjectUID";
			try
			{
				var resp = _dbConnection.QueryFirstOrDefault<ResponseMessage>(sp, new
				{
					@T_WOMEYE_DETAIL_PROJECT_UID = form.T_WOMEYE_DETAIL_PROJECT_UID,
					@START_DT = form.START_DT,
					@END_DT = form.END_DT,
					@REALIZATION_START_DT = form.REALIZATION_START_DT,
					@REALIZATION_END_DT = form.REALIZATION_END_DT,
					@DOKUMEN = form.DOKUMEN,
					@STATUS = form.STATUS_PROJECT,
					@KETERANGAN = form.KETERANGAN,
					@CATATAN = form.CATATAN
				}, commandType: CommandType.StoredProcedure, commandTimeout: 30);
				return resp;
			}
			catch (Exception e)
			{
				ResponseMessage resp = new ResponseMessage();
				resp.responseCodeProject = "500";
				resp.responseMessageProject = e.Message;
				return resp;
			}
		}

		public ResponseMessage AddDetailProject(DetailProjectModel form)
		{
			string sp = "spWOMEYE_AddDetailProject";
			try
			{
				var resp = _dbConnection.QueryFirstOrDefault<ResponseMessage>(sp, new
				{
					@PROJECT_UID = form.T_WOMEYE_PROJECT_UID,
					@TAHAP = form.TAHAP,
					@START_DT = form.START_DT,
					@END_DT = form.END_DT,
					@REALIZATION_START_DT = form.REALIZATION_START_DT,
					@REALIZATION_END_DT = form.REALIZATION_END_DT,
					@DOKUMEN = form.DOKUMEN,
					@STATUS = form.STATUS_PROJECT,
					@KETERANGAN = form.KETERANGAN,
					@USR_CRT = form.USR_CRT
				}, commandType: CommandType.StoredProcedure, commandTimeout: 30);
				return resp;
			}
			catch (Exception e)
			{
				ResponseMessage resp = new ResponseMessage();
				resp.responseCodeProject = "500";
				resp.responseMessageProject = e.Message;
				return resp;
			}
		}

    }
}
