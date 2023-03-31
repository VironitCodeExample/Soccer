using System.Text.RegularExpressions;


public class ValidatorNickName : IValidator
{
    override public bool isValid(in string text)
    {
        var regex = new Regex(@"^[a-zA-Z]+(?:['-][a-zA-Z\s*]+)*$");
        if (!regex.IsMatch(text))
            return false;
        if(text.Length < 2) 
            return false;
        return true;
    }
}