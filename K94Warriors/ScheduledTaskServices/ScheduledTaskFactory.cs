using System;
using System.Collections.Generic;

namespace K94Warriors.ScheduledTaskServices
{
    /// <summary>
    /// This class is the resolver, but it is also the global scope
    /// so we derive from NinjectScope.
    /// </summary>
    public class ScheduledTaskFactory : IScheduledTaskFactory
    {
        private readonly IDictionary<string, Type> _taskDictionary;

        public ScheduledTaskFactory(IDictionary<string, Type> taskDictionary)
        {
            if (taskDictionary == null)
                throw new ArgumentNullException("taskDictionary", "Task types must be registered.");
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
            return Activator.CreateInstance(type) as IScheduledTask;
        }
    }
}