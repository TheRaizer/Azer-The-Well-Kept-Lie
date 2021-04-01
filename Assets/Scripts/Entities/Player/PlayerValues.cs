using UnityEngine;

namespace Assets.Scripts.Player
{
    [CreateAssetMenu(fileName = "Player", menuName = "Values")]
    public class PlayerValues : ScriptableObject
    {
        public float Horiz { get; set; }
        public float Vert { get; set; }


        [field: Header("Health")]
        [field: SerializeField] public int MaxHealth { get; private set; }


        [field: Header("Speeds")]
        [field: SerializeField] public float MoveSpeedX { get; private set; }
        [field: SerializeField] public float MoveSpeedY { get; private set; }


        [field: Header("Dash Variables")]
        [field: SerializeField] public float HorizDashSpeedX { get; private set; }
        [field: SerializeField] public float HorizDashSpeedY { get; private set; }
        [field: SerializeField] public float VertDashSpeed { get; private set; }
        [field: SerializeField] public float DiagonalDashSpeedX { get; private set; }
        [field: SerializeField] public float DiagonalDashSpeedY { get; private set; }
        [field: SerializeField] public float DashTime { get; private set; }



        [field: Header("Jump Forces")]
        [field: SerializeField] public float JumpForceX { get; private set; }
        [field: SerializeField] public float JumpForceY { get; private set; }




        [field: Header("Wall Variables")]
        [field: SerializeField] public float WallRestrictionTime { get; private set; }
        [field: SerializeField] public float WallPointRadius { get; private set; }
        [field: SerializeField] public LayerMask Wall { get; private set; }
        [field: SerializeField] public float WallJumpForceX { get; private set; }
        [field: SerializeField] public float WallJumpForceY { get; private set; }
        [field: SerializeField] public float JumpLerp { get; private set; }
        [field: SerializeField] public float WallSlideSpeed { get; private set; }




        [field: Header("Grounded Variables")]
        [field: SerializeField] public float GroundPointRadius { get; private set; }
        [field: SerializeField] public LayerMask Floor { get; private set; }



        [field: Header("Swing Dmg")]
        [field: SerializeField] public int FirstSwingDmg { get; private set; }
        [field: SerializeField] public int SecondSwingDmg { get; private set; }
        [field: SerializeField] public int ThirdSwingDmg { get; private set; }


        [field: Header("Swing Knocks")]
        [field: SerializeField] public float FirstSwingKnockX { get; private set; }
        [field: SerializeField] public float FirstSwingKnockY { get; private set; }
        [field: SerializeField] public float FirstSwingKnockTime { get; private set; }
        [field: SerializeField] public float SecondSwingKnockX { get; private set; }
        [field: SerializeField] public float SecondSwingKnockY { get; private set; }
        [field: SerializeField] public float SecondSwingKnockTime { get; private set; }
        [field: SerializeField] public float ThirdSwingKnockX { get; private set; }
        [field: SerializeField] public float ThirdSwingKnockY { get; private set; }
        [field: SerializeField] public float ThirdSwingKnockTime { get; private set; }



        [field: Header("Swing Camera Shakes")]
        [field: SerializeField] public float FirstSwingCameraShakeAmt { get; private set; }
        [field: SerializeField] public float FirstSwingCameraShakeTime { get; private set; }
        [field: SerializeField] public float SecondSwingCameraShakeAmt { get; private set; }
        [field: SerializeField] public float SecondSwingCameraShakeTime { get; private set; }
        [field: SerializeField] public float ThirdSwingCameraShakeAmt { get; private set; }
        [field: SerializeField] public float ThirdSwingCameraShakeTime { get; private set; }



        [field: Header("Jump Assists")]
        [field: SerializeField] public float CoyoteTime { get; private set; } = 0.2f;
        [field: SerializeField] public float JumpBufferTime { get; private set; } = 0.3f;
    }
}
