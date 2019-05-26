using System.Collections.Generic;

namespace SharedModels
{
	public class DeleteQuestionAndAnswersRequestModel
	{
		public  IEnumerable<int> QuestionIds { get; set; }
		public IEnumerable<int> AnswerIds { get; set; }
	}
}
