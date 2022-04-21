using InventoryRequestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InventoryRequestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private const string Key = "inventory";
        private IConfiguration _configuration;
        private IUtils _utils;
        public InventoryController(IConfiguration configuration, IUtils utils)
        {
            _configuration = configuration;
            _utils = utils;
        }

        [HttpGet(Name = "GetInventory")]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetAsync() 
        {
            var apiURL = _configuration["InventoryInfoAPI"];
            var response = await _utils.ApiCallAsync(apiURL);

            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                JObject o = JObject.Parse(apiResponse);
                List<Inventory> inventoryList = JsonConvert.DeserializeObject<List<Inventory>>(o[Key].ToString());

                return Ok(inventoryList);
            }
            else
                return BadRequest(response.ReasonPhrase);                  
        }            
    }
}
