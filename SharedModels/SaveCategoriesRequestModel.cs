using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModels
{
	public class SaveCategoriesRequestModel
	{
		public IEnumerable<EditCategoryModel> EditedCategories { get; set; }
	}
}
