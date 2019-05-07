using System;
using System.Net.Http;
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
	}
}
