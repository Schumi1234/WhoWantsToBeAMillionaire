using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebserviceMain.Database.Tables
{
	[Table("tblUser", Schema = "dbo")]
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "User Id")]
		public int intUserID { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(max)")]
		[Display(Name = "Username")]
		public string strUsername { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(max)")]
		//[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string strPassword { get; set; }
	}
}
