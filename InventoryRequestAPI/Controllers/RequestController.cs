using InventoryRequestAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InventoryRequestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private const string Key = "requests";
        private IConfiguration _configuration;
        private IUtils _utils;
        public RequestController(IConfiguration configuration, IUtils utils)
        {
            _configuration = configuration;
            _utils = utils;
        }

        [HttpGet(Name = "GetRequests")]
        public async Task<ActionResult<IEnumerable<Request>>> GetAsync()
        {
            var apiURL = _configuration["RequestInfoAPI"];
            var response = await _utils.ApiCallAsync(apiURL);

            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                JObject o = JObject.Parse(apiResponse);
                List<Request> requestList = JsonConvert.DeserializeObject<List<Request>>(o[Key].ToString());

                return Ok(requestList);
            }
            else
                return BadRequest(response.ReasonPhrase);
           
        }
    }
}
