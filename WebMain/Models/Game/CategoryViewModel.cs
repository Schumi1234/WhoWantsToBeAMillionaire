using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebMain.Models.Game
{
	public class CategoryViewModel : BaseViewModel
	{
		public string CategoryName { get; set; }
		public bool Chosen { get; set; }
		public int CategoryId { get; set; }
	}
}
