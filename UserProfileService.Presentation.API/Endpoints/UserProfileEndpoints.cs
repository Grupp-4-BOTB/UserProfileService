using UserProfileService.Application.Services;
using UserProfileService.Domain.Entities;
using UserProfileService.Domain.ValueObjects;

namespace UserProfileService.Presentation.API.Endpoints;

public static class UserProfileEndpoints
{
    public static void MapUserProfileEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/profiles")
            .WithTags("Profiles");

        group.MapPost("/", CreateAsync)
            .Produces<UserProfile>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapGet("/{ownerId}", GetByOwnerIdAsync)
            .Produces<UserProfile>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{ownerId}", UpdateAsync)
            .Produces<UserProfile>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> CreateAsync(CreateProfileRequest request, UserProfileAppService service, CancellationToken ct)
    {
        var ownerId = OwnerId.From(request.OwnerId);
        var profile = await service.CreateAsync(ownerId, request.FirstName, request.LastName, request.PhoneNumber, request.Description, request.PhotoUrl, ct);

        return Results.Created($"/api/profiles/{profile.OwnerId.Value}", profile);
    }

    private static async Task<IResult> GetByOwnerIdAsync(string ownerId, UserProfileAppService service, CancellationToken ct)
    {
        var owner = OwnerId.From(ownerId);
        var profile = await service.GetByOwnerIdAsync(owner, ct);
        return profile is null ? Results.NotFound() : Results.Ok(profile);
    }

    private static async Task<IResult> UpdateAsync(string ownerId, UpdateProfileRequest request, UserProfileAppService service, CancellationToken ct)
    {
        var owner = OwnerId.From(ownerId);
        var profile = await service.UpdateAsync(owner, request.FirstName, request.LastName, request.PhoneNumber, request.Description, request.PhotoUrl, ct);
        return profile is null ? Results.NotFound() : Results.Ok(profile);
    }
}

public record CreateProfileRequest(string OwnerId, string FirstName, string LastName, string? PhoneNumber = null, string? Description = null, string? PhotoUrl = null);
public record UpdateProfileRequest(string FirstName, string LastName, string? PhoneNumber = null, string? Description = null, string? PhotoUrl = null);