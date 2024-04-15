using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MathOrWords.Models;
using Newtonsoft.Json;

namespace MathOrWords.Controllers;

internal static class WiktionaryController
{
	public static async void GetWikiData(string playerAnswer)
	{
		string url = $"https://en.wikipedia.org/w/api.php?action=opensearch&search={playerAnswer}&limit=1&namespace=0&format=json";
		HttpClient client = new HttpClient();
		HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, url);

		HttpResponseMessage response = await client.SendAsync(message);
		response.EnsureSuccessStatusCode();
		string responseContent = await response.Content.ReadAsStringAsync();

		var deserializedContent = JsonConvert.DeserializeObject<dynamic>(responseContent);
		string results = deserializedContent[1].ToString();
		char resultContent = results[2]; // Has to be handled!
	}
}
