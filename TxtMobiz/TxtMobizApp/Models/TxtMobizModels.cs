using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TxtMobizApp.Models
{
        

    public class AccountModel
    {
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string EmailAddress { get; set; }
        public string WebsiteUrl { get; set; }
        public string Address_Composite { get; set; }

        public decimal Revenue { get; set; }

    }

    public class AccountViewModel
    {
        public List<AccountModel> Accounts { get; set; }

        public List<string> Parameters { get; set; }
    }
}