using UnityEngine;
using UnityEngine.SceneManagement;

public class BallStateMachine : MonoBehaviour
{
    private BallBaseState _currentState;
    
    public BallIdleJumpState idleJumpState = new BallIdleJumpState();
    public BallShootState shootState = new BallShootState();

    public Animator animator;
    
    public float speed;

    private void Start()
    {
        _currentState = idleJumpState;
        _currentState.EnterState(this);
    }

    private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		if(Input.GetMouseButton(0))
           _currentState.UpdateState(this);
		if(Input.GetMouseButtonUp(0))
			_currentState.UpdateState(this);
	}

    public void SwitchState(BallBaseState state)
    {
        _currentState = state;
        state.EnterState(this);
    }
}
