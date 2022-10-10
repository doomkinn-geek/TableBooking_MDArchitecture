using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBooking
{
    public static class Notifications
    {
        public static void Greetings()
        {
            Console.WriteLine("Привет! Желаете забронировать столик?\n1 - мы уведомим Вас по смс (асинхронно)" +
                                    "\n2 - подождите на линии, мы Вас оповестим (синхронно)");
        }

        public static void Thanks()
        {
            Console.WriteLine("Спасибо за ваше обращение!");
        }

        public static void WaitForAnswer()
        {
            Console.WriteLine("Добрый день! Подождите секунду, я подберу столик и подтвержу вашу бронь, оставайтесь на линни");
        }

        public static void AnswerForTable(Table table)
        {
            Console.WriteLine(table is null
                ? $"К сожалению, сейчас все столики заняты"
                : $"Готово! Ваш столик номер {table.Id}");
        }
    }
}
