using Assets.Scripts.Player;
using System;
using UnityEngine;
using Azer.UtilityComponents;
using Azer.EntityComponents;
using Azer.States;

namespace Azer.Player
{
    public class AttackEnemy : MonoBehaviour
    {
        [SerializeField] private PlayerValues playerValues = null;
        [SerializeField] private CameraShake cameraShake = null;
        [SerializeField] private ObjectPooler pooler = null;

        private readonly float[] knockValues = new float[3];

        private PlayerSwing swing;
        private PlayerController player;

        private void Awake()
        {
            swing = GetComponentInParent<PlayerSwing>();
            player = GetComponentInParent<PlayerController>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy") && player.GetPlayerCurrentStateType() != typeof(PlayerKnockBackState))
            {
                if (swing.SwingCount == 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                pooler.SpawnObjectFromPool("HitImpact", collision.transform.position, Quaternion.identity);
                OnSwingCollision(collision);
            }
        }

        private void OnSwingCollision(Collider2D enemyCollider)
        {
            int dmg;

            switch (swing.SwingCount)
            {
                case 1:
                    knockValues[0] = playerValues.FirstSwingKnockX;
                    knockValues[1] = playerValues.FirstSwingKnockY;
                    knockValues[2] = playerValues.FirstSwingKnockTime;

                    dmg = playerValues.FirstSwingDmg;
                    cameraShake.BeginShake(playerValues.FirstSwingCameraShakeAmt, playerValues.FirstSwingCameraShakeTime);
                    break;
                case 2:
                    knockValues[0] = playerValues.SecondSwingKnockX;
                    knockValues[1] = playerValues.SecondSwingKnockY;
                    knockValues[2] = playerValues.SecondSwingKnockTime;

                    dmg = playerValues.SecondSwingDmg;
                    cameraShake.BeginShake(playerValues.SecondSwingCameraShakeAmt, playerValues.SecondSwingCameraShakeTime);
                    break;
                case 3:
                    knockValues[0] = playerValues.ThirdSwingKnockX;
                    knockValues[1] = playerValues.ThirdSwingKnockY;
                    knockValues[2] = playerValues.ThirdSwingKnockTime;

                    dmg = playerValues.ThirdSwingDmg;
                    cameraShake.BeginShake(playerValues.ThirdSwingCameraShakeAmt, playerValues.ThirdSwingCameraShakeTime);
                    break;
                default:
                    throw new ArgumentException();
            }

            enemyCollider.GetComponentInParent<HealthManagerTemplate>().Hit(dmg);
            enemyCollider.GetComponentInParent<IKnockBack>().GroundKnock(GetComponentInParent<Transform>(), knockValues[0], knockValues[1], knockValues[2]);
        }
    }
}
