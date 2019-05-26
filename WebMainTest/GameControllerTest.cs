using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebMain.DataServiceProvider;
using WebMain.Game;
using WebMain.Game.Helper;

namespace WebMainTest
{
	[TestClass]
	public class GameControllerTest
	{
		[TestMethod]
		public void FiftyFiftyJoker()
		{
			var gameController = new GameController(new GameHelper(new WebserviceProvider()));
		}
	}
}
