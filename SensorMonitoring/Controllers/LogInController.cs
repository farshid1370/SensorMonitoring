using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SensorMonitoring.Model;

namespace SensorMonitoring.Controllers
{
    [Route("api/[controller]")]
    public class LogInController : ControllerBase
    {
        private SensorMonitoringContext Context { get; }
        public LogInController(SensorMonitoringContext context)
        {
            Context = context;
        }
        /// <summary>
        /// Login to app
        /// </summary>
        /// <param name="phoneNumber">Phone number of user</param>
        /// <param name="password">Password of user</param>
        /// <returns>ResultModel</returns>
        [HttpPost]
        public ActionResult<ResultViewModel> LogIn(string phoneNumber, string password)
        {
            try
            {
                User user = Context.Users.SingleOrDefault(p => p.PhoneNumber == phoneNumber && p.Password == password);
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
                    result.Message = JsonConvert.SerializeObject(Context.Users.Where(p=>p.Id==user.Id).Select(p=>new{ID = p.Id,p.FirstName,p.LastName,p.PhoneNumber}).SingleOrDefault());
                    result.ExeptionMessage = "";
                }
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                ResultViewModel result = new ResultViewModel
                {
                    Validate = false,
                    ValidateMessage = "عملیات با خطا مواجه شد",
                    Message = "",
                    ExeptionMessage = ex.Message
                };
                return new BadRequestObjectResult(result);
            }

        }
    }
}