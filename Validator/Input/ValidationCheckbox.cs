using UnityEngine;
using UnityEngine.UI;

public class ValidationCheckbox : ValidationInputBase
{
    [SerializeField] private Toggle toggle;
    public override void Validate()
    {
        isOk = toggle.isOn;
    }
}
