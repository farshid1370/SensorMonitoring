using System;
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
    public class RegisterController : ControllerBase
    {
        public SensorMonitoringContext Context { get; set; }
        public RegisterController(SensorMonitoringContext _Context)
        {
            this.Context = _Context;
        }
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
                    result.Message = JsonConvert.SerializeObject(Context.Users.Where(p => p.ID == user.ID).Select(p => new { p.ID, p.FirstName, p.LastName, p.PhoneNumber }));
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