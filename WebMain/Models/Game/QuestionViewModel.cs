using System.Collections.Generic;
using SharedModels;

namespace WebMain.Models.Game
{
	public class QuestionViewModel : BaseViewModel
	{
		public int QuestionId { get; set; } 
		public string Question { get; set; }
		public List<AnswerModel> Answers { get; set; }
		public double PercentageOfCorrectlyAnswered { get; set; }
	}
}
