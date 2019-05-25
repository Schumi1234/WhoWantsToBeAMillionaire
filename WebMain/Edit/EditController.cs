using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedModels;
using WebMain.DataServiceProvider;
using WebMain.DataServiceProvider.Enums;
using WebMain.Models.Edit;

namespace WebMain.Edit
{
	public class EditController : Controller
	{
		private readonly WebserviceProvider _webserviceProvider;

		public EditController(WebserviceProvider webserviceProvider)
		{
			_webserviceProvider = webserviceProvider;
		}

		public IActionResult EditHighScoreList()
		{
			return View();
		}

		[HttpGet]
		public IActionResult EditCategories()
		{
			var viewModel = GetAllCategories();
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult EditCategories(EditCategoriesViewModel requestModel)
		{
			var success = SaveCategories(requestModel);
			var viewModel = GetAllCategories();
			if (!success)
			{
				viewModel.ErrorMessage = "Ein Fehler ist aufgetreten";
			}
			
			return View(viewModel);
		}

		public IActionResult EditQuestions()
		{
			return View();
		}

		public IActionResult EditAnswers()
		{
			return View();
		}

		private bool SaveCategories(EditCategoriesViewModel viewModel)
		{
			var requestModel = new SaveCategoriesRequestModel
			{
				EditedCategories = viewModel.Categories.Select(a => new EditCategoryModel
				{
					Name = a.Name,
					Delete = a.Delete,
					Id = a.Id
				})
			};
			var content = JsonConvert.SerializeObject(requestModel);
			var success = _webserviceProvider.PostDataFromWebService<bool>(Controllers.Edit.ToString(), "SaveCategories", content);
			return success;
		}

		private EditCategoriesViewModel GetAllCategories()
		{
			var categories = _webserviceProvider.GetDataFromWebService<IEnumerable<CategoryModel>>(Controllers.Edit.ToString(), "allCategories");
			return new EditCategoriesViewModel
			{
				Categories = categories
					.Select(a =>
						new EditCategoryViewModel
						{
							Name = a.Category,
							Id = a.CategoryId
						}).ToList()
			};
		}

		[HttpPost]
		public IActionResult AddCategory()
		{
			var viewModel = GetAllCategories();
			viewModel.Categories.Add(new EditCategoryViewModel());
			return View("EditCategories", viewModel);
		}
	}
}
