using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable InconsistentNaming

namespace WebserviceMain.Database.Tables
{
	[Table("tblGame2Category", Schema = "dbo")]
	public class Game2Category
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "Game2Category Id")]
		public int intGame2CategoryID { get; set; }

		[ForeignKey("Game")]
		[Required]
		public int intGameID { get; set; }

		[ForeignKey("Category")]
		[Required]
		public int intCategoryID { get; set; }

		public virtual Game Game { get; set; }
		public virtual Category Category { get; set; }

	}
}
