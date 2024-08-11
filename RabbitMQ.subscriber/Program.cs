using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factorty = new ConnectionFactory();

factorty.Uri = new Uri("amqp://guest:guest@localhost:5672");

using var connection = factorty.CreateConnection();

var channel = connection.CreateModel();  

channel.BasicQos(0, 1, false);

var consumer = new EventingBasicConsumer(channel);

var queueName = "direct-queue-Critical";

channel.BasicConsume(queueName, false, consumer);

Console.WriteLine("Loglar dinleniyor...");

consumer.Received += (sender, eventArgs) =>
{
    var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

    Console.WriteLine("Gelen Mesaj: " + message); 

    File.AppendAllText("log-critical.txt", message + Environment.NewLine);
    channel.BasicAck(eventArgs.DeliveryTag, false);
};
 

Console.ReadLine(); 