using System;
using System.Collections.Generic;

namespace WebMain.Models.Game
{
	public class GameRoundViewModel : BaseViewModel
	{
		public List<CategoryViewModel> Categories { get; set; }
		public int GameId { get; set; }

		public string PlayerName { get; set; }
		public DateTime GameBegin { get; set; }
		public DateTime GameEnd { get; set; }
		public List<QuestionViewModel> QuestionsToPlay { get; set; }

		public QuestionViewModel QuestionForTheRound { get; set; }

		public int IndexOfSelectedAnswer { get; set; } = 0;
		public int Score { get; set; }

		public bool JokerUsed { get; set; }
	}
}
