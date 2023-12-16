namespace iscaslune.Api.CrossCutting;

public static class DependencyInjectCors
{
    public static void InjectCors(this IServiceCollection services)
    {
        var origin = EnvironmentVariable.GetVariable("URL_BASE_FRONT");
        services.AddCors(options =>
        {
            options.AddPolicy(name: "iscasluneorigin",
                              policy =>
                              {
                                  policy.WithOrigins(origin)
                                      .WithMethods("GET")
                                      .AllowAnyHeader();
                              });
        });
    }
}
