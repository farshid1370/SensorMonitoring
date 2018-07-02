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
    public class SensorDataController : ControllerBase
    {
        public SensorMonitoringContext Context { get; set; }
        public SensorDataController(SensorMonitoringContext _Context)
        {
            this.Context = _Context;
        }
      
        [HttpGet]
        public ActionResult<ResultViewModel> Create(int Priorty , string Value , int SensorID)
        {
            try
            {
                SensorData sensorData = new SensorData();
                sensorData.SensorID = Convert.ToInt32(SensorID);
                sensorData.Priorty = Convert.ToInt32(Priorty);
                sensorData.Value = Value;
                sensorData.Time = DateTime.Now;
                Context.SensorDatas.Add(sensorData);
                Context.SaveChanges();
                ResultViewModel result = new ResultViewModel();
                result.Validate = true;
                result.ValidateMessage = "با موفقیت ثبت شد";
                result.Message = JsonConvert.SerializeObject(Context.SensorDatas.Where(p=>p.ID==sensorData.ID).Select(p=>new{p.ID,p.Priorty,p.Time,p.Value}));
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
        [HttpPost]
        public ActionResult<ResultViewModel> Get(int SensorID, int Number, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                var _sensorsData = Context.SensorDatas.Where(p => p.SensorID == SensorID && p.Time >= StartDate && p.Time <= EndDate)
                    .Select(p => new { p.ID, p.Priorty, p.Time, p.Value }).OrderByDescending(p=>p.ID).Take(Number).ToList();
                ResultViewModel result = new ResultViewModel();
                result.Validate = true;
                result.ValidateMessage = "";
                result.Message = JsonConvert.SerializeObject(_sensorsData);
                result.ExeptionMessage = "";
                if (_sensorsData.Count == 0)
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