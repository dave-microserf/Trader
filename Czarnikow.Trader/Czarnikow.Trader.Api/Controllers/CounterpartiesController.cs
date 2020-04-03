namespace Czarnikow.Trader.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Application.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    [ApiController]
    public class CounterpartiesController : ControllerBase
    {
        private readonly ILogger<CounterpartiesController> logger;
        private readonly IApplicationService service;

        public CounterpartiesController(
            ILogger<CounterpartiesController> logger, 
            IApplicationService service)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/counterparties
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                return this.Ok(await this.service.GetCounterpartiesAsync());
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, Constants.ErrorMessage);
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/counterparties/1/trades
        [HttpGet("{counterpartyId}/trades", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTradesAsync(int counterpartyId)
        {
            try
            {
                return this.Ok(await this.service.GetCounterpartyTradesAsync(counterpartyId));
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, $"{Constants.ErrorMessage}. CounterpartyId: {counterpartyId}");
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}