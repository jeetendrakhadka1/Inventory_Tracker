using System;
public static class ClaimsPrincipalExtension
{
	public ClaimsPrincipalExtension()
	{

	}

    public static string GetUserName(this ClaimsPrincipal principal)
    {
        string username;
        username = (from c in principal.Claims
                    where c.Type == "Username"
                    select c.Value).FirstOrDefault();
        return username;
    }
}
