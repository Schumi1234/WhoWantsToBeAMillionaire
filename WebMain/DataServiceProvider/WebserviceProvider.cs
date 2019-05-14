using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace WebMain.DataServiceProvider
{
	public class WebserviceProvider
	{
		public T GetDataFromWebService<T>(string url)
		{
			var client = new HttpClient();
			var response = client.GetAsync(new Uri(url)).Result;
			if (!response.IsSuccessStatusCode) return default(T);
			var responseString = response.Content.ReadAsStringAsync().Result;
			return JsonConvert.DeserializeObject<T>(responseString);
		}

		public T PostDataFromWebService<T>(string url, string content)
		{
			var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
			var client = new HttpClient();
			var response = client.PostAsync(new Uri(url), stringContent).Result;
			if (!response.IsSuccessStatusCode) return default(T);
			var responseString = response.Content.ReadAsStringAsync().Result;
			return JsonConvert.DeserializeObject<T>(responseString);
		}
	}
}
