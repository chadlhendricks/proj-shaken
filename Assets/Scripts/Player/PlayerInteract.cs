using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityTutorial.Manager;

public class PlayerInteract : MonoBehaviour
{
    private InputManager _inputManager;
    // private PauseManager _pauseManager;
    private Animator _animator;
    private Volume volume; 
    private VolumeProfile volumeProfile; 

    [SerializeField] private CellphoneFlashlight _cellphoneFlashlight;
    [SerializeField] private TwoBoneIKConstraint _rightHandConstraint;
    [SerializeField] private Transform _righthandTarget;

    private bool _isHoldingPhone;

    public void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _animator = GetComponent<Animator>();
        _rightHandConstraint.weight = 0f;

        if (volume == null)
        {
            // If volume is not assigned, try to find it in the scene
            volume = FindFirstObjectByType<Volume>();
        }

        if (volume != null)
        {
            // Get the Volume Profile
            volumeProfile = volume.profile;
        }
        else
        {
            Debug.LogError("No Volume found in the scene!");
            return;
        }
    }

    private void Update()
    {
        // TODO Need to stop being able to toggle flashlight when paused.
        if (!_inputManager.Crouch)
        {
            ToggleFlashlight();
        }
    }

    private void ToggleFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!_isHoldingPhone)
            {
                _isHoldingPhone = true;
                EnableDepthOfField(true);
                _cellphoneFlashlight.ToggleFlashlight();
                StartCoroutine(SmoothRig(0, 1));
                _animator.SetLayerWeight(1, 1f);
            }
            else
            {
                _isHoldingPhone = false;
                EnableDepthOfField(false);
                _cellphoneFlashlight.ToggleFlashlight();
                StartCoroutine(SmoothRig(1, 0));
                _animator.SetLayerWeight(1, 0f);
            }
        }   
    }

    IEnumerator SmoothRig(float start, float end)
    {
        float elapsedTime = 0;
        float waitTime = 0.5f;

        while (elapsedTime < waitTime)
        {
            _rightHandConstraint.weight = Mathf.Lerp(start, end, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void EnableDepthOfField(bool enable)
    {
        if (volumeProfile == null)
        {
            Debug.LogError("Volume Profile is not assigned!");
            return;
        }

        // Check if the DepthOfField effect exists in the Volume Profile
        if (volumeProfile.TryGet(out DepthOfField depthOfField))
        {
            depthOfField.active = enable;
        }
        else
        {
            Debug.LogError("Depth of Field effect not found in the Volume Profile!");
        }
    }
}
