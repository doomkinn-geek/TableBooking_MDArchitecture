﻿susing System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBooking
{
    public enum State
    {
        ///<summary>
        ///Стол свободен
        ///</summary>
        Free = 0,

        ///<summary>
        ///Стол занят
        ///</summary>
        Booked = 1
    }
}
