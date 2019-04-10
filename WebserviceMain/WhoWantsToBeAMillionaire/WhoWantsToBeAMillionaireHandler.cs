using System.Collections.Generic;
using WebserviceMain.Database;
using WebserviceMain.Database.Tables;

namespace WebserviceMain.WhoWantsToBeAMillionaire
{
	public class WhoWantsToBeAMillionaireHandler
	{
		private DatabaseController _databaseController;

		public WhoWantsToBeAMillionaireHandler(DatabaseController databaseController)
		{
			_databaseController = databaseController;
		}

		public IEnumerable<Category> GetCategories()
		{
			throw new System.NotImplementedException();
		}
	}
}
