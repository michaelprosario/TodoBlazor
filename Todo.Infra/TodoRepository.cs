using System.Text.Json;
using Todo.Core;

namespace Todo.Infra
{
    public class TodoRepository : ITodoRepository
    {
        private string dataPath;

        public TodoRepository()
        {
            dataPath = "c:\\dev\\data";
        }

        public TodoRepository(string dataPath)
        {
            if (string.IsNullOrEmpty(dataPath))
                throw new ArgumentException($"'{nameof(dataPath)}' cannot be null or empty.", nameof(dataPath));

            this.dataPath = dataPath;
        }

        public bool Add(TodoItem record)
        {
            string fileName = GetRecordPath(record.Id);
            if (!Directory.Exists(dataPath))
                throw new ApplicationException("data path does not exist");

            try
            {
                string jsonString = JsonSerializer.Serialize(record);
                File.WriteAllText(fileName, jsonString);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public void Delete(string id)
        {
            string fileName = GetRecordPath(id);
            File.Delete(fileName);
        }

        public TodoItem Get(string id)
        {
            string path = GetRecordPath(id);
            var record = GetRecordFromPath(path);
            return record;
        }

        public bool Update(TodoItem record)
        {
            string fileName = GetRecordPath(record.Id);
            if (!Directory.Exists(dataPath))
                throw new ApplicationException("data path does not exist");

            try
            {
                string jsonString = JsonSerializer.Serialize(record);
                File.WriteAllText(fileName, jsonString);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<TodoItem> GetAll()
        {
            var list = new List<TodoItem>();
            string[] files = Directory.GetFileSystemEntries(dataPath);

            foreach (string file in files)
            {
                TodoItem item = GetRecordFromPath(file);
                list.Add(item);
            }

            return list;
        }

        public void SetDataPath(string dataPath)
        {
            this.dataPath = dataPath;
        }

        private string GetRecordPath(string id)
        {
            return dataPath + Path.DirectorySeparatorChar + id;
        }

        public bool? RecordExists(string newId)
        {
            string fileName = GetRecordPath(newId);
            return File.Exists(fileName);
        }

        private TodoItem? GetRecordFromPath(string path)
        {
            string json = File.ReadAllText(path);
            var record =
                JsonSerializer.Deserialize<TodoItem>(json);
            return record;
        }
    }
}