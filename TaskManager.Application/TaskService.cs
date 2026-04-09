using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Domain;

namespace TaskManager.Application
{
    /// <summary>
    /// Provides business logic for task management.
    /// </summary>
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskService"/> class.
        /// </summary>
        /// <param name="taskRepository">Task repository implementation.</param>
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        /// <inheritdoc />
        public IEnumerable<TaskItem> GetAllTasks()
        {
            return _taskRepository.GetAll();
        }

        /// <inheritdoc />
        public TaskItem AddTask(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Task title cannot be empty.", nameof(title));
            }

            var newId = GenerateNextTaskId();

            var task = new TaskItem
            {
                Id = newId,
                Title = title.Trim(),
                Description = description?.Trim() ?? string.Empty,
                CreatedAt = DateTime.UtcNow,
                IsCompleted = false
            };

            _taskRepository.Add(task);
            return task;
        }

        /// <inheritdoc />
        public bool DeleteTask(int id)
        {
            if (!TaskExists(id))
            {
                return false;
            }

            _taskRepository.Delete(id);
            return true;
        }

        /// <inheritdoc />
        public bool CompleteTask(int id)
        {
            var task = _taskRepository.GetById(id);
            if (task is null)
            {
                return false;
            }

            task.IsCompleted = true;
            _taskRepository.Update(task);
            return true;
        }

        private int GenerateNextTaskId()
        {
            var tasks = _taskRepository.GetAll();
            return tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
        }

        private bool TaskExists(int id)
        {
            return _taskRepository.GetById(id) is not null;
        }
    }
}
