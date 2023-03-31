using UnityEngine;

public class ValidationOneInput : ValidationInputBase
{
    [SerializeField] private InputError error;

    public override void Start()
    {
        base.Start();
        error.ErrorText.gameObject.SetActive(false);
    }
    public override void Validate()
    {
        Error(error);
    }
}
