using MediatR;
using NetFusion.Domain.Interfaces;
using NetFusion.Domain.ValueObjects;

namespace NetFusion.Application.Features.Orders.Commands.AddOrderItem;

public class AddOrderItemHandler : IRequestHandler<AddOrderItemCommand>
{
    private readonly IOrderRepository _orderRepo;

    public AddOrderItemHandler(IOrderRepository orderRepo)
    {
        _orderRepo = orderRepo;
    }

    public async Task<Unit> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepo.GetByIdAsync(request.OrderId)
            ?? throw new KeyNotFoundException("Order not found");

        order.AddItem(request.ProductName, request.Quantity, new Money(request.Price, "USD"));

        await _orderRepo.UpdateAsync(order);
        return Unit.Value;
    }
}
