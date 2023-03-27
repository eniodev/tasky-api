using tarefasbackend.Models;

namespace tarefasbackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context =  context;
        }
        public void Create(User user)
        {
            user.Id = new System.Guid();
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User Read(string email, string password)
        {
            return _context.Users.SingleOrDefault(
                u => u.Email == email && u.Password == password
            );
        }
    }
}