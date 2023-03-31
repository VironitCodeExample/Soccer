using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IValidator : MonoBehaviour
{
    virtual public bool isValid(in string text) { return !string.IsNullOrEmpty(text); }
}
