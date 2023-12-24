using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Script References
    [SerializeField] private CellphoneFlashlight _cellphoneFlashlight;

    public void Start()
    {
    
    }

    private void Update()
    {
        ToggleFlashlight();
    }

    private void ToggleFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _cellphoneFlashlight.ToggleFlashlight();
        }
    }
}
