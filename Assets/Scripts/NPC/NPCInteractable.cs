using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour
{
    private Animator _animator;
    private NPCHeadLookAt _npcHeadLookAt;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _npcHeadLookAt = GetComponent<NPCHeadLookAt>();
    }

    public void Interact(Transform interactorTransform)
    {
        Debug.Log("Interact");
        float playerHeight = 2f;

        _animator.SetTrigger("Talk");
        _npcHeadLookAt.LookAtPosition(interactorTransform.position + Vector3.up * playerHeight);
    }   
}
