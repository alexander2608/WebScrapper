namespace WebScrapper.Model
{
    public class GetWebScrapperRequest
    {
        public string Phrase { get; set; }
        public string Url { get; set; }
    }

    public class GetWebScrapperResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public List<WebScraperResult> WebScraperResultList { get; set; }
    }
}
