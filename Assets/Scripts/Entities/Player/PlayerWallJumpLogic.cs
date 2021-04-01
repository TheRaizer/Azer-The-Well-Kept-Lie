using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azer.EntityComponents;

namespace Azer.Player
{
    public class PlayerWallJumpLogic : MonoBehaviour
    {
        public bool WallJumped { get; private set; }
        private bool wallIsRight = false;

        private WaitForSeconds jumpWait;

        private IJump jump;
        private Rigidbody2D rb;
        private PlayerController player;


        void Awake()
        {
            player = GetComponent<PlayerController>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            jumpWait = new WaitForSeconds(player.PlayerValues.WallRestrictionTime);
        }

        public void LerpWhenWallJumped()
        {
            if (WallJumped)
            {
                rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(player.PlayerValues.Horiz * player.PlayerValues.MoveSpeedX, rb.velocity.y)
                                                                    , player.PlayerValues.JumpLerp * Time.deltaTime);
            }
        }

        public void WallDirection()
        {
            if (player.WallCheck.DirToWall.x > 0)
            {
                wallIsRight = false;
            }
            else if (player.WallCheck.DirToWall.x < 0)
            {
                wallIsRight = true;
            }
        }

        public IEnumerator WallJumpCo()
        {
            if (!WallJumped)
            {
                WallJumped = true;

                WallDirection();

                Vector2 jumpDir = wallIsRight ? Vector2.left : Vector2.right;

                jump.JumpForceX = player.PlayerValues.WallJumpForceX * jumpDir.x;
                jump.JumpForceY = player.PlayerValues.WallJumpForceY;
                jump.Jump();

                yield return jumpWait;

                WallJumped = false;
            }
        }

        public void SetJump(IJump _jump)
        {
            jump = _jump;
        }

        public void EndJump()
        {
            WallJumped = false;
        }
    }
}
