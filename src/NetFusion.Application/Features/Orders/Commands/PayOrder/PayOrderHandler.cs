using MediatR;
using NetFusion.Domain.Interfaces;

namespace NetFusion.Application.Features.Orders.Commands.PayOrder;

public class PayOrderHandler : IRequestHandler<PayOrderCommand>
{
    private readonly IOrderRepository _orderRepo;

    public PayOrderHandler(IOrderRepository orderRepo)
    {
        _orderRepo = orderRepo;
    }

    public async Task<Unit> Handle(PayOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepo.GetByIdAsync(request.OrderId)
            ?? throw new KeyNotFoundException("Order not found");

        order.MarkAsPaid();
        await _orderRepo.UpdateAsync(order);

        return Unit.Value;
    }
}
