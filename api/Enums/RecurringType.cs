using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Enums
{
    public enum RecurringType
    {
        //Bill can change value each billing period (usually does)
        //Subscription is the same each billing period
        Bill,
        Subscription
    }
}
