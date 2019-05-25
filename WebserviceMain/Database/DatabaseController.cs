﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SharedModels;
using WebserviceMain.Database.Tables;

namespace WebserviceMain.Database
{
	// Url https://localhost:44339/Database
	[Route("[controller]")]
	[ApiController]
	public class DatabaseController : ControllerBase
	{
		private readonly DatabaseHandler _databaseHandler;

		public DatabaseController(DatabaseHandler databaseHandler)
		{
			_databaseHandler = databaseHandler;
		}

		public IEnumerable<Question> GetQuestionsByCategories(IEnumerable<int> categoryIds)
		{
			return _databaseHandler.GetQuestionsByCategories(categoryIds);
		}

		public IEnumerable<Category> GetCategories()
		{
			return _databaseHandler.GetCategories();
		}

		public void Delete<T>(IEnumerable<T> tables)
		{
			_databaseHandler.DeleteEntries(tables);
		}

		public void InsertUpdate<T>(IEnumerable<T> tables)
		{
			_databaseHandler.InsertUpdate(tables);
		}

		public IEnumerable<RankingModel> GetRanking()
		{
			return _databaseHandler.GetRanking();
		}

		public IEnumerable<Answer> GetAnswers(int questionId)
		{
			return _databaseHandler.GetAnswers(questionId);
		}

		public Answer GetAnswer(int answerId)
		{
			return _databaseHandler.GetAnswer(answerId);
		}

		public bool AddUser(string username, string password)
		{
			return _databaseHandler.AddUser(username, password);
		}

		public Question GetQuestionById(int questionId)
		{
			return _databaseHandler.GetQuestionById(questionId);
		}
	}
}
