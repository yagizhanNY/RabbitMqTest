using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace PublisherConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "date",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                while(true)
                {
                    string message = DateTime.Now.ToString();
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "date",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);

                    Thread.Sleep(100);
                }
                
            }

            /*Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();*/
        }
    }
}
