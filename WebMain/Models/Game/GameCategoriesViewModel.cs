using System.Collections.Generic;
using SharedModels;

namespace WebMain.Models.Game
{
	public class GameCategoriesViewModel
	{
		public IEnumerable<CategoryModel> Categories { get; set; }
	}
}
