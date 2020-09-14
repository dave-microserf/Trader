namespace Czarnikow.Trader.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Application.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/counterparties")]
    [ApiController]
    public class CounterpartyController : ControllerBase
    {
        private readonly ILogger<CounterpartyController> logger;
        private readonly IApplicationService service;

        public CounterpartyController(
            ILogger<CounterpartyController> logger,
            IApplicationService applicationService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.service = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
        }

        // GET: api/counterparties
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                return this.Ok(await this.service.UnitOfWork.CounterpartyRepository.ListAsync());
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, Request.Path);
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}