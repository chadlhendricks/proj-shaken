using UnityEngine;

public class StateIdle : State
{
    [SerializeField] private StateChase chaseState;

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _player;

    [SerializeField] private float _aggroRange = 10f;
    [SerializeField] private bool canSeeThePlayer;


    public override State RunCurrentState()
    {
        float distanceFromPlayer = Vector3.Distance(_player.transform.position, transform.position);

        if (distanceFromPlayer < _aggroRange)
        {
            // Debug.Log("Can see the player");
            return chaseState;
        } 
        else
        {
            // Debug.Log("Can't see the player");
            return this;
        }
    }
}
