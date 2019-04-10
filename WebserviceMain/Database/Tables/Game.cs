using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable InconsistentNaming

namespace WebserviceMain.Database.Tables
{
	[Table("tblGame", Schema = "dbo")]
	public class Game
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "Game Id")]
		public int intGameID { get; set; }

		[Required]
		[Column(TypeName = "nvarchar(max)")]
		[Display(Name = "Player")]
		public string strPlayerName { get; set; }

		[Required]
		[Column(TypeName = "datetime")]
		[Display(Name = "Begin")]
		public DateTime datBegin { get; set; }

		[Required]
		[Column(TypeName = "datetime")]
		[Display(Name = "End")]
		public DateTime datEnd { get; set; }

		[Required]
		[Column(TypeName = "int")]
		[Display(Name = "Score")]
		public int intScore { get; set; }
	}
}
