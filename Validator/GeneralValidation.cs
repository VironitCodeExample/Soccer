using System;
using UnityEngine;
using static LazySoccer.Network.Error.ErrorRequest;

public class GeneralValidation : MonoBehaviour
{
    public Action<ErrorsStatus, string> OnActionErrorsStatus;
    [SerializeField] private string _errorText;

    private ErrorsStatus _errorsStatus;

    public ErrorsStatus ErrorsStatus
    {
        get => _errorsStatus;
        set
        {
            _errorsStatus = value;
            OnActionErrorsStatus?.Invoke(_errorsStatus, _errorText);
        }
    }

    public void ErrorTextRequest(ErrorsStatus status, string error)
    {
        switch (status)
        {
            case ErrorsStatus.Name:
                Result(status, error); 
                break;
            case ErrorsStatus.Email:
                Result(status, error); 
                break;
            case ErrorsStatus.Password:
                Result(status, error); 
                break;
            case ErrorsStatus.ConfirmPassword:
                Result(status, error); 
                break;
            case ErrorsStatus.TwoFactorAuth:
                Result(status, error);
                break;
            default: 
                Debug.LogError(error); 
                break;
        }
    }
    private void Result(ErrorsStatus status, string error)
    {
        _errorText = error;
        ErrorsStatus = status;
    }
    public string TextError(ErrorsStatus status, string value, string value2 = "")
    {
        switch (status)
        {
            case ErrorsStatus.Name:
                return PrincipleValidation.PrincipleUserName(value);
            case ErrorsStatus.Email:
                return PrincipleValidation.PrincipleEmail(value);
            case ErrorsStatus.Password:
                return PrincipleValidation.PrinciplePassword(value);
            case ErrorsStatus.ConfirmPassword:
                return PrincipleValidation.PrinciplePassword(value);
            case ErrorsStatus.PasswordAndConfirmPassword:
                _errorText = PrincipleValidation.PrinciplePasswordAndPasswordConfirm(value, value2);
                if (_errorText.Length != 0) { ErrorsStatus = ErrorsStatus.ConfirmPassword; }
                return PrincipleValidation.PrinciplePasswordAndPasswordConfirm(value, value2);
            case ErrorsStatus.Code:
                return PrincipleValidation.PrincipleCode(value);

            default: return "";
        }
    }
    public void GetErrorMessentFromServer(ErrorsReg errorsReg)
    {
        if (errorsReg != null)
        {
            if (errorsReg.errors.UserName != null)
            {
                ErrorTextRequest(ErrorsStatus.Name, errorsReg.errors.UserName[0]);
            }
            if (errorsReg.errors.Password != null)
            {
                ErrorTextRequest(ErrorsStatus.Password, errorsReg.errors.Password[0]);
            }
            if (errorsReg.errors.Email != null)
            {
                ErrorTextRequest(ErrorsStatus.Email, errorsReg.errors.Email[0]);
            }
        }
    }
    public void GetErrorMessentFromServer(ValidationErrors errorsReg)
    {
        if (errorsReg != null)
        {
            foreach (var error in errorsReg.validationErrors)
            {
                if (error.field.Contains("Code"))
                {
                    ErrorTextRequest(ErrorsStatus.TwoFactorAuth, error.title);
                }
                if (error.field.Contains("Password"))
                {
                    ErrorTextRequest(ErrorsStatus.Password, error.title);
                }
                if (error.field.Contains("Email"))
                {
                    ErrorTextRequest(ErrorsStatus.Email, error.title);
                }
                if (error.field.Contains("UserName"))
                {
                    ErrorTextRequest(ErrorsStatus.Name, error.title);
                }
                if (error.field.Contains("Login"))
                {
                    ErrorTextRequest(ErrorsStatus.Email, error.title);
                }
            }
            ServiceLocator.GetService<ManagerLoading>().ControlLoading(false);
        }
    }
}