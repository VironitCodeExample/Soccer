using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidatorPassword : IValidator
{
    override public bool isValid(in string text)
    {
        return text.Length <= 64 && 8 <= text.Length;
    }

}
