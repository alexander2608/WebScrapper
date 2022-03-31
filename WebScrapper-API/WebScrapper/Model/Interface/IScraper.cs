namespace WebScrapper.Model
{
    public interface IScraper
    {
        Task<GetWebScrapperResponse> GetWebScraperResult(string url, string phase);

    }
}
