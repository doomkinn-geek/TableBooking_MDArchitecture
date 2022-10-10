using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TableBooking
{
    public class Restaurant
    {
        private System.Timers.Timer StateTimer;
        private readonly List<Table> _tables = new List<Table>();
        public Restaurant()
        {
            for(int i = 0; i < 10; i++)
            {
                _tables.Add(new Table(i));
            }

            StateTimer = new System.Timers.Timer(20000);
            // Hook up the Elapsed event for the timer. 
            StateTimer.Elapsed += OnTimedEvent;
            StateTimer.AutoReset = true;
            StateTimer.Enabled = true;
        }
        public void BookFreeTable(int countOfPersons)
        {
            Notifications.WaitForAnswer();
            var table = _tables.FirstOrDefault(t => t.SeatsCount > countOfPersons
                                                && t.State == State.Free);
            Thread.Sleep(1000 * 5);
            Notifications.AnswerForTable(table);
        }

        public void BookFreeTableAsync(int countOfPersons)
        {
            Notifications.WaitForAnswer();
            Task.Run(async () =>
            {
                var table = _tables.FirstOrDefault(t => t.SeatsCount > countOfPersons
                                                    && t.State == State.Free);
                await Task.Delay(1000 * 5);
                table?.SetState(State.Booked);

                Notifications.AnswerForTable(table);
            });
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            foreach(var table in _tables)
            {
                table.SetState(State.Free);
            }
        }
    }
}
