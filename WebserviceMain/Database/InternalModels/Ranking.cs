using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebserviceMain.Database.Tables;

namespace WebserviceMain.Database.InternalModels
{
	public class Ranking
	{
		public double EvaluationPoints { get; set; }
		public string PlayerName { get; set; }
		public DateTime GameBegin { get; set; }
		public DateTime GameEnd { get; set; }

		public string TimeOfGame { get; set; } 
		public IEnumerable<Category> PlayedCategories { get; set; }
		public int Score { get; set; }
	}
}
