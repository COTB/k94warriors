using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
        public async Task<HttpResponseMessage> Post(string token, string taskKey)
        {
            Debug.WriteLine("Aditi API request received. Token: {0} TaskKey: {1}", token, taskKey);

            // Validate the token
            var correctToken = ConfigurationManager.AppSettings["AditiApiKey"] ?? string.Empty;
            if (string.IsNullOrEmpty(correctToken))
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error verifying API token."));
            if (!correctToken.Equals(token, StringComparison.Ordinal))
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            // Received the correct token. Run the task.
            if (await _taskService.RunTaskForKey(taskKey))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error dispatching task.");
            }
        }
    }
}