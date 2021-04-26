using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class DateOffsetBalance
    {
        public DateTime Date { get; set; }
        public double Balance { get; set; }

        public DateOffsetBalance(DateTime date, double balance)
        {
            Date = date;
            Balance = balance;
        }
    }
}
