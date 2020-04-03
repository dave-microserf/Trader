namespace Czarnikow.Trader.Api.Controllers
{
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Czarnikow.Trader.Application.Interfaces;
    using Czarnikow.Trader.Application.Requests;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    [ApiController]
    public class TradesController : ControllerBase
    {
        private readonly ILogger<TradesController> logger;
        private readonly IApplicationService service;

        public TradesController(
            ILogger<TradesController> logger, 
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
                var trade = await this.service.GetTradeAsync(tradeId);
                
                if (trade != null)
                {
                    return this.Ok(trade);
                }

                return this.NotFound();
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, $"{Constants.ErrorMessage}. TradeId: {0}", tradeId);                
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/trades
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] CreateTradeRequest request)
        {
            try
            {
                var tradeId = await this.service.CreateTradeAsync(request);

                return this.CreatedAtAction("get", new { tradeId }, request);
            }
            catch(Exception exception)
            {
                this.logger.LogError(exception, $"{Constants.ErrorMessage}. Request: {0}", JsonSerializer.Serialize(request));
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }

        // PUT: api/trades
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync([FromBody] UpdateTradeRequest request)
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
                this.logger.LogError(exception, $"{Constants.ErrorMessage}. Request: {0}", JsonSerializer.Serialize(request));
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
                this.logger.LogError(exception, $"{Constants.ErrorMessage}. TradeId: {0}", tradeId);
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}