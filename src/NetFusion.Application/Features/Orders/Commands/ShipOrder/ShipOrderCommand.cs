using MediatR;
using NetFusion.Domain.ValueObjects;

namespace NetFusion.Application.Features.Orders.Commands.ShipOrder;

public record ShipOrderCommand(Guid OrderId, Address ShippingAddress) : IRequest;
