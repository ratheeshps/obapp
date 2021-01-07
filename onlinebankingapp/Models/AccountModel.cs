using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace obapp.Models
{
    public class CustomerModel
    {
        public string CustomerId { get; set; }
    }
    public class AccountModel
    {
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string AccountName { get; set; }
        public string LedgeCode { get; set; }
        public string CustomerID { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyName { get; set; }
    }
    public class BeneficiaryModel
    {
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAccount { get; set; }
    }
}
