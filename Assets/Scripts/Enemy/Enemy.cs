using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _player;

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

}