using UnityEngine;

public class BallIdleJumpState : BallBaseState    
{
    public override void EnterState(BallStateMachine ball)
    {
        ball.animator.enabled = true;
    }

    public override void UpdateState(BallStateMachine ball)
    {
        if(Input.GetMouseButton(0))
            ball.SwitchState(ball.shootState);
    }
}
