using Newtonsoft.Json;

namespace MathOrWords.Controllers;

internal static class WiktionaryController
{
	/// <summary>
	/// Calls the Wiktionary API to check if player answer is a valid word.
	/// </summary>
	/// <param name="playerAnswer"></param>
	/// <returns></returns>
	public static async Task<bool> GetWikiData(string playerAnswer)
	{
		// Get the first 10 search results formatted as JSON
		string url = $"https://en.wikipedia.org/w/api.php?action=opensearch&search={playerAnswer}&limit=10&namespace=0&format=json";
		HttpClient client = new HttpClient();
		HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, url);
		HttpResponseMessage response = await client.SendAsync(message);
		response.EnsureSuccessStatusCode();
		string responseContent = await response.Content.ReadAsStringAsync();

		// Convert Wiktionary data to JSON
		var deserializedContent = JsonConvert.DeserializeObject<dynamic>(responseContent);

		// Get the search results (2nd array in the JSON)
		string results = deserializedContent[1].ToString();

		try
		{
			// Check if the results string contains anything beyond the standard braces [] (any valid search result)
			char resultContent = results[2];
		}
		catch (Exception ex)
		{
			// Answer not valid (only braces, no search result found)
			return false;
		}

		// Answer valid (something within braces, i.e. search results found)
		return true;
	}
}
