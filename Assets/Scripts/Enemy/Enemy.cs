using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private AudioManager _audioManager;

    private void Awake()
    {

    }

    private void Start()
    {
        FacePlayer();
    }

    private void FacePlayer()
    {
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
    }

    public void PlayScreamSound()
    {
        Debug.Log(_audioManager);
        _audioManager.Play("GhostScream");
    }

}