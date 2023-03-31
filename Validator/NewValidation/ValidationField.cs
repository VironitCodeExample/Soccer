using UnityEngine;
using TMPro;
using System.Linq;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

public class ValidationField : MonoBehaviour
{
    [SerializeField] private TMP_InputField textField;
    [SerializeField] private TMP_Text errorField;
    [SerializeField] private ErrorsStatus errorsStatus;

    private GeneralValidation _generalValidation;
    private void Start()
    {
        _generalValidation = ServiceLocator.GetService<GeneralValidation>();
        _generalValidation.OnActionErrorsStatus += Status;
        if(textField == null)
        {
            textField = GetComponent<TMP_InputField>();
        }
        if (errorField == null)
        {
            errorField = GetComponentsInChildren<TMP_Text>(true).First(x => x.name.Contains("ErrorText"));
        }
        errorField.text = "";
        ActiveError(false);
        
    }
    private void Status(ErrorsStatus errors, string errorText)
    {
        if (errors == errorsStatus)
            errorField.text = errorText;
        if(errorField.text.Length == 0)
            ActiveError(false);
        else ActiveError(true);
    }
    [Button]
    public bool isValidatoin()
    {
        ActiveError(true);
        errorField.text = _generalValidation.TextError(errorsStatus, textField.text);
        ActiveError(!errorField.text.IsUnityNull());
        return errorField.text.Length == 0;
    }

    private void ActiveError(bool active)
    {
        errorField.gameObject.SetActive(active);
    }
    /*private void OnDisable()
    {
        textField.text = "";
        errorField.text = "";
        ActiveError(false);
    }*/
    public void SetTextInput(ErrorsStatus status, string text)
    {
        if(status == errorsStatus)
        {
            textField.text = text;
        }
    }
}
