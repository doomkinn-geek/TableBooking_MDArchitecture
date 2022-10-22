using System;
using System.Threading.Tasks;
using MassTransit;
using Messaging.InMemory;
using Microsoft.Extensions.Logging;
using Restaraunt.Booking;
using Restaurant.Messages;

namespace Restaurant.Booking.Consumers
{
    public class BookingRequestConsumer : IConsumer<IBookingRequest>
    {
        private readonly ILogger _logger;
        private readonly IInMemoryRepository<IBookingRequest> _repository;
        private readonly Restaurant _restaurant;

        public BookingRequestConsumer(Restaurant restaurant,
            IInMemoryRepository<IBookingRequest> repository,
            ILogger<BookingRequestConsumer> logger)
        {
            _restaurant = restaurant;
            _repository = repository;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IBookingRequest> context)
        {
            _logger.Log(LogLevel.Information, $"[OrderId: {context.Message.OrderId}]");

            var savedMessage = _repository.Get()
                .FirstOrDefault(m => m.OrderId == context.Message.OrderId);

            if (savedMessage is null)
            {
                _logger.Log(LogLevel.Debug, "First time message");
                _repository.AddOrUpdate(context.Message);
                var result = await _restaurant.BookFreeTableAsync(1);
                await context.Publish<ITableBooked>(new TableBooked(context.Message.OrderId, result ?? false));
                return;
            }

            _logger.Log(LogLevel.Debug, "Second time message");
        }
    }
}