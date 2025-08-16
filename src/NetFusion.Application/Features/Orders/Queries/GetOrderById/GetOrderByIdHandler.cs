using MediatR;
using NetFusion.Domain.Interfaces;
using NetFusion.Domain.Aggregates.OrderAggregate;

namespace NetFusion.Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Order?>
{
    private readonly IOrderRepository _orderRepo;

    public GetOrderByIdHandler(IOrderRepository orderRepo)
    {
        _orderRepo = orderRepo;
    }

    public async Task<Order?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        return await _orderRepo.GetByIdAsync(request.OrderId);
    }
}
