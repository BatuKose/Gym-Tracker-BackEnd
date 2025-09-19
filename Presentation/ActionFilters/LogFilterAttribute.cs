using Entites.LogModel;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilters
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        private readonly ILoggerService _logger;

        public LogFilterAttribute(ILoggerService logger)
        {
            _logger = logger;
        }

    
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInfo(Log("OnActionExecuting", context.RouteData));
        }

        private string Log(string ModelName, RouteData routeData)
        {
            var LogDetails = new LogDetails()
            {
                ModelName = ModelName,
                ControllerName = routeData.Values["Controller"],
                Action = routeData.Values["action"]
            };
            if (routeData.Values.Count>=3)
                LogDetails.Id=routeData.Values["Id"];
            return LogDetails.ToString();
        }
    }
}
