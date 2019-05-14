using System;
using SharedModels;
using WebserviceMain.Database;

namespace WebserviceMain.Login
{
	public class LoginHandler
	{
		private readonly DatabaseController _databaseController;

		public LoginHandler(DatabaseController databaseController)
		{
			_databaseController = databaseController;
		}

		public bool DoLogin(LoginRequestModel model)
		{
			_databaseController.AddUser(model.Username, model.Password);
			throw new NotImplementedException();
		}

		private bool CheckCredentials(LoginRequestModel model)
		{
			throw new NotImplementedException();
		}
	}
}
