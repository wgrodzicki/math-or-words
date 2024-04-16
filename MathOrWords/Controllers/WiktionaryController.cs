using Newtonsoft.Json;

namespace MathOrWords.Controllers;

internal static class WiktionaryController
{
	public static async Task<bool> GetWikiData(string playerAnswer)
	{
		string url = $"https://en.wikipedia.org/w/api.php?action=opensearch&search={playerAnswer}&limit=10&namespace=0&format=json";
		HttpClient client = new HttpClient();
		HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, url);

		HttpResponseMessage response = await client.SendAsync(message);
		response.EnsureSuccessStatusCode();
		string responseContent = await response.Content.ReadAsStringAsync();

		var deserializedContent = JsonConvert.DeserializeObject<dynamic>(responseContent);
		string results = deserializedContent[1].ToString();

		try
		{
			char resultContent = results[2]; // Has to be handled!
		}
		catch (Exception ex)
		{
			return false;
		}
		return true;
	}
}
