using Messaging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly Consumer _consumer;

        public Worker()
        {
            //����� ����� ��� ������� ���������
            _consumer = new Consumer("BookingNotification", "localhost");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Receive((sender, args) =>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body); //����������
                Console.WriteLine(" [x] Received {0}", message);
            });
        }
    }
}
