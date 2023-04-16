using Oculus.Platform.Models;

internal class UserDataManager
{
    /*
     * THIS CLASS IS TEMPORARY, AND IS USED FOR THE PURPOSES OF DEMONSTRATING THE FUNCTIONALITY OF A USER-DATA-RETRIEVING.
     * The module is build to represent a class that manages user data-retrieving.
     * It may be replaced with a differently built class system.
     */


    public static UserData GetUser(string username, string password) // DUMMY FUNCTION
    {
        if (username == null || password == null)
            return null;
        if (username.Equals("Admin") && password.Equals("Admin"))
            return new UserData(username, "temp@temp.com", password, "000", true);
        else
            return new UserData(username, "temp@temp.com", password, "123", false);
    }
}