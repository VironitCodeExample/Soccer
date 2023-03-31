using Sirenix.OdinInspector;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ValidationInputBase : MonoBehaviour
{
    public bool isValue = false;
    public bool isOk = false;
    //[ValueDropdown(nameof(keyType))] public string TypeInput;
    //[ValueDropdown(nameof(keyError))] public string TypeError;

    //public IEnumerable keyType => GeneralValidation.instance.KeyType;
    //public IEnumerable keyError => GeneralValidation.instance.KeyError(TypeInput);

    public IValidator validator;

    public void Value(bool result)
    {
        isValue = result;
    }

    virtual public void Start()
    {
        validator = GetComponent<IValidator>();
    }

    virtual public void Validate()
    {

    }
    virtual public void Error(InputError error)
    {
        if (!validator.isValid(error.InputField.text))
        {
            error.ErrorText.gameObject.SetActive(true);
            //error.ErrorText.text = GeneralValidation.instance.DescriptionError(TypeInput, TypeError);
            isOk = false;
        }
        else
        {
            error.ErrorText.gameObject.SetActive(false);
            error.ErrorText.text = "";
            isOk = true;
        }
    }
}
[Serializable]
public class InputError
{
    public TMP_Text ErrorText;
    public TMP_InputField InputField;
}
