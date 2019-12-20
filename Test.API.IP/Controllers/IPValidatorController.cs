using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.API.IP.Interfaces;

namespace Test.API.IP.Controllers {
    [Route("api/[controller]")]
    public class IPValidatorController : Controller {

        private readonly IValidator _validator;

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ValidateIps([FromBody]List<string> IPs) {

            if (!ModelState.IsValid) { // re-render the view when validation failed.
                return BadRequest(ModelState);
            }
            return Ok(await _validator.IPValidator(IPs));

        }
    }
}
