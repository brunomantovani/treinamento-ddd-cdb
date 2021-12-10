using Api.Controllers.Quotas.ViewModels;
using CdbContext.Application.Quotas.Commands.PurchaseQuotaCommand;
using CdbContext.Application.Quotas.Queries.GetCurrentPositionByQuotaIdQuery;
using CdbContext.DomainModels.Quotas;
using Commons.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Controllers.Quotas
{
    [ApiController]
    [Route("quotas")]
    public sealed class QuotasController
        : ControllerBase
    {
        [HttpGet("{quotaId}/current-position")]
        public async Task<IActionResult> GetCurrentPositionByQuotaIdAsync(
            [FromServices] IQueryHandlerAsync<GetCurrentPositionByQuotaIdQueryRequest, GetCurrentPositionByQuotaIdQueryResponse> handler,
            [FromRoute] Guid quotaId,
            CancellationToken cancellationToken)
        {
            var request = new GetCurrentPositionByQuotaIdQueryRequest(quotaId);

            var result = await handler
                .HandleAsync(request, cancellationToken);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PurchaseQuotaAsync(
            [FromServices] ICommandHandlerAsync<PurchaseQuotaCommandRequest, Quota> handler,
            [FromBody] PurchaseQuotaRequestViewModel viewModel,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var request = new PurchaseQuotaCommandRequest(
                viewModel.InvestmentAccountId.Value,
                viewModel.CheckingAccountId.Value,
                viewModel.Amount.Value);

            var quota = await handler
                .HandleAsync(request, cancellationToken);

            var quotaPosition = quota.GetPosition();

            var resultViewModel = new PurchaseQuotaResponseViewModel
            {
                QuotaPositionAmount = quotaPosition.Amount,
                UpdatedAt = quotaPosition.UpdatedAt
            };

            return Ok(resultViewModel);
        }
    }
}
