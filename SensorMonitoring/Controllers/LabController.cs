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
    public class LabController : ControllerBase
    {
        public SensorMonitoringContext Context { get; set; }
        public LabController(SensorMonitoringContext _Context)
        {
            this.Context = _Context;
        }
        [HttpPost]
        public ActionResult<ResultViewModel> Create(Lab lab)
        {
            try
            {
                Context.labs.Add(lab);
                Context.SaveChanges();
                ResultViewModel result = new ResultViewModel();
                result.Validate = true;
                result.ValidateMessage = "با موفقیت ثبت شد";
                result.Message = JsonConvert.SerializeObject(Context.labs.Where(p=>p.ID==lab.ID).Select(p=>new{p.ID,p.Name}));
                result.ExeptionMessage = "";

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
        [HttpGet("{UserID}")]
        public ActionResult<ResultViewModel> Get(int UserID)
        {
            try
            {
                var _labs = Context.labs.Where(p => p.UserID == UserID).Select(p => new { p.ID, p.Name }).ToList();
                ResultViewModel result = new ResultViewModel();
                result.Validate = true;
                result.ValidateMessage = "";
                result.Message = JsonConvert.SerializeObject(_labs);
                result.ExeptionMessage = "";
                if (_labs.Count == 0)
                {
                    result.ValidateMessage = "اطلاعاتی جهت نمایش وجود ندارد";
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