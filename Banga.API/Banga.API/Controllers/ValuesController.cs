using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Banga.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string test;
        public ValuesController()
        {
            _httpClient = new HttpClient();
            test = "https://infra.devskills.app/api/credit-data";

        }
        [HttpGet("test/{ssn}")]
        public async Task<ActionResult<AssessedIncomeDetails>> Get(int ssn)
        {

            string test = "424-11-9327";
            var baseUrl = "https://infra.devskills.app/api/credit-data";
            var response = await _httpClient.GetAsync($"{baseUrl}/assessed-income/{test}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return Ok(JsonConvert.DeserializeObject<AssessedIncomeDetails>(content));
            }
            else
            {
                return BadRequest();
            }
        }


           





    }
}




public class AssessedIncomeDetails
{
    [JsonProperty("assessed_income")]
    public int AssessedIncome { get; set; }
}