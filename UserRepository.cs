using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProteinTrackerWebService
{
    public interface IUserRepository
    {
        void Add(User user);
        ReadOnlyCollection<User> GetAll();
        User GetById(int id);
        void save(User updatedUser);
    }

    public class UserRepository : IUserRepository
    {
        private static readonly List<User> users = new List<User>();
        private static int nextId = 0;

        public void Add(User user)
        {
            user.UserID = nextId;
            nextId++;
            users.Add(user);
        }

        public ReadOnlyCollection<User> GetAll()
        {
            return users.AsReadOnly();
        }

        public User GetById(int id)
        {
            var user = users.SingleOrDefault(u => u.UserID == id);
            if (user == null)
                return null;

            return new User { Goal = user.Goal, Name = user.Name, UserID = user.UserID, Total = user.Total };
        }
        
        public void save(User updatedUser)
        {
            var originalUser = users.SingleOrDefault(u => u.UserID == updatedUser.UserID);
            if (originalUser == null)
                return;

            originalUser.Name = updatedUser.Name;
            originalUser.Total = updatedUser.Total;
            originalUser.Goal = updatedUser.Goal;
        }
    }
}
