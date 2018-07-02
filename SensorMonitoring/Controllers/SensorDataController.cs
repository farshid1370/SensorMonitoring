using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SensorMonitoring.Model;

namespace SensorMonitoring.Controllers
{
    [Route("api/[controller]")]
    public class SensorDataController : ControllerBase
    {
        private SensorMonitoringContext Context { get; set; }
        public SensorDataController(SensorMonitoringContext context)
        {
            Context = context;
        }
      
        [HttpGet]
        public ActionResult<ResultViewModel> Create(int priorty , string value , int sensorId)
        {
            try
            {
                SensorData sensorData = new SensorData
                {
                    SensorId = Convert.ToInt32(sensorId),
                    Priorty = Convert.ToInt32(priorty),
                    Value = value,
                    Time = DateTime.Now
                };
                Context.SensorDatas.Add(sensorData);
                Context.SaveChanges();
                ResultViewModel result = new ResultViewModel
                {
                    Validate = true,
                    ValidateMessage = "با موفقیت ثبت شد",
                    Message = JsonConvert.SerializeObject(Context.SensorDatas.Where(p => p.Id == sensorData.Id)
                        .Select(p => new {ID = p.Id, p.Priorty, p.Time, p.Value}).SingleOrDefault()),
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
        [HttpPost]
        public ActionResult<ResultViewModel> Get(int sensorId, int number, DateTime startDate, DateTime endDate)
        {
            try
            {
                var sensorsData = Context.SensorDatas.Where(p => p.SensorId == sensorId && p.Time >= startDate && p.Time <= endDate)
                    .Select(p => new { ID = p.Id, p.Priorty, p.Time, p.Value }).OrderByDescending(p=>p.ID).Take(number).ToList();
                ResultViewModel result = new ResultViewModel
                {
                    Validate = true,
                    ValidateMessage = "",
                    Message = JsonConvert.SerializeObject(sensorsData),
                    ExeptionMessage = ""
                };
                if (sensorsData.Count == 0)
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