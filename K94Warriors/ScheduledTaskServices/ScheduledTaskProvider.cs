using System;
using System.Collections.Generic;

namespace K94Warriors.ScheduledTaskServices
{
    public class ScheduledTaskProvider : IScheduledTaskProvider
    {
        private readonly IDictionary<string, Func<IScheduledTask>> _factories;

        public ScheduledTaskProvider(IDictionary<string, Func<IScheduledTask>> factories)
        {
            if (factories == null)
                throw new ArgumentNullException(nameof(factories), "Task factories must be registered.");

            _factories = factories;
        }

        /// <summary>
        /// Gets an instance of the task for the specified key. Returns null
        /// if no task factory is registered for the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IScheduledTask GetTask(string key)
        {
            var factory = _factories[key];
            var task = factory?.Invoke();
            return task;
        }
    }
}