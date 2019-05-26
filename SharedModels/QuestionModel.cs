namespace SharedModels
{
	public class QuestionModel
	{
		public int QuestionId { get; set; }

		public string Question { get; set; }

		public int NumberAnsweredCorrectly { get; set; }
		public double AnsweredCorrectlyPercentage { get; set; }
		public int CategoryId { get; set; }
	}
}
