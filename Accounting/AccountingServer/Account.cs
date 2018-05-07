using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingServer
{
    public class Account
    {
        public int Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<AccountLogs> AccountLogs { get; set; }

    }
}
