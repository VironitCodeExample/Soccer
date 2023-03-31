using System.Collections.Generic;
using UnityEngine;

public class ValidationTwoInput : ValidationInputBase
{
    [SerializeField] private List<InputError> inputErrors;
    public override void Start()
    {
        base.Start();
        inputErrors.ForEach(x => x.ErrorText.gameObject.SetActive(false));
    }
    public override void Validate()
    {
        Debug.LogError(this.name);
        if (inputErrors[0].InputField.text != inputErrors[1].InputField.text)
        {
            //inputErrors[1].ErrorText.text = GeneralValidation.instance.DescriptionError(TypeInput, TypeError);
        }
        else {
            foreach (InputError error in inputErrors)
                Error(error);
        }
    }
}
