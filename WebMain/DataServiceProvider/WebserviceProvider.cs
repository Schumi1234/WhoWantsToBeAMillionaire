using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace WebMain.DataServiceProvider
{
	public class WebserviceProvider
	{
		public T GetDataFromWebService<T>(string controllerName, string actionName)
		{
			var url = GetUrl(controllerName, actionName);
			var client = new HttpClient();
			var response = client.GetAsync(new Uri(url)).Result;
			if (!response.IsSuccessStatusCode) return default(T);
			var responseString = response.Content.ReadAsStringAsync().Result;
			return JsonConvert.DeserializeObject<T>(responseString);
		}

		private static string GetUrl(string controllerName, string actionName)
		{
			var url = $"https://localhost:44339/{controllerName}/{actionName}";
			return url;
		}

		public T PostDataFromWebService<T>(string controllerName, string actionName, string content)
		{
			var url = GetUrl(controllerName, actionName);
			var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
			var client = new HttpClient();
			var response = client.PostAsync(new Uri(url), stringContent).Result;
			if (!response.IsSuccessStatusCode) return default(T);
			var responseString = response.Content.ReadAsStringAsync().Result;
			return JsonConvert.DeserializeObject<T>(responseString);
		}
	}
}
