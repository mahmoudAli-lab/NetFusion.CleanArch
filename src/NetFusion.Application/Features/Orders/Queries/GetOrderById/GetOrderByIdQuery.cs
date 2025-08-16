using MediatR;
using NetFusion.Domain.Aggregates.OrderAggregate;

namespace NetFusion.Application.Features.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid OrderId) : IRequest<Order?>;
