using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class AccountController : Controller
    {
        private readonly IOptions<ServiceConnection> _serviceConnection;
        public IApiWrapper wrapper { get; }

        public AccountController(IOptions<ServiceConnection> serviceConnection, IApiWrapper apiWrapper)
        {
            _serviceConnection = serviceConnection;
            this.wrapper = apiWrapper;
        }
        [HttpPost]
        public  IActionResult Index(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                string result = string.Empty;
                var response =  wrapper.PostAPI(HttpContext.Session.GetString("Token"),
                                            _serviceConnection.Value.EndPoint + "accounts/getaccount",
                                            JsonConvert.SerializeObject(model)).GetAwaiter().GetResult();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (HttpContent content = response.Content)
                    {
                        Task<string> tempResult = content.ReadAsStringAsync();
                        result = tempResult.Result;
                    }
                    ApiResponse<AccountModel> accountData = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<AccountModel>>(result.ToString());
                    return View(accountData.Data);
                }
                else
                    return RedirectToAction("unauthorized", "error");
            }
            return View(model);
        }
    }
}
