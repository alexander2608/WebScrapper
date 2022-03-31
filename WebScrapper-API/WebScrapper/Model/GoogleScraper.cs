using System.Net.Http;
using System.Text.RegularExpressions;
using InfoTrack.Controllers;

namespace WebScrapper.Model
{
    public class GoogleScraper : IScraper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">The source that will be search. Example: https://www.google.co.uk/search?num=100&q=land+registry+search</param>
        /// <param name="phase">The domain or name are matched in search result. Example: www.infotrack.co.uk</param>
        public async Task<GetWebScrapperResponse> GetWebScraperResult(string url, string phase)
        {
            var response = new GetWebScrapperResponse();

            var content = await GetHttpContentFromWebPage(url);

            if (String.IsNullOrEmpty(content))
            {
                response.IsSuccess = false;
                response.Message = "The url is not valid.";
                return response;
            }

            List<string> searchResults = GetSearchResultsFromContent(content);

            List<WebScraperResult> webScraperResultList = GetWebScraperResultListFromSearchResults(searchResults);

            FindPhaseInWebScraperResultList(phase, webScraperResultList);

            response.IsSuccess = true;
            response.WebScraperResultList = webScraperResultList;

            return response;

        }

        // Get the html content from the url
        private static async Task<string> GetHttpContentFromWebPage(string url)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:99.0) Gecko/20100101 Firefox/99.0");
            try{
                response = await httpClient.GetAsync(url);
            }
            catch (InvalidOperationException ex)
            {
                return null;
            }

            httpClient.Dispose();

            return await response.Content.ReadAsStringAsync();

        }

        // Use Regex to find the 100 results, return a list of string
        private List<string> GetSearchResultsFromContent(string content)
        {
            List<string> resultList = new List<string>();

            // Find center_col in HTML source code
            Regex regex = new Regex("id=\"center_col\"([\\s\\S]*)id=\"bottomads\"");

            MatchCollection matches = regex.Matches(content);

            string center_Col = matches.First().Value;

            // Find advertisement
            regex = new Regex("class=\"uEierd\".*?data-pcu=\"(.*?)?\"");

            matches = regex.Matches(center_Col);

            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    resultList.Add(match.Groups[1].Value);
                }
            }

            // Find google search result
            regex = new Regex("class=\"g.*?href=\"(h.*?)\"");

            matches = regex.Matches(center_Col);

            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    resultList.Add(match.Groups[1].Value);
                }
            }

            return resultList;
        }

        private List<WebScraperResult> GetWebScraperResultListFromSearchResults(List<string> searchResultList){

            // Convert the search result to webscraper result object
            int position = 1;

            List<WebScraperResult> webScraperResultList = new List<WebScraperResult>();

            foreach (string searchResult in searchResultList)
            {
                WebScraperResult scraperResult = new WebScraperResult();
                scraperResult.Position = position;
                scraperResult.Address = searchResult;
                webScraperResultList.Add(scraperResult);
                position++;
            }

            return webScraperResultList;
        }

        private void FindPhaseInWebScraperResultList (string phase, List<WebScraperResult> webScraperResultList)
        {
            foreach(WebScraperResult webScraperResult in webScraperResultList)
            {
                if (webScraperResult.Address.Contains(phase))
                {
                    webScraperResult.IsMatch = true;
                }
                    
            }
        }

    }

}
