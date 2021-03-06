﻿using System;
using System.Collections.Generic;

namespace SharedModels
{
	public class SaveGameRequestModel
	{
		public string PlayerName { get; set; }
		public DateTime GameBegin { get; set; }
		public DateTime GameEnd { get; set; }
		public int Score { get; set; }
		public List<CategoryModel> Categories { get; set; }
	}
}
