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
    [Route("api/[controller]")]
    public class SensorController : ControllerBase
    {
        public SensorMonitoringContext Context { get; set; }
        public SensorController(SensorMonitoringContext _Context)
        {
            this.Context = _Context;
        }
        [HttpPost]
        public ActionResult<ResultViewModel> Create(Sensor sensor)
        {
            try
            {
                Context.Sensors.Add(sensor);
                Context.SaveChanges();
                Sensor _sensor = new Sensor();
                _sensor.ID = sensor.ID;
                _sensor.IndicatorCode = sensor.IndicatorCode;
                _sensor.Title = sensor.Title;
                _sensor.SubTitle = sensor.SubTitle;
                ResultViewModel result = new ResultViewModel();
                result.Validate = true;
                result.ValidateMessage = "با موفقیت ثبت شد";
                result.Message = JsonConvert.SerializeObject(_sensor);
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
        [HttpGet("{LabID}")]
        public ActionResult<ResultViewModel> Get(int LabID)
        {
            try
            {
                var _sensors = Context.Sensors.Where(p => p.LabID == LabID).Select(p => new { p.ID, p.IndicatorCode,p.Title,p.SubTitle }).ToList();
                ResultViewModel result = new ResultViewModel();
                result.Validate = true;
                result.ValidateMessage = "";
                result.Message = JsonConvert.SerializeObject(_sensors);
                result.ExeptionMessage = "";
                if (_sensors.Count == 0)
                {
                    result.ValidateMessage = "اطلاعاتی جهت نمایش وجود ندارد"; f
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