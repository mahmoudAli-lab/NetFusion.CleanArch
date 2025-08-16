public class GetUserEndpoint : Endpoint<GetUserRequest, GetUserResponse>
{
    private readonly IMediator _mediator;

    public GetUserEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/api/users/{id}");
        Version(1);
        Summary(s =>
        {
            s.Summary = "Get user by ID";
            s.Description = "Retrieves a user by their unique identifier";
            s.Responses[200] = "User found";
            s.Responses[404] = "User not found";
        });
    }

    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        var query = new GetUserQuery(req.Id);
        var result = await _mediator.Send(query, ct);
        
        if (result.IsSuccess)
        {
            await SendOkAsync(result.Value.ToResponse(), ct);
        }
        else
        {
            await SendNotFoundAsync(ct);
        }
    }
}

public static class EndpointExtensions
{
    public static IServiceCollection AddFastEndpoints(this IServiceCollection services)
    {
        return services.AddFastEndpoints()
                      .AddEndpointsApiExplorer();
    }
}
