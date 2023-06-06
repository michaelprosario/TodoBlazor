namespace Todo.Core
{
    public class TodoItem
    {
        public TodoItem()
        {
            Status = "New";
            Priority = 2;
            Complexity = 2;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; } = 2;
        public int Complexity { get; set; } = 3;
    }

    public interface ITodoRepository
    {
        bool Add(TodoItem record);
        void Delete(string newId);
        TodoItem Get(string id);
        bool Update(TodoItem record);
        List<TodoItem> GetAll();
    }

    public interface ITodoService
    {
        string Add(TodoItem record);
        bool Update(TodoItem record);
        void Delete(string newId);
        TodoItem Get(string id);
        List<TodoItem> GetAll();
    }

    public class TodoService : ITodoService
    {
        private readonly ITodoRepository todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }

        public string Add(TodoItem record)
        {
            // todo - validate record

            if (string.IsNullOrEmpty(record.Id)) throw new InvalidDataException("Assign id to record");

            var ok = todoRepository.Add(record);
            return record.Id;
        }

        public bool Update(TodoItem record)
        {
            // validate record

            // run update operation on repository
            var ok = todoRepository.Update(record);

            return ok;
        }

        public void Delete(string newId)
        {
            todoRepository.Delete(newId);
        }

        public TodoItem Get(string id)
        {
            return todoRepository.Get(id);
        }

        public List<TodoItem> GetAll()
        {
            return todoRepository.GetAll();
        }
    }
}