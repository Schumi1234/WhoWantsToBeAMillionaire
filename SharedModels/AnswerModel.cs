namespace SharedModels
{
	public class AnswerModel
	{
		public int AnswerId { get; set; }
		public string Answer { get; set; } = string.Empty;
		public int QuestionId { get; set; }
		public bool Correct { get; set; }

		public bool DisableAnswer { get; set; }
	}
}
