using WebScrapper.Model;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class WebScrapperController : ControllerBase
    {
        [HttpPost("GetWebScrapper")]
        public async Task<GetWebScrapperResponse> GetWebScraperResults([FromBody] GetWebScrapperRequest request)
        {
            GoogleScraper googleScraper = new GoogleScraper();
            if (request == null || String.IsNullOrEmpty(request.Url)|| String.IsNullOrEmpty(request.Phrase))
            {
                var response = new GetWebScrapperResponse();
                response.IsSuccess = false;
                response.Message = "Url and Phrase cannot be null or empty.";
                return response;
            }
            return await googleScraper.GetWebScraperResult(request.Url,request.Phrase);
        }

        [HttpGet("HealthTest")]
        public GetWebScrapperResponse HeathyTest()
        {
            var webScraperResultList = new List<WebScraperResult>();
            var webScraperResult = new WebScraperResult();
            webScraperResult.Address = "www.google.com";
            webScraperResult.Position = 0;
            webScraperResult.IsMatch = true;
            webScraperResultList.Add(webScraperResult);
            GetWebScrapperResponse response = new GetWebScrapperResponse();
            response.WebScraperResultList = webScraperResultList;
            response.IsSuccess = true;
            response.Message = "OK";
            return response;
        }
    }

}
