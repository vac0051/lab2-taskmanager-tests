using System.Collections.Generic;
using TaskManager.Domain;

namespace TaskManager.Application
{
    /// <summary>
    /// Contains task business operations used by the presentation layer.
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// Returns all tasks.
        /// </summary>
        /// <returns>Collection of tasks.</returns>
        IEnumerable<TaskItem> GetAllTasks();

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="title">Task title.</param>
        /// <param name="description">Task description.</param>
        /// <returns>Created task.</returns>
        TaskItem AddTask(string title, string description);

        /// <summary>
        /// Deletes task by id.
        /// </summary>
        /// <param name="id">Task identifier.</param>
        /// <returns><c>true</c> when task was removed; otherwise <c>false</c>.</returns>
        bool DeleteTask(int id);

        /// <summary>
        /// Marks task as completed.
        /// </summary>
        /// <param name="id">Task identifier.</param>
        /// <returns><c>true</c> when task was updated; otherwise <c>false</c>.</returns>
        bool CompleteTask(int id);
    }
}
