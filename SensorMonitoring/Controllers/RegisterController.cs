using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SensorMonitoring.Model;


namespace SensorMonitoring.Controllers
{
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private SensorMonitoringContext Context { get; }
        public RegisterController(SensorMonitoringContext context)
        {
            Context = context;
        }
        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="user">The object of User</param>
        /// <returns>ResultModel</returns>
        [HttpPost]
        public ActionResult<ResultViewModel> Register(User user)
        {
            try
            {
                User finedUser = Context.Users.SingleOrDefault(p => p.PhoneNumber == user.PhoneNumber);
                ResultViewModel result = new ResultViewModel();
                if (finedUser != null)
                {
                    result.Validate = false;
                    result.ValidateMessage = "تلفن وارد شده قبلا استفاده شده است";
                    result.Message = "";
                    result.ExeptionMessage = "";
                }
                else
                {
                    Context.Users.Add(user);
                    Context.SaveChanges();
                    result.Validate = true;
                    result.ValidateMessage = "با موفقیت ثبت شد";
                    result.Message = JsonConvert.SerializeObject(Context.Users.Where(p => p.Id == user.Id).Select(p => new { ID = p.Id, p.FirstName, p.LastName, p.PhoneNumber }).SingleOrDefault());
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