using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellphoneFlashlight : MonoBehaviour
{
    public GameObject _cellphoneLight;
    [SerializeField] private GameObject _rightHand;
    private bool _isFlashOn;

    private void Start()
    {
        SetFlashlightState(true);
    }

    public void ToggleFlashlight()
    {
        _isFlashOn = !_isFlashOn;

        SetFlashlightState(_isFlashOn);
    }

    public void SetFlashlightState(bool state)
    {
        _rightHand.SetActive(state);
        gameObject.SetActive(state);
        _cellphoneLight.SetActive(state);
    }
}
