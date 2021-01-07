using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace obapp.Models
{
    public class FundTransferModel
    {
        [Required]
        public string FromAccount { get; set; }
        public string CustomerId { get; set; }
        [Required]
        public string ToAccount { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public List<SelectListItem> FromAccounts { get; set; }
        public List<SelectListItem> ToAccounts { get; set; }
    }
    public class FundTransferResponse
    {
        public string ReferenceNumber { get; set; }
        public string ValueDate { get; set; }
    }
}
