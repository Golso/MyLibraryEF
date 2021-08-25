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
