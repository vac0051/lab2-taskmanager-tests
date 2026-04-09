using Moq;
using TaskManager.Application;
using TaskManager.Domain;

namespace TaskManager.Tests;

public class TaskServiceTests
{
    [Fact]
    public void AddTask_WithValidData_AddsTaskAndReturnsCreatedEntity()
    {
        var repositoryMock = new Mock<ITaskRepository>();
        repositoryMock.Setup(r => r.GetAll()).Returns(new[]
        {
            new TaskItem { Id = 1, Title = "Old task" },
            new TaskItem { Id = 3, Title = "Another old task" }
        });

        var service = new TaskService(repositoryMock.Object);

        var createdTask = service.AddTask("  New title  ", "  Description  ");

        Assert.Equal(4, createdTask.Id);
        Assert.Equal("New title", createdTask.Title);
        Assert.Equal("Description", createdTask.Description);
        Assert.False(createdTask.IsCompleted);
        Assert.True(createdTask.CreatedAt <= DateTime.UtcNow);

        repositoryMock.Verify(r => r.Add(It.Is<TaskItem>(t =>
            t.Id == 4 &&
            t.Title == "New title" &&
            t.Description == "Description" &&
            !t.IsCompleted)), Times.Once);
    }

    [Fact]
    public void AddTask_WithEmptyTitle_ThrowsArgumentException()
    {
        var repositoryMock = new Mock<ITaskRepository>();
        var service = new TaskService(repositoryMock.Object);

        Assert.Throws<ArgumentException>(() => service.AddTask("   ", "description"));
        repositoryMock.Verify(r => r.Add(It.IsAny<TaskItem>()), Times.Never);
    }

    [Fact]
    public void DeleteTask_WhenTaskExists_ReturnsTrueAndCallsRepositoryDelete()
    {
        var repositoryMock = new Mock<ITaskRepository>();
        repositoryMock.Setup(r => r.GetById(7)).Returns(new TaskItem { Id = 7, Title = "To delete" });
        var service = new TaskService(repositoryMock.Object);

        var result = service.DeleteTask(7);

        Assert.True(result);
        repositoryMock.Verify(r => r.Delete(7), Times.Once);
    }

    [Fact]
    public void DeleteTask_WhenTaskDoesNotExist_ReturnsFalse()
    {
        var repositoryMock = new Mock<ITaskRepository>();
        repositoryMock.Setup(r => r.GetById(7)).Returns((TaskItem?)null);
        var service = new TaskService(repositoryMock.Object);

        var result = service.DeleteTask(7);

        Assert.False(result);
        repositoryMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public void CompleteTask_WhenTaskExists_MarksTaskAsCompletedAndUpdatesRepository()
    {
        var task = new TaskItem { Id = 5, Title = "Do work", IsCompleted = false };
        var repositoryMock = new Mock<ITaskRepository>();
        repositoryMock.Setup(r => r.GetById(5)).Returns(task);
        var service = new TaskService(repositoryMock.Object);

        var result = service.CompleteTask(5);

        Assert.True(result);
        Assert.True(task.IsCompleted);
        repositoryMock.Verify(r => r.Update(task), Times.Once);
    }

    [Fact]
    public void CompleteTask_WhenTaskDoesNotExist_ReturnsFalse()
    {
        var repositoryMock = new Mock<ITaskRepository>();
        repositoryMock.Setup(r => r.GetById(5)).Returns((TaskItem?)null);
        var service = new TaskService(repositoryMock.Object);

        var result = service.CompleteTask(5);

        Assert.False(result);
        repositoryMock.Verify(r => r.Update(It.IsAny<TaskItem>()), Times.Never);
    }
}
