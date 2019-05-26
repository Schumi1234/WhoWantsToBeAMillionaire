using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedModels;
using WebMain.DataServiceProvider;
using WebMain.DataServiceProvider.Enums;
using WebMain.Models.Edit;

namespace WebMain.Edit
{
	public class EditController : Controller
	{
		private readonly WebserviceProvider _webserviceProvider;

		public EditController(WebserviceProvider webserviceProvider)
		{
			_webserviceProvider = webserviceProvider;
		}

		public IActionResult EditHighScoreList()
		{
			return View();
		}

		[HttpGet]
		public IActionResult EditCategories()
		{
			var viewModel = GetAllCategories();
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult EditCategories(EditCategoriesViewModel requestModel)
		{
			var success = SaveCategories(requestModel);
			var viewModel = GetAllCategories();
			if (!success)
			{
				viewModel.ErrorMessage = "Ein Fehler ist aufgetreten";
			}

			return View(viewModel);
		}

		private bool SaveCategories(EditCategoriesViewModel viewModel)
		{
			var requestModel = new SaveCategoriesRequestModel
			{
				EditedCategories = viewModel.Categories.Select(a => new EditCategoryModel
				{
					Name = a.Name,
					Delete = a.Delete,
					Id = a.Id
				})
			};
			var content = JsonConvert.SerializeObject(requestModel);
			var success = _webserviceProvider.PostDataFromWebService<bool>(Controllers.Edit.ToString(), "SaveCategories", content);
			return success;
		}

		private EditCategoriesViewModel GetAllCategories()
		{
			var categories = _webserviceProvider.GetDataFromWebService<IEnumerable<CategoryModel>>(Controllers.Edit.ToString(), "AllCategories");
			return new EditCategoriesViewModel
			{
				Categories = categories
					.Select(a =>
						new EditCategoryViewModel
						{
							Name = a.Category,
							Id = a.CategoryId
						}).ToList()
			};
		}

		public IActionResult EditQuestions()
		{
			var categories = _webserviceProvider.GetDataFromWebService<IEnumerable<CategoryModel>>(Controllers.Edit.ToString(), "AllCategories");
			var questions = _webserviceProvider.GetDataFromWebService<IEnumerable<QuestionModel>>(Controllers.Edit.ToString(), "AllQuestions");
			var answers = _webserviceProvider.GetDataFromWebService<IEnumerable<AnswerModel>>(Controllers.Edit.ToString(), "AllAnswers");
			var viewModel = new EditQuestionAndAnswersViewModel
			{
				Categories = categories.ToList(),
				QuestionsAndAnswers = questions.Select(a => new EditQuestionAndAnswerViewModel
				{
					Name = a.Question,
					SelectedCategoryId = a.CategoryId,
					Answers = answers.Where(b => b.QuestionId == a.QuestionId).ToList(),
					Id = a.QuestionId
				}).ToList()
			};
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult AddCategory()
		{
			var viewModel = GetAllCategories();
			viewModel.Categories.Add(new EditCategoryViewModel());
			return View("EditCategories", viewModel);
		}

		[HttpPost]
		public IActionResult QuestionDetail(IEnumerable<CategoryModel> categories)
		{
			var viewModel = new EditQuestionAndAnswerViewModel
			{
				Answers = new List<AnswerModel>
				{
					new AnswerModel(),
					new AnswerModel(),
					new AnswerModel(),
					new AnswerModel()
				},
				Categories = categories
			};
			return View(viewModel);
		}

		[HttpGet]
		public IActionResult QuestionDetail(int questionId)
		{
			var categories = _webserviceProvider.GetDataFromWebService<IEnumerable<CategoryModel>>(Controllers.Edit.ToString(), "AllCategories");
			var content = JsonConvert.SerializeObject(questionId);
			var question = _webserviceProvider.PostDataFromWebService<QuestionModel>(Controllers.Edit.ToString(), "QuestionById", content);
			var answers = _webserviceProvider.PostDataFromWebService<IEnumerable<AnswerModel>>(Controllers.Edit.ToString(), "AnswersOfQuestion", content);
			var viewModel = new EditQuestionAndAnswerViewModel
			{
				Answers = answers.ToList(),
				Categories = categories.ToList(),
				Name = question.Question,
				Id = questionId,
				SelectedCategoryId = question.CategoryId
			};
			return View(viewModel);
		}

		public IActionResult SaveQuestion(EditQuestionAndAnswerViewModel viewModel)
		{
			viewModel.Answers[viewModel.CorrectAnswerIndex].Correct = true;
			var requestModel = new SaveQuestionAndAnswersRequestModel
			{
				Answers = viewModel.Answers,
				Question = new QuestionModel
				{
					Question = viewModel.Name,
					QuestionId = viewModel.Id,
					CategoryId = viewModel.SelectedCategoryId
				}
			};
			var content = JsonConvert.SerializeObject(requestModel);
			var success = _webserviceProvider.PostDataFromWebService<bool>(Controllers.Edit.ToString(), "SaveQuestionWithAnswer", content);
			return RedirectToAction("EditQuestions");
		}


		//toDO: doesnt work yet
		[HttpGet]
		public IActionResult DeleteQuestionAndAnswers(EditQuestionAndAnswersViewModel viewModel)
		{
			/*
			var requestModel = new DeleteQuestionAndAnswersRequestModel
			{
				QuestionIds = questionIds,
				AnswerIds = answersIds
			};
			var content = JsonConvert.SerializeObject(requestModel);
			var success = _webserviceProvider.PostDataFromWebService<bool>(Controllers.Edit.ToString(), "DeleteQuestionWithAnswer", content);
			*/
			return RedirectToAction("EditQuestions");
		}
	}
}
