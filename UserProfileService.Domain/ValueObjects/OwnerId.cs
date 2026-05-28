namespace UserProfileService.Domain.ValueObjects;

public record OwnerId(string Value)
{
    public static OwnerId From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("OwnerId cannot be null or empty.", nameof(value));

        return new OwnerId(value);
    }
}
