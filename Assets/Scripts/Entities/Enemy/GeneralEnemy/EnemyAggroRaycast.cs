using Azer.EntityComponents;
using Azer.UtilityComponents;
using UnityEngine;

namespace Azer.GeneralEnemy
{
    public class EnemyAggroRaycast : MonoBehaviour
    {
        [SerializeField] private float wideDistance = 0f;
        [SerializeField] private float middleDistance = 0f;

        [SerializeField] private Transform topRange = null;
        [SerializeField] private Transform bottomRange = null;
        [SerializeField] private Transform middleRange = null;

        [SerializeField] private LayerMask hittable = 1 << 1;
        [SerializeField] private FlipSprite flipSprite = null;

        private CalculateDirectionToEntity calculateDir;

        private bool manualOverride = false;

        private Vector2 topDir;
        private Vector2 midDir;
        private Vector2 bottomDir;
        private Vector2 topDirFlipped;
        private Vector2 midDirFlipped;
        private Vector2 bottomDirFlipped;

        private void Awake()
        {
            calculateDir = new CalculateDirectionToEntity();

            topDir = calculateDir.DirToEntity(transform, topRange);
            midDir = calculateDir.DirToEntity(transform, middleRange);
            bottomDir = calculateDir.DirToEntity(transform, bottomRange);

            topDirFlipped = new Vector2(-topDir.x, topDir.y);
            midDirFlipped = new Vector2(-midDir.x, midDir.y);
            bottomDirFlipped = new Vector2(-bottomDir.x, bottomDir.y);
        }

        public void RaycastAggroRange()
        {
            RaycastHit2D hitTop;
            RaycastHit2D hitMid;
            RaycastHit2D hitBottom;

            if (!manualOverride)
            {
                if (flipSprite.FacingRight)
                {
                    hitTop = Physics2D.Raycast(transform.position, topDir, wideDistance, hittable);
                    hitMid = Physics2D.Raycast(transform.position, midDir, middleDistance, hittable);
                    hitBottom = Physics2D.Raycast(transform.position, bottomDir, wideDistance, hittable);

                    Debug.DrawRay(transform.position, topDir * wideDistance, Color.red);
                    Debug.DrawRay(transform.position, midDir * middleDistance, Color.red);
                    Debug.DrawRay(transform.position, bottomDir * wideDistance, Color.red);
                }
                else
                {
                    hitTop = Physics2D.Raycast(transform.position, topDirFlipped, wideDistance, hittable);
                    hitMid = Physics2D.Raycast(transform.position, midDirFlipped, middleDistance, hittable);
                    hitBottom = Physics2D.Raycast(transform.position, bottomDirFlipped, wideDistance, hittable);

                    Debug.DrawRay(transform.position, topDirFlipped * wideDistance, Color.red);
                    Debug.DrawRay(transform.position, midDirFlipped * middleDistance, Color.red);
                    Debug.DrawRay(transform.position, bottomDirFlipped * wideDistance, Color.red);
                }
            }
            else
            {
                hitTop = Physics2D.Raycast(transform.position, calculateDir.DirToEntity(transform, topRange), wideDistance, hittable);
                hitMid = Physics2D.Raycast(transform.position, calculateDir.DirToEntity(transform, middleRange), middleDistance, hittable);
                hitBottom = Physics2D.Raycast(transform.position, calculateDir.DirToEntity(transform, bottomRange), wideDistance, hittable);

                Debug.DrawRay(transform.position, calculateDir.DirToEntity(transform, topRange) * wideDistance, Color.red);
                Debug.DrawRay(transform.position, calculateDir.DirToEntity(transform, middleRange) * middleDistance, Color.red);
                Debug.DrawRay(transform.position, calculateDir.DirToEntity(transform, bottomRange) * wideDistance, Color.red);
            }


            if (hitTop)
            {
                if (hitTop.collider.CompareTag("Player"))
                {
                    GetComponentInParent<IAggroRange>().OnEnter();
                }
            }
            else if(hitMid)
            {
                if(hitMid.collider.CompareTag("Player"))
                {
                    GetComponentInParent<IAggroRange>().OnEnter();
                }
            }
            else if(hitBottom)
            {
                if (hitBottom.collider.CompareTag("Player"))
                {
                    GetComponentInParent<IAggroRange>().OnEnter();
                }
            }
        }

        private void Update()
        {
            RaycastAggroRange();
        }

        public void ManuallyOverride()
        {
            manualOverride = true;
        }
        public void UndoOverride()
        {
            manualOverride = false;
        }
    }

    public interface IAggroRange
    {
        void OnEnter();
        void OnExit();
    }
}
