using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBooking
{
    public class Table
    {
        private readonly object _lock = new object();
        public State State { get; private set; }
        public int SeatsCount { get; }
        public int Id { get; }
        public Table(int id)
        {
            Id = id;
            State = State.Free;
            SeatsCount = new Random().Next(2, 5);            
        }
        public bool SetState(State state)
        {
            lock (_lock)
            {
                if (state == State)
                    return false;

                State = state;
                return true;
            }
        }        
    }
}
