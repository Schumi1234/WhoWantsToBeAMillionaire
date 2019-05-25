using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace WebserviceMain.Edit
{
	[Route("[controller]")]
	[ApiController]
	public class EditController : Controller
	{
		private readonly EditHandler _editHandler;

		public EditController(EditHandler editHandler)
		{
			_editHandler = editHandler;
		}

		[Route("allCategories")]
		[HttpGet]
		public IEnumerable<CategoryModel> GetAllCategories()
		{
			return _editHandler.GetAllCategories();
		}

		[Route("SaveCategories")]
		[HttpPost]
		public bool SaveEditedCategories(SaveCategoriesRequestModel requestModel)
		{
			return _editHandler.SaveEditedCategories(requestModel);
		}
	}
}
