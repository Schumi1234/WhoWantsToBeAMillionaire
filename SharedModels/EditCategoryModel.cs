using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModels
{
	public class EditCategoryModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool Delete { get; set; }
	}
}
