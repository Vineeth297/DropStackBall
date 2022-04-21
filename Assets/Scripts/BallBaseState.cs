using UnityEngine;

public abstract class BallBaseState
{
	protected static readonly int JumpHash = Animator.StringToHash("JumpState");
    public abstract void EnterState(BallStateMachine ball);
    
    public abstract void UpdateState(BallStateMachine ball);
}
