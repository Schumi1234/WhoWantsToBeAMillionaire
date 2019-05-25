using System.Collections.Generic;
using System.Linq;
using SharedModels;
using WebserviceMain.Database;
using WebserviceMain.Database.Tables;

namespace WebserviceMain.Edit
{
	public class EditHandler
	{
		private readonly DatabaseController _databaseController;

		public EditHandler(DatabaseController databaseController)
		{
			_databaseController = databaseController;
		}

		public IEnumerable<CategoryModel> GetAllCategories()
		{
			return _databaseController
				.GetCategories()
				.Select(a => new CategoryModel
				{
					Category = a.strName,
					CategoryId = a.intCategoryId
				});

		}

		public bool SaveEditedCategories(SaveCategoriesRequestModel requestModel)
		{
			bool success;
			IEnumerable<Category> categoryTable;
			if (requestModel.EditedCategories.Any(a => a.Delete && a.Id != 0))
			{
				categoryTable = requestModel.EditedCategories.Where(a => a.Delete).Select(a => new Category
				{
					intCategoryId = a.Id,
					strName = a.Name
				});

				success = _databaseController.Delete(categoryTable);
				if (!success)
				{
					return false;
				}
			}
			
			categoryTable = requestModel.EditedCategories.Where(a => !a.Delete).Select(a => new Category
			{
				intCategoryId = a.Id,
				strName = a.Name
			});

			success = _databaseController.InsertUpdate(categoryTable);

			return success;
		}
	}
}
