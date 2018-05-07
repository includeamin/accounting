using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingServer
{
    public class AccountLogs
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public bool Paid { get; set; }


    }
}
