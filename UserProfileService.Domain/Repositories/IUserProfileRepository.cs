using UserProfileService.Domain.Entities;
using UserProfileService.Domain.ValueObjects;

namespace UserProfileService.Domain.Repositories;

public interface IUserProfileRepository
{
    Task<UserProfile?> GetByOwnerIdAsync(OwnerId ownerId, CancellationToken ct);
    Task AddAsync(UserProfile profile, CancellationToken ct);
    Task UpdateAsync(UserProfile profile, CancellationToken ct);
}
