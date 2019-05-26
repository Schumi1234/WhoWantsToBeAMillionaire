using System.Collections.Generic;
using SharedModels;

namespace WebMain.Models.Edit
{
	public class RankingViewModel
	{
		public List<RankingModel> Rankings { get; set; }
		public bool LoggedIn { get; set; }
	}
}
