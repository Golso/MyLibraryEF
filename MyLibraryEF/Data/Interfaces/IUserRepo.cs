using MyLibraryEF.Models;

namespace MyLibraryEF.Data.Interfaces
{
    public interface IUserRepo
    {
        void AddUser(User user);
        void RemoveUser(int userId);
        void ChangeStateOfUser(int userId, int state);
        int GetUserId(string userName, string password);
        int GetUserState(int userId);
        string GetUserName(int userId);
        bool LoginPasswordExists(string userName, string password);

    }
}
