var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("dbPostgres")
    .WithImage("postgres:16-alpine")
    .WithDataVolume()
    .AddDatabase("CommunityHub");

var api = builder.AddProject<Projects.CommunityHub_API>("api")
    .WithReference(db)
    .WaitFor(db)
    .WithEnvironment("Jwt__Issuer", builder.Configuration["Jwt:Issuer"])
    .WithEnvironment("Jwt__Audience", builder.Configuration["Jwt:Audience"])
    .WithEnvironment("Jwt__Key", builder.Configuration["Jwt:Key"])
    .WithEnvironment("Jwt__AccessTokenLifetimeMinutes", builder.Configuration["Jwt:AccessTokenLifetimeMinutes"])
    .WithEnvironment("Jwt__RefreshTokenLifetimeHours", builder.Configuration["Jwt:RefreshTokenLifetimeHours"])
    .WithEnvironment("ApiCredentials__Username", builder.Configuration["ApiCredentials:Username"])
    .WithEnvironment("ApiCredentials__Password", builder.Configuration["ApiCredentials:Password"]);

var gateway = builder.AddProject<Projects.Gateway_API>("gateway")
    .WithReference(api)
    .WaitFor(api)
    .WithEnvironment("CommunityServiceApi__ApiCredentials__Username", builder.Configuration["CommunityServiceApi:ApiCredentials:Username"])
    .WithEnvironment("CommunityServiceApi__ApiCredentials__Password", builder.Configuration["CommunityServiceApi:ApiCredentials:Password"])
    .WithEnvironment("CommunityServiceApi__Jwt__Issuer", builder.Configuration["CommunityServiceApi:Jwt:Issuer"])
    .WithEnvironment("CommunityServiceApi__Jwt__Audience", builder.Configuration["CommunityServiceApi:Jwt:Audience"])
    .WithEnvironment("CommunityServiceApi__Jwt__Key", builder.Configuration["CommunityServiceApi:Jwt:Key"])
    .WithEnvironment("CommunityServiceApi__Jwt__AccessTokenLifetimeMinutes", builder.Configuration["CommunityServiceApi:Jwt:AccessTokenLifetimeMinutes"])
    .WithEnvironment("CommunityServiceApi__Jwt__RefreshTokenLifetimeHours", builder.Configuration["CommunityServiceApi:Jwt:RefreshTokenLifetimeHours"])
    .WithEnvironment("CommunityServiceApi__BaseUrl", api.GetEndpoint("https"));

builder.Build().Run();
