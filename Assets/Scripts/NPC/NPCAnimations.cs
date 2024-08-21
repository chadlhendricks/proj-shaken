using UnityEngine;

public class NPCAnimations : MonoBehaviour
{
    private Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartTalkingAnimation()
    {
        _animator.SetTrigger("Talk");
    }

    public void StartTypingAnimation()
    {
        _animator.SetTrigger("Talk");
    }
}
