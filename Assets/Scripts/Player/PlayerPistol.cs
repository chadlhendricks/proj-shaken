using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using UnityTutorial.Manager;

public class PlayerPistol : MonoBehaviour
{
    private Animator _animator;
    private InputManager _inputManager;

    [SerializeField] private MultiAimConstraint _bodyAimingConstraint;
    [SerializeField] private MultiAimConstraint _rightHandAimingConstraint;
    [SerializeField] private TwoBoneIKConstraint _leftHandAimingConstraint;

    [SerializeField] private GameObject _cellphoneConstraint;

    [SerializeField] private GameObject _pistol;
    private bool _isHoldingGun;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _inputManager = GetComponent<InputManager>();

        _bodyAimingConstraint.weight = 0f;
        _rightHandAimingConstraint.weight = 0f;
        _leftHandAimingConstraint.weight = 0f;
        _pistol.SetActive(false);
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
                _isHoldingGun = true;
                _cellphoneConstraint.SetActive(false); // Makes sure the cellphone constraint is disabled
                _animator.SetBool("PistolEquipped", true);
                StartCoroutine(EnableGunAfterTime(0.5f)); // Enable gun after .5 seconds
                StartCoroutine(SmoothRig(0, 2)); // Set weight of rig constraints smoothly
                _animator.SetLayerWeight(2, 1f); // Set Layer weight to Pistol layer
            }
            else
            {
                _isHoldingGun = false;
                StartCoroutine(DisableGunAfterTime(2f)); // Disable gun after 2 seconds
                _animator.SetBool("PistolEquipped", false);        
                StartCoroutine(SmoothRig(1, 0)); // Set weight of rig constraints smoothly
                _animator.SetLayerWeight(2, 0f); // Set Layer weight to Pistol layer
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

    private IEnumerator EnableGunAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        _pistol.SetActive(true);
    }

    private IEnumerator DisableGunAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        _pistol.SetActive(false);
    }
}
