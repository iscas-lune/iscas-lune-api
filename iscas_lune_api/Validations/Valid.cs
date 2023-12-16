namespace iscaslune.Api.Validations;

public static class Valid
{
    public static void ValidStringSemLength(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));
    }
}
