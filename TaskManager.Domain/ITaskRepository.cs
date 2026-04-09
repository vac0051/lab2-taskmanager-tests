using System;
using System.Collections.Generic;

namespace TaskManager.Domain
{
    /// <summary>
    /// Defines CRUD operations for task persistence.
    /// </summary>
    public interface ITaskRepository
    {
        /// <summary>
        /// Returns all tasks from storage.
        /// </summary>
        /// <returns>Collection of tasks.</returns>
        IEnumerable<TaskItem> GetAll();

        /// <summary>
        /// Finds task by identifier.
        /// </summary>
        /// <param name="id">Task identifier.</param>
        /// <returns>Task when found; otherwise <c>null</c>.</returns>
        TaskItem? GetById(int id);

        /// <summary>
        /// Adds a new task to storage.
        /// </summary>
        /// <param name="task">Task to add.</param>
        void Add(TaskItem task);

        /// <summary>
        /// Deletes a task by identifier.
        /// </summary>
        /// <param name="id">Task identifier.</param>
        void Delete(int id);

        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="task">Task with updated values.</param>
        void Update(TaskItem task);
    }
}
