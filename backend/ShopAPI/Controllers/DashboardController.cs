using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Application.DTOs;
using ShopAPI.Application.Queries;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin, manager")]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("summary")]

        public async Task<ActionResult<DashboardSummaryDTO>> GetSummary()
        {

            var query = new GetDashboardSummaryQuery();
            var summary = await _mediator.Send(query);
            return Ok(summary);
        }

        [HttpGet("graphByCategory")]
        public async Task<ActionResult<IEnumerable<GraphCategoryDTO>>> GetGraphCategories()
        {
            var query = new GetGraphCategoryQuery();
            var data = await _mediator.Send(query);
            return Ok(data);
        }
    }
}
