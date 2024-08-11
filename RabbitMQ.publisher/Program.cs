using RabbitMQ.Client;
using Shared;
using System.Text;
using System.Text.Json;

var factorty = new ConnectionFactory();
 
factorty.Uri = new Uri("amqp://guest:guest@localhost:5672");

using var connection = factorty.CreateConnection();

var channel = connection.CreateModel();

channel.ExchangeDeclare("header-exchange", ExchangeType.Headers,true);  
 
Dictionary<string, object> headers = new Dictionary<string, object>();
 
headers.Add("format", "pdf");
headers.Add("shape", "a4");
var properties = channel.CreateBasicProperties();
properties.Headers = headers;
properties.Persistent = true; // Mesajların kalıcı olması için


var product = new Product { Id = 5, Name = "Kalem", Price = 35, Stock = 100 };

var  productJsonString = JsonSerializer.Serialize(product);

channel.BasicPublish("header-exchange", string.Empty, properties, Encoding.UTF8.GetBytes(productJsonString));

Console.WriteLine("Mesaj gönderilmiştir.");

public enum LogNames
{
    Critical = 1,
    Error = 2,
    Warning = 3,
    Info = 4
}