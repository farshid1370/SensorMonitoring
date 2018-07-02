using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SensorMonitoring.Model;

namespace SensorMonitoring.Controllers
{
    [Route("api/[controller]")]
    public class LabController : ControllerBase
    {
        private SensorMonitoringContext Context { get; }
        public LabController(SensorMonitoringContext context)
        {
            Context = context;
        }
        [HttpPost]
        public ActionResult<ResultViewModel> Create(Lab lab)
        {
            try
            {
                Context.Labs.Add(lab);
                Context.SaveChanges();
                ResultViewModel result = new ResultViewModel
                {
                    Validate = true,
                    ValidateMessage = "با موفقیت ثبت شد",
                    Message = JsonConvert.SerializeObject(Context.Labs.Where(p => p.Id == lab.Id)
                        .Select(p => new {ID = p.Id, p.Name}).SingleOrDefault()),
                    ExeptionMessage = ""
                };

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
        [HttpGet("{userID}")]
        public ActionResult<ResultViewModel> Get(int userId)
        {
            try
            {
                var labs = Context.Labs.Where(p => p.UserId == userId).Select(p => new { ID = p.Id, p.Name }).ToList();
                ResultViewModel result = new ResultViewModel
                {
                    Validate = true,
                    ValidateMessage = "",
                    Message = JsonConvert.SerializeObject(labs),
                    ExeptionMessage = ""
                };
                if (labs.Count == 0)
                {
                    result.ValidateMessage = "اطلاعاتی جهت نمایش وجود ندارد";
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