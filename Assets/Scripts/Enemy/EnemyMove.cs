using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Transform _enemyTransform;
    private float _moveSpeed = 0.5f;

    private void Start()
    {
        _enemyTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        transform.position += Vector3.right * _moveSpeed * Time.deltaTime;
    }
}
