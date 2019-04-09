using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable InconsistentNaming

namespace WebserviceMain.Database.Tables
{
	[Table("tblCategory", Schema = "dbo")]
	public class Category
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "Category Id")]
		public int intCategoryId { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(max)")]
		[Display(Name = "Category")]
		public string strName { get; set; }
	}
}
