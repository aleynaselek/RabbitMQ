using RabbitMQ.Client;
using System.Text;

var factorty = new ConnectionFactory();
 
factorty.Uri = new Uri("amqp://guest:guest@localhost:5672");

using var connection = factorty.CreateConnection();

var channel = connection.CreateModel();

channel.ExchangeDeclare("logs-direct", ExchangeType.Direct,true); 

Enum.GetNames(typeof(LogNames)).ToList().ForEach(x =>
{
    var routeKey = $"route-{x}";
    var queueName = $"direct-queue-{x}";
    channel.QueueDeclare(queueName, true, false, false);
    channel.QueueBind(queueName, "logs-direct", routeKey, null);
});

Enumerable.Range(1, 50).ToList().ForEach(x =>
{
    LogNames log = (LogNames)new Random().Next(1, 5);

    string message = $"log-type: {log}";

    var messageBody = Encoding.UTF8.GetBytes(message);

    var routeKey = $"route-{log}";

    channel.BasicPublish("logs-direct", routeKey, null, messageBody);

    Console.WriteLine($"Log gönderilmiştir: {message}");

}); 

Console.ReadLine();

public enum LogNames
{
    Critical = 1,
    Error = 2,
    Warning = 3,
    Info = 4
}