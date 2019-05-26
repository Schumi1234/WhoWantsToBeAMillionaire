using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModels
{
	public class SaveQuestionAnswerRequestModel
	{
		public int QuestionId { get; set; }
		public bool Correct { get; set; }
	}
}
