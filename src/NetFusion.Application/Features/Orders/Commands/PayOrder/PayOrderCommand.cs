using MediatR;

namespace NetFusion.Application.Features.Orders.Commands.PayOrder;

public record PayOrderCommand(Guid OrderId) : IRequest;
