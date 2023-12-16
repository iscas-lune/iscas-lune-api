namespace iscaslune.Api;

public static class EnvironmentVariable
{
    public static string GetVariable(string key)
    {
        return Environment.GetEnvironmentVariable(key) ??
            throw new ArgumentException("Variável não encontrada");
    }
}
