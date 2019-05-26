using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedModels;

namespace WebMain.Models.Edit
{
	public class EditQuestionAndAnswersViewModel : BaseViewModel
	{
		public List<CategoryModel> Categories { get; set; }
		public List<EditQuestionAndAnswerViewModel> QuestionsAndAnswers { get; set; }
	}
}
