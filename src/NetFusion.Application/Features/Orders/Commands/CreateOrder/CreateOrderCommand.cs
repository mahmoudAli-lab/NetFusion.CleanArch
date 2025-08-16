using MediatR;

namespace NetFusion.Application.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(Guid CustomerId) : IRequest<Guid>;
