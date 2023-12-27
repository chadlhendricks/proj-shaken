using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Script References
    [SerializeField] private CellphoneFlashlight _cellphoneFlashlight;
    private Animator _animator;

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ToggleFlashlight();
        SitToType();
    }

    private void ToggleFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _cellphoneFlashlight.ToggleFlashlight();
        }
    }

    private void SitToType()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            _animator.SetLayerWeight(1, 1);
        }
    }
}
