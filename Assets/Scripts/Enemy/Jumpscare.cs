using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTutorial.PlayerControl;

public class Jumpscare : MonoBehaviour
{
    // References
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private ScreenShake _screenShake;
    [SerializeField] private GameObject _jumpscareCamera;
    [SerializeField] private GameObject _mainCamera;
    // public AudioSource _jumpscareAudio;
    [SerializeField] private Animator _enemy;
    [SerializeField] private GameObject _mouseCursor;

    // Variables
    [SerializeField] private bool _isJumpscareCamOn;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _isJumpscareCamOn = true;

            if (_isJumpscareCamOn == true && _enemy != null)
            {
                _playerController.enabled = false;
                _jumpscareCamera.SetActive(true);
                _screenShake.Shake(3, 20);
                // _enemy.SetTrigger("Attack");
                // _enemy.SetTrigger("Scream");
                // _jumpscareAudio.Play();
            }
            else
            {
                Debug.Log("Enemy is null.");
            }
        }
    }
}