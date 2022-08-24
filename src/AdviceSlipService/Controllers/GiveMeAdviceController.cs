using AdviceSlipService.Models;
using AdviceSlipService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace AdviceSlipService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class GiveMeAdviceController : ControllerBase
    {
        private readonly IAdviceSlipProviderService _adviceSlipProviderService;

        public GiveMeAdviceController(IAdviceSlipProviderService adviceSlipProviderService)
        {
            _adviceSlipProviderService = adviceSlipProviderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AdviceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesDefaultResponseType]
        public async Task<IActionResult> Post(AdviceRequest request)
        {
            // request parameter validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (request == null || string.IsNullOrEmpty(request.Topic))
            //{
            //    return BadRequest("The request field 'topic' is required.");
            //}

            var result = await _adviceSlipProviderService.GetAdviceSlip(request);

            return Ok(result);
        }
    }
}
