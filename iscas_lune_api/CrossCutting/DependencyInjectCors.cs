namespace iscaslune.Api.CrossCutting;

public static class DependencyInjectCors
{
    public static void InjectCors(this IServiceCollection services)
    {
        var origin = EnvironmentVariable.GetVariable("URL_BASE_FRONT");
        var originAdmin = EnvironmentVariable.GetVariable("URL_BASE_FRONT_ADMIN");
        services.AddCors(options =>
        {
            options.AddPolicy(name: "iscasluneorigin",
                              policy =>
                              {
                                  policy.WithOrigins(origin)
                                      .WithMethods("GET")
                                      .AllowAnyHeader();
                              });
            options.AddPolicy(name: "iscasluneoriginwithdelte",
                              policy =>
                              {
                                  policy.WithOrigins(origin)
                                      .WithMethods("DELETE")
                                      .AllowAnyHeader();
                              });
            options.AddPolicy(name: "iscasluneoriginwithpost",
                              policy =>
                              {
                                  policy.WithOrigins(origin)
                                      .WithMethods("POST", "PUT")
                                      .AllowAnyHeader();
                              });

            options.AddPolicy(name: "iscasluneoriginadmin",
                                  policy =>
                                  {
                                      policy.WithOrigins(originAdmin)
                                          .AllowAnyMethod()
                                          .AllowAnyHeader();
                                  });
        });
    }
}
