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

		[HttpPost("Update")]
		public bool Update([FromBody] string test)
		{
			return false;
		}

		[Route("newCategory")]
		[HttpGet]
		public void InsertCategory()
		{
			_databaseHandler.InsertCategory();
		}

		[HttpPost]
		public ActionResult<IEnumerable<string>> Delete()
		{
			return new string[] { "value1", "value2" };
		}
	}
}
