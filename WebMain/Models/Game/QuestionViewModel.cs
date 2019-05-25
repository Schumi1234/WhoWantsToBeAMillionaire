using System.Collections.Generic;
using SharedModels;

namespace WebMain.Models.Game
{
	public class QuestionViewModel : BaseViewModel
	{
		public string Question { get; set; }
		public IEnumerable<AnswerModel> Answers { get; set; }
		public double PercentageOfCorrectlyAnswered { get; set; }
	}
}
