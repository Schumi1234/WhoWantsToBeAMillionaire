using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModels
{
	public class SaveQuestionAndAnswersRequestModel
	{
		public QuestionModel Question { get; set; }
		public IEnumerable<AnswerModel> Answers { get; set; }
	}
}
