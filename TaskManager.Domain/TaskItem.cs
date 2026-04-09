using System;

namespace TaskManager.Domain
{
    /// <summary>
    /// Represents a single task in the task manager.
    /// </summary>
    public class TaskItem
    {
        /// <summary>
        /// Gets or sets task identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets task title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets task description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets value indicating whether task is completed.
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Gets or sets UTC creation time.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
