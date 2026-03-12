using TaskApi.Controllers;
using TaskApi.Models;
using TaskApi.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TaskApi.Tests.Controllers;

public class TaskControllerTests
{
    private readonly TasksController _controller;
    private readonly Mock<ITaskRepository> _mockRepo;

    public TaskControllerTests()
    {
        _mockRepo = new Mock<ITaskRepository>();
        _controller = new TasksController(_mockRepo.Object);
    }

    [Fact]
    public void GetAll_CuandoHayTareas_RetornaTodasLasTareas()
    {
        //Arrange
        _mockRepo.Setup(repo => repo.GetAll()).Returns(
            new List<TaskItem> { 
                new TaskItem { Id = 1, Title = "Tarea 1",},
                new TaskItem { Id = 2, Title = "Tarea 2",}
            });
        //Act
        _controller.GetAll()
            .Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<IEnumerable<TaskItem>>()
            .Which.Should().HaveCount(2);
    }

    [Fact]
    public void GetById_CuandoExisteTarea_RetornaTarea()
    {
        //Arrange
        _mockRepo.Setup(repo => repo.GetById(1)).Returns(
            new TaskItem { Id = 1, Title = "Tarea 1",});
        //Act
        _controller.GetById(1)
            .Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<TaskItem>()
            .Which.Id.Should().Be(1);
    }

    [Fact]
    public void GetById_CuandoNoExisteTarea_RetornaNotFound()
    {
        //Arrange
        _mockRepo.Setup(repo => repo.GetById(999)).Returns((TaskItem?)null);
        //Act
        _controller.GetById(999)
            .Should().BeOfType<NotFoundResult>();
    }
}
