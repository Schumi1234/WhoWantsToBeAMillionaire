using Microsoft.AspNetCore.Mvc;
using WebserviceMain.Login.InternalModel;

namespace WebserviceMain.Login
{
	[Route("[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private LoginHandler _loginHandler;

		public LoginController(LoginHandler loginHandler)
		{
			_loginHandler = loginHandler;
		}

		[HttpPost]
		public LoginResponseModel DoLogin(LoginRequestModel model)
		{
			return _loginHandler.DoLogin(model);
		}
	}
}
