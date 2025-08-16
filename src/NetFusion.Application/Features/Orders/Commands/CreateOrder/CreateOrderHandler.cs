using MediatR;
using NetFusion.Domain.Aggregates.OrderAggregate;
using NetFusion.Domain.Interfaces;

namespace NetFusion.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepo;

    public CreateOrderHandler(IOrderRepository orderRepo)
    {
        _orderRepo = orderRepo;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order(request.CustomerId);
        await _orderRepo.AddAsync(order);
        return order.Id.Value;
    }
}
