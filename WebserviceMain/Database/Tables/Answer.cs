using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable InconsistentNaming

namespace WebserviceMain.Database.Tables
{
	[Table("tblAnswer", Schema = "dbo")]
	public class Answer
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "Answer Id")]
		public int intAnswerID { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(max)")]
		[Display(Name = "Answer")]
		public string strName { get; set; }

		[Required]
		[Column(TypeName = "bit")]
		[Display(Name = "Correct")]
		public bool blnCorrect { get; set; }

		[ForeignKey("Question")]
		[Required]
		public int intQuestionID { get; set; }

		public virtual Question Question { get; set; }
	}
}
