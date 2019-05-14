using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace WebserviceMain.Login
{
	[Route("[controller]")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly LoginHandler _loginHandler;

		public LoginController(LoginHandler loginHandler)
		{
			_loginHandler = loginHandler;
		}

		[Route("DoLogin")]
		[HttpPost]
		public bool DoLogin([FromBody] LoginRequestModel model)
		{
			return _loginHandler.DoLogin(model);
		}
	}
}
