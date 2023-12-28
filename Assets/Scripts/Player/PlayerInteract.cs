using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Animations.Rigging;

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
            //_animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 1f, Time.deltaTime * 13f));
            _cellphoneFlashlight.ToggleFlashlight();
        }
        else
        {
            //_animator.SetLayerWeight(1, Mathf.Lerp(_animator.GetLayerWeight(1), 0f, Time.deltaTime * 13f));
        }
    }

    private void SitToType()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {

        }
    }
}
