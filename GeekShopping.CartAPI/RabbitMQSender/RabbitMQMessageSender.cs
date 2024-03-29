﻿using GeekShopping.MessageBus;
using System.Text.Json;
using RabbitMQ.Client;
using GeekShopping.CartAPI.Messages;
using System.Text;

namespace GeekShopping.CartAPI.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _username;
        private IConnection _connection;

        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _username = "guest";
            _password = "guest";
        }

        public void SendMessage(BaseMessage message, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _username,
                Password = _password
            };

            _connection = factory.CreateConnection();

            using var channel = _connection.CreateModel();

            channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);

            byte[] body = GetMessageAsByteArray(message);

            channel.BasicPublish(
                exchange: "", 
                routingKey: queueName, 
                basicProperties: null, 
                body: body
            );
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize<CheckoutHeaderVO>((CheckoutHeaderVO) message, options);

            var body = Encoding.UTF8.GetBytes(json);

            return body;
        }
    }
}
