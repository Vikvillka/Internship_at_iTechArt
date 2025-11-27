var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddPostgres("dbPostgres")
    .WithImage("postgres:16-alpine")
    .WithDataVolume()
    .AddDatabase("CommunityHub");

var dbUser = builder.AddPostgres("dbUserService")
    .WithImage("postgres:16-alpine")
    .WithDataVolume()
    .AddDatabase("UserService");

var dbHistory = builder.AddPostgres("dbHistoryService")
    .WithImage("postgres:16-alpine")
    .WithDataVolume()
    .AddDatabase("HistoryService");

var rabbitmq = builder.AddRabbitMQ("messaging")
    .WithManagementPlugin()   
    .WithDataVolume();

var redis = builder.AddRedis("redis")
    .WithHostPort(6379)
    .WithRedisInsight();

var apiCommunity = builder.AddProject<Projects.CommunityHub_API>("apiCommunity")
    .WithReference(db)
    .WaitFor(db)
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq)
    .WithEnvironment("Jwt__Issuer", builder.Configuration["Jwt:Issuer"])
    .WithEnvironment("Jwt__Audience", builder.Configuration["Jwt:Audience"])
    .WithEnvironment("Jwt__Key", builder.Configuration["Jwt:Key"])
    .WithEnvironment("Jwt__AccessTokenLifetimeMinutes", builder.Configuration["Jwt:AccessTokenLifetimeMinutes"])
    .WithEnvironment("Jwt__RefreshTokenLifetimeHours", builder.Configuration["Jwt:RefreshTokenLifetimeHours"])
    .WithEnvironment("ApiCredentials__Username", builder.Configuration["ApiCredentials:Username"])
    .WithEnvironment("ApiCredentials__Password", builder.Configuration["ApiCredentials:Password"])
    .WithEnvironment("RabbitMq__DeleteEntityExchange", builder.Configuration["RabbitMq:DeleteEntityExchange"])
    .WithEnvironment("RabbitMq__HistoryExchange", builder.Configuration["RabbitMq:HistoryExchange"]);

var apiUser = builder.AddProject<Projects.UserService_GRpc>("apiUser")
    .WithReference(dbUser)
    .WaitFor(dbUser)
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq)
    .WithReference(redis)
    .WithEnvironment("IsRunOnAspire", "true")
    .WithEnvironment("Jwt__Issuer", builder.Configuration["Jwt:Issuer"])
    .WithEnvironment("Jwt__Audience", builder.Configuration["Jwt:Audience"])
    .WithEnvironment("Jwt__Key", builder.Configuration["Jwt:Key"])
    .WithEnvironment("Jwt__AccessTokenLifetimeMinutes", builder.Configuration["Jwt:AccessTokenLifetimeMinutes"])
    .WithEnvironment("Jwt__RefreshTokenLifetimeHours", builder.Configuration["Jwt:RefreshTokenLifetimeHours"])
    .WithEnvironment("ApiCredentials__Username", builder.Configuration["ApiCredentials:Username"])
    .WithEnvironment("ApiCredentials__Password", builder.Configuration["ApiCredentials:Password"])
    .WithEnvironment("RabbitMq__Queues", builder.Configuration["RabbitMq:Queues:DeleteEntityQueue"]);

var gateway = builder.AddProject<Projects.Gateway_API>("gateway")
    .WithReference(apiCommunity)
    .WithReference(apiUser)
    .WithReference(redis)
    .WaitFor(apiCommunity)
    .WaitFor(apiUser)
    .WaitFor(redis)
    .WithEnvironment("IsRunOnAspire", "true")
    .WithEnvironment("CommunityServiceApi__ApiCredentials__Username", builder.Configuration["CommunityServiceApi:ApiCredentials:Username"])
    .WithEnvironment("CommunityServiceApi__ApiCredentials__Password", builder.Configuration["CommunityServiceApi:ApiCredentials:Password"])
    .WithEnvironment("CommunityServiceApi__Jwt__Issuer", builder.Configuration["CommunityServiceApi:Jwt:Issuer"])
    .WithEnvironment("CommunityServiceApi__Jwt__Audience", builder.Configuration["CommunityServiceApi:Jwt:Audience"])
    .WithEnvironment("CommunityServiceApi__Jwt__Key", builder.Configuration["CommunityServiceApi:Jwt:Key"])
    .WithEnvironment("CommunityServiceApi__Jwt__AccessTokenLifetimeMinutes", builder.Configuration["CommunityServiceApi:Jwt:AccessTokenLifetimeMinutes"])
    .WithEnvironment("CommunityServiceApi__Jwt__RefreshTokenLifetimeHours", builder.Configuration["CommunityServiceApi:Jwt:RefreshTokenLifetimeHours"])
    .WithEnvironment("CommunityServiceApi__BaseUrl", apiCommunity.GetEndpoint("https"))
    .WithEnvironment("UserServiceApi__BaseUrl", apiUser.GetEndpoint("https"));

var historyWorker = builder.AddProject<Projects.HistoryService_Worker>("history-worker")
    .WithReference(dbHistory)
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq)
    .WithEnvironment("RabbitMq__HistoryQueue", "OnHistoryRecordAdded");

builder.Build().Run();
