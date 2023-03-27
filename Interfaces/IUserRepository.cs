using tarefasbackend.Models;

public interface IUserRepository
    {
        User Read(string email, string password);
        void Create(User user);
    }