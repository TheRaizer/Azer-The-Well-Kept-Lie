using Azer.States;
using System.Collections;
using UnityEngine;
using Azer.Player;
using Azer.EntityComponents;

public class PlayerDashState : State, IKeepEnumeratorToStop
{
    public IEnumerator PushCoroutine { get; private set; }
    private readonly PlayerController player;
    private readonly Rigidbody2D rb;

    public PlayerDashState(StateMachine _stateMachine, PlayerController _player, Rigidbody2D _rb) : base(_stateMachine)
    {
        player = _player;
        rb = _rb;
    }

    public override void Enter()
    {
        base.Enter();
        player.PlayerAnim.SetDashParam(true);

        player.DashCount++;

        if (player.PlayerValues.Vert == 0)
        {
            if (player.PlayerValues.Horiz > 0)
            {
                PushCoroutine = player.Dash.PushBothAxis(player.PlayerValues.DashTime, 1, player.PlayerValues.HorizDashSpeedX, player.PlayerValues.HorizDashSpeedY);
            }

            else if (player.PlayerValues.Horiz < 0)
            {
                PushCoroutine = player.Dash.PushBothAxis(player.PlayerValues.DashTime, -1, player.PlayerValues.HorizDashSpeedX, player.PlayerValues.HorizDashSpeedY);
            }
        }
        else if (player.PlayerValues.Vert != 0 && player.PlayerValues.Horiz != 0)
        {
            if (player.PlayerValues.Horiz > 0)
            {
                PushCoroutine = player.Dash.PushBothAxis(player.PlayerValues.DashTime, 1, player.PlayerValues.DiagonalDashSpeedX, player.PlayerValues.DiagonalDashSpeedY);
            }

            else if (player.PlayerValues.Horiz < 0)
            {
                PushCoroutine = player.Dash.PushBothAxis(player.PlayerValues.DashTime, -1, player.PlayerValues.DiagonalDashSpeedX, player.PlayerValues.DiagonalDashSpeedY);
            }
        }

        else if (player.PlayerValues.Horiz == 0 && player.PlayerValues.Vert > 0)
        {
            PushCoroutine = player.Dash.PushVert(player.PlayerValues.DashTime, player.PlayerValues.VertDashSpeed);
        }
        if (PushCoroutine != null)
            player.StartCoroutine(PushCoroutine);
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (!player.Dash.IsPushing && player.PlayerValues.Horiz == 0 && player.GroundCheck.IsGrounded)
        {
            stateMachine.ChangeState(typeof(PlayerIdleState));
        }
        else if (!player.Dash.IsPushing && player.PlayerValues.Horiz != 0 && player.GroundCheck.IsGrounded)
        {
            stateMachine.ChangeState(typeof(PlayerRunState));
        }
        else if (rb.velocity.y < 0 && !player.GroundCheck.IsGrounded)
        {
            stateMachine.ChangeState(typeof(PlayerFallingState));
        }
        else if (player.WallCheck.AtWall && player.PlayerValues.Horiz != 0 && rb.velocity.y < 0)
        {
            stateMachine.ChangeState(typeof(PlayerWallSlideState));
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (player.Dash.IsPushing && player.GroundCheck.IsGrounded)
        {
            player.Dash.EndPush();
            player.StopCoroutine(PushCoroutine);
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.PlayerAnim.SetDashParam(false);
        player.Dash.EndPush();
        player.StopCoroutine(PushCoroutine);
    }
}
