using System.Globalization;
using UserProfileService.Domain.ValueObjects;

namespace UserProfileService.Domain.Entities;

public class UserProfile
{
    public Guid Id { get; private set; }
    public OwnerId OwnerId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? Description { get; private set; }
    public string? PhotoUrl { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private UserProfile() { }


    public static UserProfile Create (OwnerId ownerId, string firstName, string lastName, string? phoneNumber = null, string? description = null, string? photoUrl = null)
    {
        return new UserProfile
        {
            Id = Guid.NewGuid(),
            OwnerId = ownerId,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Description = description,
            PhotoUrl = photoUrl,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public void Update(string firstName, string lastName, string? phoneNumber = null, string? description = null, string? photoUrl = null)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Description = description;
        PhotoUrl = photoUrl;
        UpdatedAt = DateTime.UtcNow;
    }
}
