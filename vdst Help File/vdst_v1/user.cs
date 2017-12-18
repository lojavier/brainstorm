using System;

public class user
{
    public string ID;
    public string FirstName;
    public string LastName;
    public string Passcode;
    public bool isAdmin;

	public user()
	{
        ID = "00";
        Passcode = "00";
        FirstName = "Admin";
        LastName = "Admin";
        isAdmin = true;
	}
}
