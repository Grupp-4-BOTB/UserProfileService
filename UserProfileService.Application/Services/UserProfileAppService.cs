using UserProfileService.Domain.Entities;
using UserProfileService.Domain.Repositories;
using UserProfileService.Domain.ValueObjects;

namespace UserProfileService.Application.Services;

public class UserProfileAppService
{
    private readonly IUserProfileRepository _repository;
    
    public UserProfileAppService(IUserProfileRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserProfile> CreateAsync(OwnerId ownerId, string firstName, string lastName, string? phoneNumber, string? description, string? photoUrl, CancellationToken ct)
    {
        var profile = UserProfile.Create(ownerId, firstName, lastName, phoneNumber, description, photoUrl);
        await _repository.AddAsync(profile, ct);
        return profile;
    }

    public async Task<UserProfile?> GetByOwnerIdAsync(OwnerId ownerId, CancellationToken ct)
    {
        return await _repository.GetByOwnerIdAsync(ownerId, ct);
    }

    public async Task<UserProfile?> UpdateAsync(OwnerId ownerId, string firstName, string lastName, string? phoneNumber, string? description, string? photoUrl, CancellationToken ct)
    {
        var profile = await _repository.GetByOwnerIdAsync(ownerId, ct);

        if (profile is null)
            return null;
        
        profile.Update(firstName, lastName, phoneNumber, description, photoUrl);
        await _repository.UpdateAsync(profile, ct);
        return profile;
    }
}
