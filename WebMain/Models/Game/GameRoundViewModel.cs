using System;
using System.Collections.Generic;

namespace WebMain.Models.Game
{
	public class GameRoundViewModel : BaseViewModel
	{
		public int GameId { get; set; }

		public DateTime GameBegin { get; set; }
		public DateTime GameEnd { get; set; }
		public List<QuestionViewModel> QuestionsToPlay { get; set; }

		public QuestionViewModel QuestionForTheRound { get; set; }

		public int Score { get; set; }

		public bool JokerUsed { get; set; }
	}
}
