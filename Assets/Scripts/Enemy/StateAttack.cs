using UnityEngine;

public class StateAttack : State
{
    [SerializeField] private Animator _animator;

    public override State RunCurrentState()
    {
        Debug.Log("I have attacked");
        _animator.SetBool("InAttackRange", true);
        return this;
    }
}
