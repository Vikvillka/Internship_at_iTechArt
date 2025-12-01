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
    .WithEnvironment("Jwt__Key", builder.Configuration["Jwt:Key"]);

var apiUser = builder.AddProject<Projects.UserService_GRpc>("apiUser")
    .WithReference(dbUser)
    .WaitFor(dbUser)
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq)
    .WithReference(redis)
    .WithEnvironment("IsRunOnAspire", "true")
    .WithEnvironment("Jwt__Key", builder.Configuration["Jwt:Key"]);

var gateway = builder.AddProject<Projects.Gateway_API>("gateway")
    .WithReference(apiCommunity)
    .WithReference(apiUser)
    .WithReference(redis)
    .WaitFor(apiCommunity)
    .WaitFor(apiUser)
    .WaitFor(redis)
    .WithEnvironment("IsRunOnAspire", "true")
    .WithEnvironment("Jwt__Key", builder.Configuration["Jwt:Key"]);

var historyWorker = builder.AddProject<Projects.HistoryService_Worker>("history-worker")
    .WithReference(dbHistory)
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq)
    .WithEnvironment("RabbitMq__HistoryQueue", "OnHistoryRecordAdded");

builder.Build().Run();
