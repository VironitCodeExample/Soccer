using System;
using System.Net.Mail;

public class ValidatorEmail : IValidator
{
    override public bool isValid(in string text)
    {
        try
        {
            MailAddress m = new MailAddress(text);

            if (m.Address.Length > 320)
                return false;

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
