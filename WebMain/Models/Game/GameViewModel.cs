using System.Collections.Generic;
using SharedModels;

namespace WebMain.Models.Game
{
	public class GameViewModel
	{
		public QuestionModel Question { get; set; }
		public IEnumerable<AnswerModel> Answers { get; set; }

		public int Score { get; set; }
	}
}
