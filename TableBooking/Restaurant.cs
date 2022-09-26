using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBooking
{
    public class Restaurant
    {
        private readonly List<Table> _tables = new List<Table>();
        public Restaurant()
        {
            for(int i = 0; i < 10; i++)
            {
                _tables.Add(new Table(i));
            }    
        }
        public void BookFreeTable(int countOfPersons)
        {
            Console.WriteLine("Добрый день! Подождите секунду, я подберу столик и подтвержу вашу бронь, оставайтесь на линни");
            var table = _tables.FirstOrDefault(t => t.SeatsCount > countOfPersons
                                                && t.State == State.Free);
            Thread.Sleep(1000 * 5);
            Console.WriteLine(table is null
                ? $"К сожалению, сейчас все столики заняты"
                : $"Готово! Ваш столик номер {table.Id}");
        }

        public void BookFreeTableAsync(int countOfPersons)
        {
            Console.WriteLine("Добрый день! Подождите секунду, я подберу столик и подтвержу вашу бронь, оставайтесь на линни");
            Task.Run(async () =>
            {
                var table = _tables.FirstOrDefault(t => t.SeatsCount > countOfPersons
                                                    && t.State == State.Free);
                await Task.Delay(1000 * 5);
                table?.SetState(State.Booked);

                Console.WriteLine(table is null
                    ? $"УВЕДОМЛЕНИЕ: К сожалению, сейчас все столики заняты"
                    : $"УВЕДОМЛЕНИЕ: Готово! Ваш столик номер {table.Id}");
            });
        }
    }
}
