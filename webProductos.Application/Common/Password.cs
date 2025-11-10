namespace webProductos.Application.Common;

public sealed class Password
{
    private readonly string _raw;
    private readonly string _storedHash;

    public Password(string raw, string storedHash = null)
    {
        _raw = raw;
        _storedHash = storedHash;
    }

    public string Hash()
    {
        return BCrypt.Net.BCrypt.HashPassword(_raw);
    }

    public bool VerifyHash()
    {
        if (string.IsNullOrWhiteSpace(_storedHash)) return false;
        return BCrypt.Net.BCrypt.Verify(_raw, _storedHash);
    }
}