using TodoAPI.Models;

namespace TodoAPI.Tests
{
    public class ToDoModelTests
    {
        [Fact]
        public void CanChangeName()
        {
            // Arrange
            var testToDo = new ToDoItem { Name = "Complete unit tests" };

            // Act
            testToDo.Name = "Complete Azure Pipeline";

            // Assert
            Assert.Equal("Complete Azure Pipe", testToDo.Name);
        }
    }
}