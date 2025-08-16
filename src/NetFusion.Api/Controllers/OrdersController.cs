using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetFusion.Application.Features.Orders.Commands.CreateOrder;
using NetFusion.Application.Features.Orders.Commands.AddOrderItem;
using NetFusion.Application.Features.Orders.Commands.PayOrder;
using NetFusion.Application.Features.Orders.Commands.ShipOrder;
using NetFusion.Application.Features.Orders.Queries.GetOrderById;
using NetFusion.Application.Features.Orders.Queries.GetAllOrders;
using NetFusion.Domain.ValueObjects;

namespace NetFusion.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Create a new Order
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand command)
    {
        var orderId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = orderId }, orderId);
    }

    /// <summary>
    /// Add an item to an existing order
    /// </summary>
    [HttpPost("{id}/items")]
    public async Task<IActionResult> AddItem(Guid id, [FromBody] AddOrderItemCommand command)
    {
        if (id != command.OrderId) return BadRequest("Mismatched OrderId");
        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Pay an order
    /// </summary>
    [HttpPost("{id}/pay")]
    public async Task<IActionResult> Pay(Guid id)
    {
        await _mediator.Send(new PayOrderCommand(id));
        return NoContent();
    }

    /// <summary>
    /// Ship an order
    /// </summary>
    [HttpPost("{id}/ship")]
    public async Task<IActionResult> Ship(Guid id, [FromBody] Address address)
    {
        await _mediator.Send(new ShipOrderCommand(id, address));
        return NoContent();
    }

    /// <summary>
    /// Get order by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery(id));
        if (order == null) return NotFound();
        return Ok(order);
    }

    /// <summary>
    /// Get all orders (with optional pagination in the future)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _mediator.Send(new GetAllOrdersQuery());
        return Ok(orders);
    }
}
