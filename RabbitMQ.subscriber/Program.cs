using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factorty = new ConnectionFactory();

factorty.Uri = new Uri("amqp://guest:guest@localhost:5672");

using var connection = factorty.CreateConnection();

var channel = connection.CreateModel();

// channel.QueueDeclare("hello-queue", true, false, false);

var consumer = new EventingBasicConsumer(channel);

channel.BasicConsume("hello-queue", true, consumer);

consumer.Received += (sender, eventArgs) =>
{
    var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
    Console.WriteLine("Gelen Mesaj: " + message); 
};
 

Console.ReadLine();


