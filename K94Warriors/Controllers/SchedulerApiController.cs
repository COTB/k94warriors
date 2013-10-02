using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web.Http;
using K94Warriors.ScheduledTaskServices;

namespace K94Warriors.Controllers
{
    public class SchedulerApiController : ApiController
    {
        private readonly IScheduledTaskService _taskService;

        public SchedulerApiController(IScheduledTaskService taskService)
        {
            if (taskService == null)
                throw new ArgumentNullException("taskService");
            _taskService = taskService;
        }

        /// <summary>
        /// API for Aditi cloud scheduler to call when scheduled tasks are due.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="taskKey"></param>
        public void Post(string token, string taskKey)
        {
            Debug.WriteLine("Aditi API request received. Token: {0} TaskKey: {1}", token, taskKey);

            // Validate the token
            var correctToken = ConfigurationManager.AppSettings["AditiApiKey"] ?? string.Empty;
            if (string.IsNullOrEmpty(correctToken))
                return;
            if (!correctToken.Equals(token, StringComparison.Ordinal))
                throw new SecurityException("Request made to Aditi API with invalid token.");

            // Received the correct token. Run the task.
            _taskService.RunTaskForKey(taskKey);
        }
    }
}