using MediatR;
using NetFusion.Domain.Interfaces;
using NetFusion.Domain.Aggregates.OrderAggregate;

namespace NetFusion.Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<Order>>
{
    private readonly IOrderRepository _orderRepo;

    public GetAllOrdersHandler(IOrderRepository orderRepo)
    {
        _orderRepo = orderRepo;
    }

    public async Task<IEnumerable<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        // Implementation depends on repository details
        // For now assume repository supports returning all
        // You can expand with pagination/filtering later
        throw new NotImplementedException("Implement repository query for all orders");
    }
}
