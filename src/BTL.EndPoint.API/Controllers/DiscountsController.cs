using BTL.Application.Contract.Discounts.Commands;
using Framework.Application.CQRS.CommandHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Query.Retrieval.Discounts;

namespace BTL.EndPoint.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IDiscountReadRepository _readRepository;
        public DiscountsController(ICommandBus commandBus, IDiscountReadRepository readRepository)
        {
            _commandBus = commandBus;
            _readRepository = readRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateDiscountCommand command, CancellationToken cancellationToken)
        {
            await _commandBus.DispatchAsync(command, false, cancellationToken);
            return Ok();
        }

        [HttpGet("{skip}/{take}")]
        public async Task<IActionResult> Get(int skip, int take, CancellationToken cancellationToken)
        {
            var response = await _readRepository.GetAsync(skip, take, cancellationToken);
            return Ok(response);
        }
    }
}
