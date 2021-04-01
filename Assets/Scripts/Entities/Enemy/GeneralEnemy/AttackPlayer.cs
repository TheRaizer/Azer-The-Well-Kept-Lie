using UnityEngine;
using Azer.EntityComponents;
using Azer.UtilityComponents;
using Azer.Player;

namespace Azer.GeneralEnemy
{
    public class AttackPlayer : MonoBehaviour
    {
        private Enemy parentEnemy;
        private CameraShake cameraShake;

        [SerializeField] private float knockBackXStrength = 0f;
        [SerializeField] private float knockBackYStrength = 0f;
        [SerializeField] private float knockTime = 0f;
        [SerializeField] private float cameraShakeAmt = 0f;
        [SerializeField] private float cameraShakeTime = 0f;

        private void Awake()
        {
            cameraShake = FindObjectOfType<CameraShake>();
        }

        private void Start()
        {
            parentEnemy = GetComponentInParent<IEnemy>().ParentEnemy;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (collision.GetComponentInParent<HealthManagerTemplate>().CanHit)
                {
                    collision.GetComponentInParent<HealthManagerTemplate>().Hit(parentEnemy.Dmg);
                    collision.GetComponentInParent<PlayerKnockBackLogic>().GroundKnock(transform, knockBackXStrength, knockBackYStrength, knockTime);

                    parentEnemy.InAggro = true;

                    cameraShake.BeginShake(cameraShakeAmt, cameraShakeTime);
                }
            }
        }
    }
}
