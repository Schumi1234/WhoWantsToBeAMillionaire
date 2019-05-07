using System;
using System.Collections.Generic;
using System.Linq;
using SharedModels;
using WebserviceMain.Database;

namespace WebserviceMain.Joker
{
	public class JokerHandler
	{
		private readonly DatabaseController _databaseController;

		public JokerHandler(DatabaseController databaseController)
		{
			_databaseController = databaseController;
		}

		public IEnumerable<AnswerModel> FiftyFiftyJoker(int questionId)
		{
			var answers = _databaseController.GetAnswers(questionId);

			return answers.Where(a => !a.blnCorrect).Take(2).Select(b => new AnswerModel
			{
				Answer = b.strName,
				AnswerId = b.intAnswerID
			});
		}
	}
}
