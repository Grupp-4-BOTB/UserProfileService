using Microsoft.EntityFrameworkCore;
using UserProfileService.Domain.Entities;
using UserProfileService.Domain.ValueObjects;

namespace UserProfileService.Infrastructure.Persistance.Repositories;

public class UserProfileRepository
{
    private readonly UserProfileDbContext _context;

    public UserProfileRepository(UserProfileDbContext context)
    {
        _context = context;
    }

    public async Task<UserProfile?> GetByOwnerIdAsync(OwnerId ownerId, CancellationToken ct)
    {
        return await _context.UserProfiles
            .FirstOrDefaultAsync(p => p.OwnerId == ownerId, ct);
    }

    public async Task AddAsync(UserProfile profile, CancellationToken ct)
    {
        await _context.UserProfiles.AddAsync(profile, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(UserProfile profile, CancellationToken ct)
    {
        _context.UserProfiles.Update(profile);
        await _context.SaveChangesAsync(ct);
    }
}
