using System;
using System.Collections.Generic;

namespace SharedModels
{
	public class RankingModel
	{
		public double EvaluationPoints { get; set; }
		public string PlayerName { get; set; }
		public DateTime GameBegin { get; set; }
		public DateTime GameEnd { get; set; }

		public string TimeOfGame { get; set; } 
		public IEnumerable<CategoryModel> PlayedCategories { get; set; }
		public int Score { get; set; }
		public bool Delete { get; set; }
		public int GameId { get; set; }
	}
}
