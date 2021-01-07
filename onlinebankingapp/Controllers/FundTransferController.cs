using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using obapp.Models;
using onlinebankingapp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace obapp.Controllers
{
    public class FundTransferController : Controller
    {
        private readonly IOptions<ServiceConnection> _serviceConnection;
        public IApiWrapper wrapper { get; }
        public FundTransferController(IOptions<ServiceConnection> serviceConnection, IApiWrapper apiWrapper)
        {
            _serviceConnection = serviceConnection;
            this.wrapper = apiWrapper;
        }
        public IActionResult Index()
        {
            FundTransferModel model = new FundTransferModel();
            model.FromAccounts = GetFromAccounts();
            model.ToAccounts = GetBeneficiaries();

            return View(model);
        }
        [HttpPost]
        public IActionResult Index(FundTransferModel model)
        {
            if (ModelState.IsValid)
            {
                string result = string.Empty;
                model.CustomerId = "1";
                var response = wrapper.PostAPI(HttpContext.Session.GetString("Token"),
                                            _serviceConnection.Value.EndPoint + "fundtransfer",
                                            JsonConvert.SerializeObject(model)).GetAwaiter().GetResult();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (HttpContent content = response.Content)
                    {
                        Task<string> tempResult = content.ReadAsStringAsync();
                        result = tempResult.Result;
                    }
                    ApiResponse<FundTransferResponse> fundResponse = JsonConvert.DeserializeObject<ApiResponse<FundTransferResponse>>(result.ToString());
                    TempData["Response"] = fundResponse;
                    return RedirectToAction("status");
                }
            }


            //model.FromAccounts = GetFromAccounts();
            //model.ToAccounts = GetBeneficiaries();

            return View(model);
        }
        public IActionResult Status()
        {
            return View();
        }
        private List<SelectListItem> GetFromAccounts() {
            CustomerModel customerModel = new CustomerModel();
            List<SelectListItem> fromAccounts = new List<SelectListItem>();
            customerModel.CustomerId = HttpContext.Session.GetString("CustomerId").ToString();
            string result = string.Empty;
            var response = wrapper.PostAPI(HttpContext.Session.GetString("Token"),
                                        _serviceConnection.Value.EndPoint + "accounts/GetCustomerAccount",
                                        JsonConvert.SerializeObject(customerModel)).GetAwaiter().GetResult();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (HttpContent content = response.Content)
                {
                    Task<string> tempResult = content.ReadAsStringAsync();
                    result = tempResult.Result;
                }
                ApiResponse<List<AccountModel>> accountData = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<List<AccountModel>>>(result.ToString());
                foreach(var item in accountData.Data)
                {
                    SelectListItem listItem = new SelectListItem();
                    listItem.Text = item.AccountNumber + item.AccountName + " - (" + item.AccountType + ")";
                    listItem.Value = item.AccountNumber;
                    fromAccounts.Add(listItem);
                }
                return fromAccounts;
            }
            return null;
        }

        private List<SelectListItem> GetBeneficiaries() {
            CustomerModel customerModel = new CustomerModel();
            List<SelectListItem> benList = new List<SelectListItem>();
            customerModel.CustomerId = HttpContext.Session.GetString("CustomerId").ToString();
            string result = string.Empty;
            var response = wrapper.PostAPI(HttpContext.Session.GetString("Token"),
                                        _serviceConnection.Value.EndPoint + "beneficiary",
                                        JsonConvert.SerializeObject(customerModel)).GetAwaiter().GetResult();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (HttpContent content = response.Content)
                {
                    Task<string> tempResult = content.ReadAsStringAsync();
                    result = tempResult.Result;
                }
                ApiResponse<List<BeneficiaryModel>> accountData = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<List<BeneficiaryModel>>>(result.ToString());
                foreach (var item in accountData.Data)
                {
                    SelectListItem listItem = new SelectListItem();
                    listItem.Text = item.BeneficiaryName + " - (" + item.BeneficiaryAccount + ")";
                    listItem.Value = item.BeneficiaryAccount;
                    benList.Add(listItem);
                }
                return benList;
            }
            return null;
        }
    }
}
