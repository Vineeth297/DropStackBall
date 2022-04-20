using UnityEngine;

public class BallStateMachine : MonoBehaviour
{
    private BallBaseState _currentState;
    
    public BallIdleJumpState idleJumpState = new BallIdleJumpState();
    public BallShootState shootState = new BallShootState();

    public Animator animator;
    
    [SerializeField] private float speed;

    private void Start()
    {
        _currentState = idleJumpState;
        _currentState.EnterState(this);
    }

    private void Update()
    {
       if(Input.GetMouseButtonDown(0))
           _currentState.UpdateState(this);
       if(Input.GetMouseButton(0))
           transform.Translate(Vector3.down * (Time.deltaTime * speed));
    }

    public void SwitchState(BallBaseState state)
    {
        _currentState = state;
        state.EnterState(this);
    }
}
