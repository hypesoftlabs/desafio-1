using Hypesoft.Application.DTOs;
using MediatR;

namespace Hypesoft.Application.Queries.Dashboard;

public class GetDashboardDataQuery : IRequest<ApiResponseDto<DashboardDto>>
{
}
