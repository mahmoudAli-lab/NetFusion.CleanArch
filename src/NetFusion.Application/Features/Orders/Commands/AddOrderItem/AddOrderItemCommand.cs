using MediatR;
using NetFusion.Domain.ValueObjects;

namespace NetFusion.Application.Features.Orders.Commands.AddOrderItem;

public record AddOrderItemCommand(Guid OrderId, string ProductName, int Quantity, decimal Price) : IRequest;
