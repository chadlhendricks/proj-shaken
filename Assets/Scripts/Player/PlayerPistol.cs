using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityTutorial.Manager;

public class PlayerPistol : MonoBehaviour
{
    [SerializeField] private GameObject _gun;
    private Animator _animator;
    private InputManager _inputManager;

    [SerializeField] private TwoBoneIKConstraint _leftHandGunConstraint;
    [SerializeField] private TwoBoneIKConstraint _rightHandGunConstraint;
    //[SerializeField] private MultiAimConstraint _rightHandGunConstraint;

    [SerializeField] private GameObject _cellphoneConstraint;

    private bool _isHoldingGun;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _inputManager = GetComponent<InputManager>();

        _leftHandGunConstraint.weight = 0f;
        _rightHandGunConstraint.weight = 0f;
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
                _cellphoneConstraint.SetActive(false);
                _isHoldingGun = true;
                _gun.SetActive(true);
                _animator.SetTrigger("EquipPistol");
                StartCoroutine(SmoothRig(0, 2));
                _animator.SetLayerWeight(2, 1f);
            }
            else
            {
                _isHoldingGun = false;
                _gun.SetActive(false);
                // Add animation putting pistol back
                _animator.SetTrigger("UnequipPistol");
                StartCoroutine(SmoothRig(1, 0));
                _animator.SetLayerWeight(2, 0f);
            }
        }
    }

    IEnumerator SmoothRig(float start, float end)
    {
        float elapsedTime = 0;
        float waitTime = 0.5f;

        while (elapsedTime < waitTime)
        {
            //_rightHandGunConstraint.weight = Mathf.Lerp(start, end, (elapsedTime / waitTime));

            _leftHandGunConstraint.weight = Mathf.Lerp(start, end, (elapsedTime / waitTime));
            _rightHandGunConstraint.weight = Mathf.Lerp(start, end, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
