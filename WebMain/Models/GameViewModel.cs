using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedModels;

namespace WebMain.Models
{
	public class GameViewModel
	{
		public IEnumerable<CategoryModel> Categories { get; set; }
	}
}
