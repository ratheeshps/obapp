using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace obapp.Models
{
    public class FundTransferModel
    {
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public decimal Amount { get; set; }
        public List<SelectListItem> FromAccounts { get; set; }
        public List<SelectListItem> ToAccounts { get; set; }
    }
}
