using System.ComponentModel.DataAnnotations;

namespace WebMain.Models.Login
{
	public class LoginViewModel : BaseViewModel
	{
		[Required(ErrorMessage = "Bitte einen Benutzernamen eingeben")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Bitte ein Passwort eingeben")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
