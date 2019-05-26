using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable InconsistentNaming

namespace WebserviceMain.Database.Tables
{
	[Table("tblQuestion", Schema = "dbo")]
	public class Question
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "Question Id")]
		public int intQuestionId { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(max)")]
		[Display(Name = "Question")]
		public string strName { get; set; }

		[Column(TypeName = "int")]
		[Display(Name = "intAnsweredCorrectly")]
		public int intAnsweredCorrectly { get; set; }

		[Column(TypeName = "int")]
		[Display(Name = "intAnsweredWrong")]
		public int intAnsweredWrong { get; set; }

		[ForeignKey("Category")]
		[Required]
		public int intCategoryId { get; set; }

		public virtual Category Category { get; set; }
	}
}
