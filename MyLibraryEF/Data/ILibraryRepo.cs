using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryEF.Data
{
    interface ILibraryRepo
    {
        bool LoginPasswordExists(string userName, string password);
        int GetUserId(string userName, string password);
        int GetUserState(int userId);
        string GetUserName(int userId);
        int GetAmountOfBooks(int userID);
    }
}
