using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebserviceMain.Database.Tables;

namespace WebserviceMain.Database
{
	// Url https://localhost:44339/Database
	[Route("[controller]")]
	[ApiController]
	public class DatabaseController : ControllerBase
	{
		private readonly DatabaseHandler _databaseHandler;

		public DatabaseController(DatabaseHandler databaseHandler)
		{
			_databaseHandler = databaseHandler;
		}

		public IEnumerable<Category> GetCategories()
		{
			return _databaseHandler.GetCategories();
		}

		public void Delete<T>(IEnumerable<T> tables)
		{
			_databaseHandler.DeleteEntries(tables);
		}

		public void InsertUpdate<T>(IEnumerable<T> tables)
		{
			_databaseHandler.InsertUpdate(tables);
		}
	}
}
