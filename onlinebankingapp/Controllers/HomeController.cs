using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using obapp.Models;
using onlinebankingapp.Models;
using onlinebankingapp.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace onlinebankingapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<ServiceConnection> _serviceConnection;

        public IApiWrapper wrapper { get; }

        public HomeController(ILogger<HomeController> logger,IOptions<ServiceConnection> serviceConnection,IApiWrapper apiWrapper)
        {
            _logger = logger;
            _serviceConnection = serviceConnection;
            this.wrapper = apiWrapper;
        }

        public IActionResult Index()
        {
            UserAccessModel model = new UserAccessModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(UserAccessModel model)
        {
                string result = string.Empty;
            if (ModelState.IsValid)
            {

                Task<HttpResponseMessage> response = wrapper.PostAPI(string.Empty,
                                            _serviceConnection.Value.AuthUrl,
                                            JsonConvert.SerializeObject(model));

                using (HttpContent content = response.Result.Content)
                {
                    Task<string> tempResult = content.ReadAsStringAsync();
                    result = tempResult.Result;
                   
                    HttpContext.Session.SetString("Token", result);
                    HttpContext.Session.SetString("CustomerId", model.CustomerID);

                }
            }
            if (result != string.Empty)
                return RedirectToAction("Dashboard");
            return View(model);
        }
        public IActionResult Dashboard()
        {
            string result = string.Empty;
            CustomerModel customerModel = new CustomerModel();
            customerModel.CustomerId = HttpContext.Session.GetString("CustomerId").ToString();
            Task<HttpResponseMessage> response = wrapper.PostAPI(HttpContext.Session.GetString("Token"),
                                        _serviceConnection.Value.EndPoint+ "accounts/GetCustomerAccount",
                                        JsonConvert.SerializeObject(customerModel));
            using (HttpContent content = response.Result.Content)
            {
                Task<string> tempResult = content.ReadAsStringAsync();
                result = tempResult.Result;
            }
            ApiResponse<List<AccountModel>> accountData = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<List<AccountModel>>>(result.ToString());
            return View(accountData.Data);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
