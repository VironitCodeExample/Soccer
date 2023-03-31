using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using UnityEngine;

public static class PrincipleValidation
{
    private static Regex passwordRex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,64}$");
    private static Regex emailRex = new Regex("^\\S+@\\S+\\.\\S+$");
    private static Regex UserName = new Regex("^\\b(?:\\w|-)+\\b$");

    public static string PrincipleUserName(string value)
    {
        if (UserName.IsMatch(value))
        {
            if(value.Length > 3 && value.Length < 65)
                return "";
            else 
                return "The UserName is invalid";
        }
        else
        return "The UserName is invalid";
    }
    public static string PrincipleEmail(string value)
    {
        if (emailRex.IsMatch(value))
        {
            if (value.Length > 3 && value.Length < 321)
                return "";
            else
                return "The email is invalid";
        }
        else
            return "The email is invalid";
    }
    public static string PrinciplePassword(string value)
    {
        if (!passwordRex.IsMatch(value))
        {
            return "The password must contain 8-64 characters, including letters, numbers, special characters, no spaces";
        }
        return "";
    }

    public static string PrinciplePasswordAndPasswordConfirm(string value1, string value2)
    {
        if (value1 != value2)
        {
            return "Password and confirm password fields must match";
        }
        return "";
    }
    public static string PrincipleCode(string value)
    {
        if (value.Length > 7 && value.Length < 16)
        {
            return "";
        }
        return "The Code is invalid";
    }
}
