using System.Net.Http.Json;
using NetFusion.Application.Features.Orders.Commands.CreateOrder;
using NetFusion.Application.Features.Orders.Commands.AddOrderItem;
using NetFusion.Domain.ValueObjects;
using Xunit;

namespace NetFusion.IntegrationTests.Orders;

public class OrderApiTests : TestBase
{
    public OrderApiTests(Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<Program> factory) : base(factory) { }

    [Fact]
    public async Task CreateOrder_Should_Return_Id()
    {
        var command = new CreateOrderCommand(Guid.NewGuid());

        var response = await Client.PostAsJsonAsync("/api/orders", command);
        response.EnsureSuccessStatusCode();

        var orderId = await response.Content.ReadFromJsonAsync<Guid>();
        Assert.NotEqual(Guid.Empty, orderId);
    }

    [Fact]
    public async Task AddItem_Should_Succeed()
    {
        var command = new CreateOrderCommand(Guid.NewGuid());
        var createResponse = await Client.PostAsJsonAsync("/api/orders", command);
        var orderId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var addItem = new AddOrderItemCommand(orderId, "Phone", 1, 500);
        var addResponse = await Client.PostAsJsonAsync($"/api/orders/{orderId}/items", addItem);

        addResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetOrder_Should_Return_Order()
    {
        var command = new CreateOrderCommand(Guid.NewGuid());
        var createResponse = await Client.PostAsJsonAsync("/api/orders", command);
        var orderId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var response = await Client.GetAsync($"/api/orders/{orderId}");
        response.EnsureSuccessStatusCode();

        var order = await response.Content.ReadAsStringAsync();
        Assert.Contains(orderId.ToString(), order);
    }
}
