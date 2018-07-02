using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SensorMonitoring.Model;

namespace SensorMonitoring.Controllers
{
    [Route("api/[controller]")]
    public class SensorController : ControllerBase
    {
        private SensorMonitoringContext Context { get; set; }
        public SensorController(SensorMonitoringContext context)
        {
            Context = context;
        }
        [HttpPost]
        public ActionResult<ResultViewModel> Create(Sensor sensor)
        {
            try
            {
                Context.Sensors.Add(sensor);
                Context.SaveChanges();
                ResultViewModel result = new ResultViewModel
                {
                    Validate = true,
                    ValidateMessage = "با موفقیت ثبت شد",
                    Message = JsonConvert.SerializeObject(Context.Sensors.Where(p => p.Id == sensor.Id)
                        .Select(p => new {ID = p.Id, p.IndicatorCode, p.Title, p.SubTitle})),
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
        [HttpGet("{labID}")]
        public ActionResult<ResultViewModel> Get(int labId)
        {
            try
            {
                var sensors = Context.Sensors.Where(p => p.LabId == labId).Select(p => new { ID = p.Id, p.IndicatorCode,p.Title,p.SubTitle }).ToList();
                ResultViewModel result = new ResultViewModel
                {
                    Validate = true,
                    ValidateMessage = "",
                    Message = JsonConvert.SerializeObject(sensors),
                    ExeptionMessage = ""
                };
                if (sensors.Count == 0)
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