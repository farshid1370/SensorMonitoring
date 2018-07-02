﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SensorMonitoring.Model;

namespace SensorMonitoring.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LogInController : ControllerBase
    {
        public SensorMonitoringContext Context { get; set; }
        public LogInController(SensorMonitoringContext _Context)
        {
            this.Context = _Context;
        }
        [HttpPost]
        public ActionResult<ResultViewModel> LogIn(string PhoneNumber, string Password)
        {
            try
            {
                User user = Context.Users.SingleOrDefault(p => p.PhoneNumber == PhoneNumber && p.Password == Password);
                ResultViewModel result = new ResultViewModel();
                if (user == null)
                {
                    result.Validate = false;
                    result.ValidateMessage = "نام کاربری یا رمز عبور اشتباه است";
                    result.Message = "";
                    result.ExeptionMessage = "";
                }
                else
                {
                    result.Validate = true;
                    result.ValidateMessage = "با موفقیت وارد شدید";
                    result.Message = JsonConvert.SerializeObject(Context.Users.Where(p=>p.ID==user.ID).Select(p=>new{p.ID,p.FirstName,p.LastName,p.PhoneNumber}));
                    result.ExeptionMessage = "";
                }
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                ResultViewModel result = new ResultViewModel();
                result.Validate = false;
                result.ValidateMessage = "عملیات با خطا مواجه شد";
                result.Message = "";
                result.ExeptionMessage = ex.Message;
                return new BadRequestObjectResult(result);
            }

        }
    }
}