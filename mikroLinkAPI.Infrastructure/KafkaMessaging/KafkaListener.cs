using mikroLinkAPI.Domain.Interfaces.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mikroLinkAPI.Infrastructure.KafkaMessaging
{
    public class KafkaListener(IKafkaConsumerService kafkaConsumer)
    {
        private readonly IKafkaConsumerService _kafkaConsumer = kafkaConsumer;

        public void Baslat(CancellationToken cancellationToken)
        {
            _kafkaConsumer.StartListening("company-topic", message =>
            {
                Console.WriteLine($"Kafka'dan mesaj alındı: {message}");
            }, cancellationToken);
        }
    }

}
