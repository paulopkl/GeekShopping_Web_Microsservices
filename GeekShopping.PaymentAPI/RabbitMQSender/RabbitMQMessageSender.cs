using GeekShopping.MessageBus;
using System.Text.Json;
using RabbitMQ.Client;
using System.Text;
using GeekShopping.PaymentAPI.Messages;

namespace GeekShopping.PaymentAPI.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _username;
        private IConnection _connection;
        private const string ExchangeName = "DirectPaymentUpdateExchange";
        private const string PaymentEmailUpdateQueueName = "PaymentEmailUpdateQueueName";
        private const string PaymentOrderUpdateQueueName = "PaymentOrderUpdateQueueName";

        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _username = "guest";
            _password = "guest";
        }

        public void SendMessage(BaseMessage message)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();

                channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, durable: false);

                channel.QueueDeclare(PaymentEmailUpdateQueueName, false, false, false, null);
                channel.QueueDeclare(PaymentOrderUpdateQueueName, false, false, false, null);

                string emailRoutingKey = "PaymentEmail";
                channel.QueueBind(PaymentEmailUpdateQueueName, ExchangeName, emailRoutingKey);

                string orderRoutingKey = "PaymentOrder";
                channel.QueueBind(PaymentOrderUpdateQueueName, ExchangeName, orderRoutingKey);

                byte[] body = GetMessageAsByteArray(message);

                channel.BasicPublish(exchange: ExchangeName, emailRoutingKey, basicProperties: null, body: body);
                channel.BasicPublish(exchange: ExchangeName, orderRoutingKey, basicProperties: null, body: body);
            }
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize<UpdatePaymentResultMessage>((UpdatePaymentResultMessage) message, options);

            var body = Encoding.UTF8.GetBytes(json);

            return body;
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    UserName = _username,
                    Password = _password
                };

                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null) return true;

            CreateConnection();

            return _connection != null;
        }
    }
}
