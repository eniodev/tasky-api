using tarefasbackend.Models;


    public interface ITaskRepository
    {
        List<Tasks> Read(Guid id);
        void Create(Tasks task);
        void Delete(Guid id);
        void Update(Guid id, Tasks task);
    }
