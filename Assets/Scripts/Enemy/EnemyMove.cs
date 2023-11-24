using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Rigidbody _enemyRigidbody;
    [SerializeField] private float _moveSpeed = 0.5f;

    private void Start()
    {
        _enemyRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _enemyRigidbody.velocity = transform.forward * _moveSpeed;
    }
}
