using MathOrWords.Models;
using Newtonsoft.Json.Linq;
using static MathOrWords.Helpers.Helpers;

namespace MathOrWords.Controllers;

internal static class WikipediaApi
{
    /// <summary>
    /// Calls the Wikipedia API to check if player answer is a valid word.
    /// </summary>
    /// <param name="playerAnswer"></param>
    /// <returns></returns> 
    public static async Task<bool> GetWikiData(string playerAnswer, WordCategory wordCategory)
    {
        // Get the search results formatted as JSON
        string url = $"https://en.wikipedia.org/w/api.php?action=query&list=search&srsearch={playerAnswer}&utf8=&format=json";
        HttpClient client = new HttpClient();
        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, url);

        try
        {
            HttpResponseMessage response = await client.SendAsync(message);
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            return ValidateWikiData(responseContent, wordCategory) == true ? true : false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    /// <summary>
    /// Validates the Wikipedia-fetched data to match player answer with the word category.
    /// </summary>
    /// <param name="responseContent"></param>
    /// <param name="wordCategory"></param>
    /// <returns></returns>
    private static bool ValidateWikiData(string responseContent, WordCategory wordCategory)
    {
        JObject wikiSearch = JObject.Parse(responseContent);

        // Get individual search results
        IList<JToken> wikiSearchResults = wikiSearch["query"]["search"].Children().ToList();

        // No results at all
        if (wikiSearchResults.Count <= 0)
            return false;

        bool categoryFound = false;

        foreach (JToken wikiSearchResult in wikiSearchResults)
        {
            WikiSearchModel category = wikiSearchResult.ToObject<WikiSearchModel>();

            // Check if any result mentions the word category
            if (category.Snippet.Contains(wordCategory.Category))
            {
                categoryFound = true;
                break;
            }

            // Check if any result mentions the word helper category
            foreach (string helperCategory in wordCategory.HelperCategories)
            {
                if (category.Snippet.Contains(helperCategory))
                {
                    categoryFound = true;
                    break;
                }
            }
        }
        return categoryFound;
    }
}
