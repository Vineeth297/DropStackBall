using System.Runtime.InteropServices;
using UnityEngine;

public class BallIdleJumpState : BallBaseState    
{
    public override void EnterState(BallStateMachine ball)
	{
		ball.animator.SetBool(JumpHash, false);
	}

	public override void UpdateState(BallStateMachine ball)
    {
		if(Input.GetMouseButtonDown(0))
            ball.SwitchState(ball.shootState);
    }
}
