using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedModels;

namespace WebMain.Models.Edit
{
	public class EditQuestionAndAnswerViewModel : BaseViewModel
	{
		public IEnumerable<CategoryModel> Categories { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }

		public int SelectedCategoryId { get; set; } 

		public int CorrectAnswerIndex { get; set; }
		public List<AnswerModel> Answers { get; set; }

		public bool Delete { get; set; }
	}
}
