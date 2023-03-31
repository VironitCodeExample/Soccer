using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindValidationContainer : MonoBehaviour
{
    [SerializeField] private List<ValidationField> validationFields;
    private void Start()
    {
        validationFields = GetComponentsInChildren<ValidationField>(true).ToList();
    }

    public bool isRatification()
    {
        bool validation = true;
        foreach (var field in validationFields)
        {
            if (!field.isValidatoin())
                validation = false;
        }
        return validation;
    }
    public void SendText(ErrorsStatus status, string text)
    {
        validationFields.ForEach(x => x.SetTextInput(status, text));
    }
}
