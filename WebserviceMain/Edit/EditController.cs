using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedModels;

namespace WebserviceMain.Edit
{
	[Route("[controller]")]
	[ApiController]
	public class EditController : Controller
	{
		private readonly EditHandler _editHandler;

		public EditController(EditHandler editHandler)
		{
			_editHandler = editHandler;
		}

		[Route("AllCategories")]
		[HttpGet]
		public IEnumerable<CategoryModel> GetAllCategories()
		{
			return _editHandler.GetAllCategories();
		}

		[Route("SaveCategories")]
		[HttpPost]
		public bool SaveEditedCategories(SaveCategoriesRequestModel requestModel)
		{
			return _editHandler.SaveEditedCategories(requestModel);
		}

		[Route("AllQuestions")]
		[HttpGet]
		public IEnumerable<QuestionModel> GetAllQuestions()
		{
			return _editHandler.GetAllQuestions();
		}

		[Route("AllAnswers")]
		[HttpGet]
		public IEnumerable<AnswerModel> GetAllAnswers()
		{
			return _editHandler.GetAllAnswers();
		}

		[Route("QuestionById")]
		[HttpPost]
		public QuestionModel GetQuestionById([FromBody] int questionId)
		{
			return _editHandler.GetQuestionById(questionId);
		}

		[Route("AnswersOfQuestion")]
		[HttpPost]
		public IEnumerable<AnswerModel> GetAnswers([FromBody] int questionId)
		{
			return _editHandler.GetAnswers(questionId);
		}

		[Route("SaveQuestionWithAnswer")]
		[HttpPost]
		public bool SaveQuestionAndAnswers(SaveQuestionAndAnswersRequestModel requestModel)
		{
			return _editHandler.SaveQuestionAndAnswers(requestModel);
		}

		[Route("DeleteQuestionWithAnswer")]
		[HttpPost]
		public bool DeleteQuestionAndAnswers(DeleteQuestionAndAnswersRequestModel requestModel)
		{
			return _editHandler.DeleteQuestionAndAnswers(requestModel);
		}
	}
}
