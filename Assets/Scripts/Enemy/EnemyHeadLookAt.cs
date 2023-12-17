using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EnemyHeadLookAt : MonoBehaviour
{
    [SerializeField] private Rig _rig;
    [SerializeField] private Transform _headLookAtTransform;

    private bool _isLookingAtPosition;

    private void Start()
    {
        LookAtPosition(_headLookAtTransform.position);
    }

    private void Update()
    {
        float targetWeight = _isLookingAtPosition ? 1f : 0f;
        float lerpSpeed = 2f;
        _rig.weight = Mathf.Lerp(_rig.weight, targetWeight, Time.deltaTime * lerpSpeed);
    }

    public void LookAtPosition(Vector3 lookAtPosition)
    {
        _isLookingAtPosition = true;
        _headLookAtTransform.position = lookAtPosition;
    }
}
