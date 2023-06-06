using NUnit.Framework;
using Todo.Core;
using Todo.Infra;

namespace Todo.IntegrationTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TodoServices__Add__HandleValidData()
        {
            // arrange
            TodoItem todo = GetTestRecord();

            string dataPath = @"c:\dev\data";
            var repo = new TodoRepository(dataPath);
            var service = new TodoService(repo);

            // act
            string newId = service.Add(todo);

            // assert
            Assert.IsTrue(repo.RecordExists(newId));
        }

        [Test]
        public void TodoServices__Delete__HandleValidData()
        {
            // arrange
            TodoItem todo = GetTestRecord();

            string dataPath = @"c:\dev\data";
            var repo = new TodoRepository(dataPath);
            var service = new TodoService(repo);
            string newId = service.Add(todo);

            // act
            service.Delete(newId);

            // assert
            Assert.IsTrue(!repo.RecordExists(newId));
        }

        [Test]
        public void TodoServices__GetAll__HandleValidData()
        {
            // arrange
            TodoItem todo = GetTestRecord();
            string dataPath = @"c:\dev\data";
            var repo = new TodoRepository(dataPath);
            var service = new TodoService(repo);

            string newId = service.Add(todo);

            // act
            List<TodoItem> records = service.GetAll();

            // assert
            Assert.IsTrue(records.Count > 0);
        }

        [Test]
        public void TodoServices__Get__HandleValidData()
        {
            // arrange
            TodoItem todo = GetTestRecord();

            string dataPath = @"c:\dev\data";
            var repo = new TodoRepository(dataPath);
            var service = new TodoService(repo);
            string newId = service.Add(todo);

            // act
            TodoItem record = service.Get(newId);

            // assert
            Assert.IsTrue(record != null);
        }

        private static TodoItem GetTestRecord()
        {
            var todo = new TodoItem
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Test",
                Details = "Details 123",
                Status = "In progress",
                Complexity = 1,
                Priority = 1
            };

            return todo;
        }
    }
}