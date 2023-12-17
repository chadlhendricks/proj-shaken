using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellphoneFlashlight : MonoBehaviour
{
    private bool _isFlashOn;

    private void Start()
    {
        SetFlashlightState(false);
    }

    public void ToggleFlashlight()
    {
        _isFlashOn = !_isFlashOn;

        SetFlashlightState(_isFlashOn);
    }

    public void SetFlashlightState(bool state)
    {
        gameObject.SetActive(state);
    }
}
