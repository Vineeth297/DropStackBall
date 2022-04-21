using Unity.VisualScripting;
using UnityEngine;

public class BallShootState : BallBaseState
{
    public override void EnterState(BallStateMachine ball)
    {
		ball.animator.SetBool(JumpHash,true);
    }
	
	public override void UpdateState(BallStateMachine ball)
	{
		if (Input.GetMouseButton(0)) 
			ball.transform.Translate(Vector3.down * (Time.deltaTime * ball.speed));
		
		if(Input.GetMouseButtonUp(0))
			ball.SwitchState(ball.idleJumpState);
	}
}
