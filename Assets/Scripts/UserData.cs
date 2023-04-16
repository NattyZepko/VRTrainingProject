public class UserData
{
    private int id;
    private string name = "bOB";
    private string email = "TEST@TEST.COM";
    private string password = "testpass";
    private string phone = "000";
    private bool isAdmin = false;
    private static int idCounter = 0;

    public UserData(string name, string email, string password, string phone = "000", bool isAdmin = false)
    {
        this.id = idCounter++;
        this.name = name;
        this.email = email;
        this.password = password;
        this.phone = phone;
        this.isAdmin = isAdmin;
    }


    public int Id { get { return id; } }
    public string Name { get { return name; } set { name = value; } }
    public string Email { get { return email; } set { email = value; } }
    public string Password { get { return password; } set { password = value; } }
    public string Phone { get { return phone; } set { phone = value; } }
    public bool IsAdmin { get { return isAdmin; } }

}