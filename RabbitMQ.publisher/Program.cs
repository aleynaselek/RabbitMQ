using RabbitMQ.Client;
using System.Text;

var factorty = new ConnectionFactory();
 
factorty.Uri = new Uri("amqp://guest:guest@localhost:5672");

using var connection = factorty.CreateConnection();

var channel = connection.CreateModel();

channel.QueueDeclare("hello-queue", true, false, false);

string message = "Hello World!";

var messageBody = Encoding.UTF8.GetBytes(message);

channel.BasicPublish("", "hello-queue", null, messageBody);

Console.WriteLine("Mesaj gönderilmiştir.");

Console.ReadLine();


