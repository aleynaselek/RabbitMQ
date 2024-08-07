using RabbitMQ.Client;
using System.Text;

var factorty = new ConnectionFactory();
 
factorty.Uri = new Uri("amqp://guest:guest@localhost:5672");

using var connection = factorty.CreateConnection();

var channel = connection.CreateModel();

channel.ExchangeDeclare("logs-fanout", ExchangeType.Fanout,true); 

Enumerable.Range(1, 50).ToList().ForEach(x =>
{
    string message = $"log {x}";

    var messageBody = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish("logs-fanout", "", null, messageBody);

    Console.WriteLine($"Mesaj gönderilmiştir: {message}");

}); 

Console.ReadLine();