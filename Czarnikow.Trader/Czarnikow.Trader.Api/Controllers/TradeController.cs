namespace Czarnikow.Trader.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Application.Api;
    using Czarnikow.Trader.Application.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/trades")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ILogger<TradeController> logger;
        private readonly IApplicationService service;

        public TradeController(
            ILogger<TradeController> logger,
            IApplicationService service)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: api/trades/1
        [HttpGet("{tradeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(int tradeId)
        {
            try
            {
                var trade = await this.service.UnitOfWork.TradeRepository.FindAsync(tradeId);
                
                if (trade != null)
                {
                    return this.Ok(trade);
                }

                return this.NotFound();
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, Request.Path);
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/trades?counterpartyId=1
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTradesAsync([FromQuery]int counterpartyId)
        {
            try
            {
                return this.Ok(await this.service.UnitOfWork.TradeRepository.FindByCounterpartyAsync(counterpartyId));
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, Request.Path); 
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/trades
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] CreateTrade request)
        {
            try
            {
                var tradeId = await this.service.CreateTradeAsync(request);

                return this.CreatedAtAction("get", new { tradeId }, request);
            }
            catch(Exception exception)
            {
                this.logger.LogError(exception, Request.Path);
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }

        // PUT: api/trades
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync([FromBody] UpdateTrade request)
        {
            try
            {
                var result = await this.service.UpdateTradeAsync(request);

                if (result)
                {
                    return this.Ok();
                }

                return this.NotFound();
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, Request.Path);
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/trades/5
        [HttpDelete("{tradeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(int tradeId)
        {
            try
            {
                var result = await this.service.DeleteTradeAsync(tradeId);
                
                if (result)
                {
                    return this.Ok();
                }

                return this.NotFound();
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, Request.Path);
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}