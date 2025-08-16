using MediatR;
using NetFusion.Domain.Aggregates.OrderAggregate;

namespace NetFusion.Application.Features.Orders.Queries.GetAllOrders;

public record GetAllOrdersQuery() : IRequest<IEnumerable<Order>>;
