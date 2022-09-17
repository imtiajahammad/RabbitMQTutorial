// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};
using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare("demo-queue",
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null
    );

var message = new { Name = "Producer", Message = "Hello" };
var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

channel.BasicPublish("", "demo-queue", null, body);



/*
https://www.youtube.com/watch?v=w84uFSwulBI&t=60s


DOCKER:
docker images

docker run -d --hostname my-rabbit --name ecomm-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management
    hostName: my-rabbit
    name: ecomm-rabbit
    ports we want to access outside 
    ports that are needed is one is 5672 that is the ports used by AMQP protocol
    and the other port is 15672 which is the port used by management console
docker logs -f 612 


 
 
 */