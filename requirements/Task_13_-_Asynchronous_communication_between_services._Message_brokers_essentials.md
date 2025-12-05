### Description

In this task, you’ll introduce **asynchronous, event-driven communication** between services using **RabbitMQ**.  
The goal is to decouple services and allow background processing without blocking the main API.  

Let's implement 2 events and their processing.
1) Add a new microservice (not API, but of Worker type) called **History Service**, which has its own database and listens for update events from other services.
   Whenever any existing service performs an **update** (e.g., update entity, update user, update product — whatever your domain supports — just choose one 'update' endpoint), it should **publish a message** to RabbitMQ. 
   The History Service should subscribe to the message queue and write a record to its own database.
2) For one entity, implement clearing all the references to this entity in other microservices. Example: I have an entity and in user service, there is a database to store the user-to-entity relations. If entity service deletes an entity, publish a message that will be received by user service, so that user service deletes the relations to this entity.

This task teaches message brokers, exchanges, queues, routing, consumer workers, and eventual consistency.

#### Steps to implement:

Add an instance of RabbitMq. For now, use docker. The suggested commands are
```
docker run -d -p 15672:15672 -p 5672:5672 --name rabbitmq rabbitmq:3-management

#then, in the console of docker container

rabbitmqctl add_user user user
rabbitmqctl set_user_tags user administrator
rabbitmqctl set_permissions -p / user ".*" ".*" ".*"
```

Then, you need to add to queues to RabbitMq. One for your history records and one for an entity deletion. Suggested commands are (run in the docker container console)
```
rabbitmqadmin declare queue --vhost=/ name=OnHistoryRecordAdded durable=true -u user -p user
rabbitmqadmin declare queue --vhost=/ name=OnMyEntityDeleted durable=true -u user -p user
```

 Then, you'll need to create exchanges to publish your messages to. It's better not to publish messages in the queue directly - by having an exchange, you support multiple subscribers for the same event. Suggested commands are (run in the docker container console)
```
rabbitmqadmin declare exchange --vhost=/ name=AddHistoryRecord type=direct -u user -p user
rabbitmqadmin declare exchange --vhost=/ name=DeleteEntity type=direct -u user -p user
```

Then, bind created exchanges to the queues. After that, any messages published to the exchange, will be redirected to the corresponding queue. Suggested commands are (run in the docker container console)
```
rabbitmqadmin --vhost=/ declare binding source=AddHistoryRecord destination_type=queue destination=OnHistoryRecordAdded -u user -p user
rabbitmqadmin --vhost=/ declare binding source=DeleteEntity destination_type=queue destination=OnMyEntityDeleted -u user -p user
```

Now your RabbitMq is set up and ready to receive first messages.

Let's start with the entity deletion.
To add an event listener, you need to add a hosted service to your API service (whatever service that needs to listen to the queue)
```c#
builder.Services.AddHostedService<RabbitMqListener>();

public class RabbitMqListener : BackgroundService  
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)  
    {
    }
}
```

Then, install the `RabbitMQ.Client` package and implement the basic subscription

```c#
using System.Text;  
using RabbitMQ.Client;  
using RabbitMQ.Client.Events;  
  
namespace WebApplication1;  
  
public class RabbitMqListener : BackgroundService  
{  
    private readonly ConnectionFactory _factory;  
    private IConnection _connection;  
    private IChannel _channel;  
    
    private readonly ILogger<RabbitMqListener> _logger;
    
    public RabbitMqListener(ILogger<RabbitMqListener> logger)  
    {        
	    _logger = logger;  
        _factory = new ConnectionFactory  
        {  
            HostName = "localhost",  
            UserName = "user",  
            Password = "user",  
            VirtualHost = "/",  
            RequestedHeartbeat = ConnectionFactory.DefaultHeartbeat  
        };  
    }
    
    protected override async Task ExecuteAsync(
	    CancellationToken stoppingToken
    )  
    {        
	    _connection = await _factory.CreateConnectionAsync(stoppingToken);  
        _channel = await _connection.CreateChannelAsync(
	        cancellationToken: stoppingToken
	    );  
	    
        var consumer = new AsyncEventingBasicConsumer(_channel);  
        
        consumer.ReceivedAsync += async (_, @event) =>  
        {  
            var msg = Encoding.UTF8.GetString(@event.Body.ToArray());  
            
            _logger.LogInformation("Recieved message: " + msg);  
            
            await _channel.BasicAckAsync(
	            @event.DeliveryTag,
	            multiple: false,
	            cancellationToken: stoppingToken
	        );  
        };  
        
        await _channel.BasicConsumeAsync(  
            queue: "OnMyEntityDeleted",  
            autoAck: false,  
            consumer: consumer,  
            cancellationToken: stoppingToken  
        );    
    }
}
```

In the constructor, the connection factory is created. **Move all the connection details to appsettings!** ExecuteAsync method now creates a connection and a channel between your service and rabbitmq. It adds the receiver for a specified queue by it's name. **Name should be also configurable by apppsettings!** For now, it's just logging of a message. The logging row should be replaced with an actual implementation - you should delete the necessary entities there.

Let's test if it worked. Open the RabbitMq admin panel (by default, http://localhost:15672/) and navigate exchanges tab. Choose the "OnMyEntityDeleted" exchange. You'll see the ppage with the statistics of usage and "Publish message" section. Enter some text and click 'Publish message'. The message should be sent to the queue and processed by your service - you should see a log now.

This code is not a final version! Let's improve it
- find a way to add multiple consumers, for the case if you need to listen to multiple queues
- it's not a responsibility of a `RabbitMqListener` to process all the logic behind your events. Instead, create some `MyEventProcessor` service and call it instead. Remember, you'll have multiple Processors, so find a way to call of them for the corresponding queues.
- let's add a convenient way to register all of your processors. Ideally, your startup should look like this: `builder.Services.AddRabbitMqEventProcessor([queueNameFromAppsettings], [ProcessorClass])` (or use any other applicable approach).

Find a way to **publish** a message by yourself. Make sure you published the message when an entity is deleted. This message should contain an Id of the deleted entity as a payload.

When the deletion logic is completed, move to the next event - History record.
Usually, you don't have event listeners directly in your API services. Instead, they create multiple Worker services for such purposes . Let's create one and try it.

Add a separate folder for your History service. It will contain Worker itself and contracts project. Use Worker template provided by dotnet by default. You'll see the program file like that:
```c#
var builder = Host.CreateApplicationBuilder(args);  
builder.Services.AddHostedService<Worker>();  
  
var host = builder.Build();  
host.Run();
```
Almost the same thing you had before for your API services - but no controllers now!
Add dbContext, entities and migrations the same way you did it before. Your History record should contain such fields as "HistoryEventType" (or just "Type"), "Date", "Payload" (for any useful attached information) and "TriggeredBy" to track the source of the event.
Basically, we want to collect some history of important actions happened in our application in this service. It will listen to the queue of incoming history events and save them in the database.
For any 'update' endpoint in your services, publish a message to the 'AddHistoryRecord' exchange. It should contain type, date, issuer identifier (just name of the service) and payload (use entity id as a payload). To send, first, serialise it to the string. Then, deserialize it in your History service, map to entity and save. The model that History service uses should be in its Contracts project and published to nuget - the same way you used for the other services.

Optionally, find a way to set up your rabbitmq and history service using Aspire. Make sure it sets up all the necessary queues and exchanges, so that a developer doesn't need to do it manually, or find a way to configure them automatically.
