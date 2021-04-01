using Azer.EntityComponents;
using Azer.States;
using UnityEngine;
using Azer.Player;
using Assets.Scripts.Player;

public class PlayerMoveableState : State
{
    private readonly PlayerController player;
    private readonly Rigidbody2D rb;

    public PlayerMoveableState(StateMachine _stateMachine, PlayerController _player, Rigidbody2D _rb) : base(_stateMachine)
    {
        rb = _rb;
        player = _player;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.FlipSprite.FlipTransformScale(player.PlayerValues.Horiz);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        rb.velocity = new Vector2(player.PlayerValues.Horiz * player.PlayerValues.MoveSpeedX, rb.velocity.y);
    }
}
