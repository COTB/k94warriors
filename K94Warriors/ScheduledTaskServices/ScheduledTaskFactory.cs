using System;
using System.Linq;
using System.Collections.Generic;

namespace K94Warriors.ScheduledTaskServices
{
    public class ScheduledTaskFactory : IScheduledTaskFactory
    {
        private readonly IDictionary<string, Type> _taskDictionary;

        public ScheduledTaskFactory(IDictionary<string, Type> taskDictionary)
        {
            if (taskDictionary == null)
                throw new ArgumentNullException("taskDictionary", "Task types must be registered.");

            // validate registered types
            if (taskDictionary.Values.Any(value => !value.GetInterfaces().Contains(typeof(IScheduledTask))))
            {
                throw new ArgumentException("Registered types must implement interface IScheduledTask.");
            }

            _taskDictionary = taskDictionary;
        }

        /// <summary>
        /// Gets an instance of the task for the specified key. Returns null
        /// if no task is registered for the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IScheduledTask GetTask(string key)
        {
            var type = _taskDictionary[key];
            if (type == null)
                return null;
            return Activator.CreateInstance(type) as IScheduledTask;
        }
    }
}