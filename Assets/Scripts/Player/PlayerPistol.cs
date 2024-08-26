using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using UnityTutorial.Manager;

public class PlayerPistol : MonoBehaviour
{
    [SerializeField] private Camera AimCamera;
    [SerializeField] private InputAction _aimInput;
    [SerializeField] private GameObject _gun;

    private Animator _animator;
    private InputManager _inputManager;

    [SerializeField] private MultiAimConstraint _bodyAimingConstraint;
    [SerializeField] private MultiAimConstraint _rightHandAimingConstraint;
    [SerializeField] private TwoBoneIKConstraint _leftHandAimingConstraint;

    [SerializeField] private GameObject _cellphoneConstraint;

    private bool _isHoldingGun;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _inputManager = GetComponent<InputManager>();

        _bodyAimingConstraint.weight = 0f;
        _rightHandAimingConstraint.weight = 0f;
        _leftHandAimingConstraint.weight = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_inputManager.Crouch)
        {
            TogglePistol();
        }
    }

    private void TogglePistol()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!_isHoldingGun)
            {
                _animator.SetTrigger("EquipPistol");
                _cellphoneConstraint.SetActive(false);
                _isHoldingGun = true;
                StartCoroutine(SmoothRig(0, 2));
                _animator.SetLayerWeight(2, 1f);
                _gun.SetActive(true);
            }
            else
            {
                //_animator.SetTrigger("UnequipPistol");
                _isHoldingGun = false;
                StartCoroutine(SmoothRig(1, 0));
                _animator.SetLayerWeight(2, 0f);
                _gun.SetActive(false);
            }
        }
    }

    IEnumerator SmoothRig(float start, float end)
    {
        float elapsedTime = 0;
        float waitTime = 1f;

        while (elapsedTime < waitTime)
        {
            _bodyAimingConstraint.weight = Mathf.Lerp(start, end, (elapsedTime / waitTime));
            _rightHandAimingConstraint.weight = Mathf.Lerp(start, end, (elapsedTime / waitTime));
            _leftHandAimingConstraint.weight = Mathf.Lerp(start, end, (elapsedTime / waitTime));

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
