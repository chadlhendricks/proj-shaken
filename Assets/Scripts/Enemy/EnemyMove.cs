using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private Transform _transform;
    private float _moveSpeed = 10f;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rigidBody.velocity = transform.forward * _moveSpeed;
        Debug.Log(_rigidBody.velocity);
    }
}
