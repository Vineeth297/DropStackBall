using UnityEngine;

public class BallShootState : BallBaseState
{
    public override void EnterState(BallStateMachine ball)
    {
        ball.animator.enabled = false;
    }

    public override void UpdateState(BallStateMachine ball)
    {
        if(Input.GetMouseButtonUp(0))
            ball.SwitchState(ball.idleJumpState);
    }
}
