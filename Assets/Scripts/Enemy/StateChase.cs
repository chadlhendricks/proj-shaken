using UnityEngine;
using UnityEngine.Animations.Rigging;

public class StateChase : State
{
    [SerializeField] private StateAttack attackState;
    [SerializeField] private StateIdle idleState;

    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _ghost;

    [SerializeField] private MultiAimConstraint _headConstraint;
    [SerializeField] private MultiAimConstraint _bodyConstraint;

    [SerializeField] private bool inAttackRange;
    [SerializeField] private float _aggroRange = 10f;
    [SerializeField] private float _speed = 1f;


    public override State RunCurrentState()
    {
        FacePlayer();
        EnemyMove();

        float distanceFromPlayer = Vector3.Distance(_player.transform.position, transform.position);
        if (distanceFromPlayer > _aggroRange)
        {
            EnemyStop();
            StopFacingPlayer();
            return idleState;
        }

        if (inAttackRange)
        {
            return attackState;
        }
        else
        {
            return this;
        }
    }

    // Faces Enemy in the direction of the player
    private void FacePlayer()
    {
        _headConstraint.weight = 1f;
        _bodyConstraint.weight = 1f;

        Vector3 direction = (_player.transform.position - _ghost.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);
        _ghost.transform.rotation = rotation;
    }

    private void StopFacingPlayer()
    {
        _headConstraint.weight = 0f;
        _bodyConstraint.weight = 0f;
    }

    private void EnemyMove()
    {
        _rigidBody.linearVelocity = transform.forward * _speed;
        _animator.SetBool("CanSeeThePlayer", true);
    }

    private void EnemyStop()
    {
        _rigidBody.linearVelocity = transform.forward * 0;
        _animator.SetBool("CanSeeThePlayer", false);
    }
}
