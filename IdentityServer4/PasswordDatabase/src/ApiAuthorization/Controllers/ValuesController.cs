using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static IList<string> Data { get; } = new List<string>
        {
            "value1",
            "value2"
        };
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get() => Ok(Data);

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id) => Ok(Data[id]);

        // POST api/values
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAsync()
        {
            var data = new byte[Request.ContentLength.Value];
            await Request.Body.ReadAsync(data, 0, data.Length);
            string value = Encoding.UTF8.GetString(data);
            Data.Add(value);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize]
        public void Put(int id, [FromBody] string value) => Data[id] = value;

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize]
        public void Delete(int id) => Data.RemoveAt(id);
    }
}
