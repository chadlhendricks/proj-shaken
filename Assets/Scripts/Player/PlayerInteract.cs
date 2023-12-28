using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerInteract : MonoBehaviour
{
    // Script References
    [SerializeField] private CellphoneFlashlight _cellphoneFlashlight;
    [SerializeField] private TwoBoneIKConstraint _rightHandConstraint;

    private Animator _animator;

    private bool _isHoldingPhone;

    public void Start()
    {
        _animator = GetComponent<Animator>();
        _rightHandConstraint.weight = 0f;
    }

    private void Update()
    {
        ToggleFlashlight();
    }

    private void ToggleFlashlight()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!_isHoldingPhone)
            {
                _isHoldingPhone = true;
                _cellphoneFlashlight.ToggleFlashlight();
                StartCoroutine(SmoothRig(0, 1));
                _animator.SetLayerWeight(1, 1f);
            }
            else
            {
                _isHoldingPhone = false;
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
}
