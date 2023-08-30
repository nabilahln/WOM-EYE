using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WOM_EYE.DTOs.Auth;
using WOM_EYE.DTOs.Login;
using WOM_EYE.Interfaces.Login;
using WOM_EYE.Models.Login;
using WOM_EYE.Models.User;

namespace WOM_EYE.Providers.Login
{
	public class LoginProvider : ILogin
	{
		private IDbConnection _dbConnection;
		private LoginModel _loginModel;
		private UserModel _userModel;
		private ForgetPasswordModel _forgetPassword;
		private List<SelectListValidasi> _listSelectValidasi;
		private List<ForgetPasswordModel> _ListForgetPass;
		private List<SelectListPositionPass> _ListPositionPass;
        public LoginProvider(
            IDbConnection dbConnection,
            LoginModel loginModel,
            UserModel userModel,
            List<SelectListValidasi> listSelectValidasi, ForgetPasswordModel forgetPassword, List<ForgetPasswordModel> listForgetPass, List<SelectListPositionPass> listPositionPass)
        {
            this._dbConnection = dbConnection;
            this._loginModel = loginModel;
            this._userModel = userModel;
            _listSelectValidasi = listSelectValidasi;
            _forgetPassword = forgetPassword;
            _ListForgetPass = listForgetPass;
            _ListPositionPass = listPositionPass;
        }

        public ResponseMessage changePassword(ChangePasswordDTO form)
		{
			string spName = "spWOMEYE_ChangePassword";

			try
			{
				var resp = _dbConnection.QueryFirstOrDefault<ResponseMessage>(spName, new
				{
					@userId = form.userId,
					@passOld = form.passOld,
					@passNew = form.passNew
				}, commandType: CommandType.StoredProcedure, commandTimeout: 30);

				return resp;
			}
			catch (Exception e)
			{
				ResponseMessage resp = new ResponseMessage();
				resp.responseCode = "500";
				resp.responseMessage = e.Message;
				return resp;
			}
		}

		public ResponseMessage changePassLogin(ChangePasswordDTO form)
		{
			string spName = "spWOMEYE_ChangePassword";

			try
			{
				var resp = _dbConnection.QueryFirstOrDefault<ResponseMessage>(spName, new
				{
					@userId = form.userId,
					@passOld = form.passOld,
					@passNew = form.passNew
				}, commandType: CommandType.StoredProcedure, commandTimeout: 30);

				return resp;
			}
			catch (Exception e)
			{
				ResponseMessage resp = new ResponseMessage();
				resp.responseCode = "500";
				resp.responseMessage = e.Message;
				return resp;
			}
		}

		public LoginModel getDataUser(LoginModel form)
		{
			string spName = "spWOMEYE_Login1";

			var resp = _dbConnection.QueryFirstOrDefault<LoginModel>(spName, new
			{
				@USER_ID = form.USER_ID,
				@USER_PASS = form.USER_PASS
			}, commandType: CommandType.StoredProcedure, commandTimeout: 30);

			return resp;
		}

		public List<SelectListValidasi> ddlValidasi()
		{
			string sp = "spWOMEYE_GetListValidasi";
			var resp = _dbConnection.ExecuteReader(sp, commandType: CommandType.StoredProcedure, commandTimeout: 30);
			while (resp.Read())
			{
				SelectListValidasi validasi = new SelectListValidasi();
				validasi.JENIS_VALIDASI = resp[0].ToString();
				validasi.ISI_VALIDASI = resp[1].ToString();
				validasi.PESAN_ERROR = resp[2].ToString();
				_listSelectValidasi.Add(validasi);
			}
			resp.Close();
			return _listSelectValidasi;
		}

		public ResponseMessage ForgetPassword(ForgetPasswordModel form)
		{
			string spName = "spWOMEYE_ForgetPassword1";

			try
			{
				var resp = _dbConnection.QueryFirstOrDefault<ResponseMessage>(spName, new
				{
					@userId = form.USER_ID,
					@empid = form.NIK_EMP,
					@CodeUnique = form.CODE_UNIQUE,
					@PassNew = form.USER_PASS
				}, commandType: CommandType.StoredProcedure, commandTimeout: 30);

				return resp;
			}
			catch (Exception e)
			{
				ResponseMessage resp = new ResponseMessage();
				resp.responseCode = "500";
				resp.responseMessage = e.Message;
				return resp;
			}
		}

		public List<SelectListPositionPass> ddlPositionPass()
		{
			string sp = "spWOMEYE_GetPosition";
			var resp = _dbConnection.ExecuteReader(sp, commandType: CommandType.StoredProcedure, commandTimeout: 30);
			while (resp.Read())
			{
				SelectListPositionPass Position = new  SelectListPositionPass();
				Position.key = resp[0].ToString();
				Position.value = resp[1].ToString();
				_ListPositionPass.Add(Position);
			}
			resp.Close();
			return _ListPositionPass;
		}
	}
}
