using MediatR;
using NetFusion.Domain.Interfaces;

namespace NetFusion.Application.Features.Orders.Commands.ShipOrder;

public class ShipOrderHandler : IRequestHandler<ShipOrderCommand>
{
    private readonly IOrderRepository _orderRepo;

    public ShipOrderHandler(IOrderRepository orderRepo)
    {
        _orderRepo = orderRepo;
    }

    public async Task<Unit> Handle(ShipOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepo.GetByIdAsync(request.OrderId)
            ?? throw new KeyNotFoundException("Order not found");

        order.MarkAsShipped(request.ShippingAddress);
        await _orderRepo.UpdateAsync(order);

        return Unit.Value;
    }
}
