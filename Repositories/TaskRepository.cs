using Microsoft.EntityFrameworkCore;
using tarefasbackend.Models;

namespace tarefasbackend.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _context;
        public TaskRepository(DataContext context)
        {
            _context = context;
        }

        public List<Tasks> Read(Guid id)
        {
            return _context.Tasks.Where(task => task.UserId == id).ToList();
            
        }
        public void Create(Tasks task)
        {
            task.Id = Guid.NewGuid();
            _context.Add(task);
            _context.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var task = _context.Tasks.Find(id);
            _context.Entry(task).State = EntityState.Deleted;
            _context.SaveChanges();
        }
        public void Update(Guid id, Tasks task)
        {
            var taskToUpdate = _context.Tasks.Find(id);

            taskToUpdate.Name = task.Name;
            taskToUpdate.Done = task.Done;
            _context.Entry(taskToUpdate).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}