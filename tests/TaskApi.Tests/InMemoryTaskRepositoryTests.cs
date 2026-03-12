using TaskApi.Repositories;
using TaskApi.Models;
using FluentAssertions;
namespace TaskApi.Tests.Repositories;
public class InMemoryTaskRepositoryTests {
    private readonly InMemoryTaskRepository _repo;
    public InMemoryTaskRepositoryTests(){
        _repo = new();
    }
    
    [Fact]
    public void Add_TareaValida_AsignaIdYRetornaTarea(){
        //Arrange        
        var tarea = new TaskItem {
            Title = "Comprar Guitarra",
            Description= "Comprar Guitarra para ser Feliz"
        };
        //Act
        var resultado = _repo.Add(tarea);
        //Arrange
        resultado.Id.Should().BeGreaterThan(0);
        resultado.Title.Should().Be("Comprar Guitarra");
    }

    [Fact]
    public void Add_VariasTareas_AsignaIdsUnicos(){
        //Arrange        
        var tarea1 = new TaskItem { Title = "Tarea 1", Description= "Descripción 1" };
        var tarea2 = new TaskItem { Title = "Tarea 2", Description= "Descripción 2" };
        //Act
        var resultado1 = _repo.Add(tarea1);
        var resultado2 = _repo.Add(tarea2);
        //Arrange
        resultado1.Id.Should().NotBe(resultado2.Id);
    }

    [Fact]
    public void GetAllReposirotyVacio_ObtieneVacio()
    {
        //Act
        var resultado = _repo.GetAll();
        //Assert
        resultado.Should().BeEmpty();
    }

    [Fact]
    public void GetAll_CuandoSeAgreganTareas_RetornaTodasLasTareas(){
        //Arrange        
        var tarea1 = new TaskItem { Title = "Tarea 1", Description= "Descripción 1" };
        var tarea2 = new TaskItem { Title = "Tarea 2", Description= "Descripción 2" };
        _repo.Add(tarea1);
        _repo.Add(tarea2);
        //Act
        var resultado = _repo.GetAll();
        //Arrange
        resultado.Should().HaveCount(2);
    }

    [Fact]
    public void GetById_CuandoExisteTarea_RetornaTarea(){
        //Arrange        
        var tarea = new TaskItem { Title = "Tarea 1", Description= "Descripción 1" };
        var tareaAgregada = _repo.Add(tarea);
        //Act
        var resultado = _repo.GetById(tareaAgregada.Id);
        //Arrange
        resultado.Should().NotBeNull();
        resultado!.Id.Should().Be(tareaAgregada.Id);
    }

    [Fact]
    public void GetById_CuandoNoExisteTarea_RetornaNull(){
        //Act
        var resultado = _repo.GetById(999);
        //Arrange
        resultado.Should().BeNull();
    }

    [Fact]
    public void Update_CuandoExisteTarea_ActualizaYRetornaTarea(){
        //Arrange        
        var tarea = new TaskItem { Title = "Tarea 1", Description= "Descripción 1" };
        var tareaAgregada = _repo.Add(tarea);
        var tareaActualizada = new TaskItem { Title = "Tarea Actualizada", Description= "Descripción Actualizada"};
        //Act
        var resultado = _repo.Update(tareaAgregada.Id, tareaActualizada);
        //Arrange
        resultado.Should().NotBeNull();
        resultado!.Title.Should().Be("Tarea Actualizada");
    }

    [Fact]
    public void Update_CuandoNoExisteTarea_RetornaNull(){
        //Arrange        
        var tareaActualizada = new TaskItem { Title = "Tarea Actualizada", Description= "Descripción Actualizada"};
        //Act
        var resultado = _repo.Update(999, tareaActualizada);
        //Arrange
        resultado.Should().BeNull();
    }

    [Fact]
    public void Delete_CuandoExisteTarea_EliminaYRetornaTrue(){
        //Arrange        
        var tarea = new TaskItem { Title = "Tarea 1", Description= "Descripción 1" };
        var tareaAgregada = _repo.Add(tarea);
        //Act
        var resultado = _repo.Delete(tareaAgregada.Id);
        //Arrange
        resultado.Should().BeTrue();
    }

    [Fact]
    public void Delete_CuandoNoExisteTarea_RetornaFalse(){
        //Act
        var resultado = _repo.Delete(999);
        //Arrange
        resultado.Should().BeFalse();
    }
}