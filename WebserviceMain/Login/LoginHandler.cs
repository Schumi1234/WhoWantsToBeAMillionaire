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
			return _databaseController.CheckLogin(model.Username, model.Password);
		}

		public bool LoggedIn()
		{
			return _databaseController.LoggedIn();
		}
	}
}
