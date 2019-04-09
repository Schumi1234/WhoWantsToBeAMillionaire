using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebserviceMain.Database.Tables;

namespace WebserviceMain.Database
{
	[Route("[controller]")]
	[ApiController]
	public class DatabaseController : ControllerBase
	{
		private readonly DatabaseHandler _databaseHandler;

		public DatabaseController(DatabaseHandler databaseHandler)
		{
			_databaseHandler = databaseHandler;
		}

		public IEnumerable<Category> GetCategory()
		{
			return _databaseHandler.GetCategories();
		}

		public bool Update([FromBody] string test)
		{
			return false;
		}

		public void InsertUpdateCategory()
		{
			_databaseHandler.InsertUpdateCategory();
		}
	}
}
