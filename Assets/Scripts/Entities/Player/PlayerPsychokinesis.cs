using UnityEngine;
using Azer.EntityComponents;

namespace Azer.Player
{
    public class PlayerPsychokinesis : DrainManagerTemplate
    {
        [SerializeField] private LayerMask moveableObjects = 1 << 1;
        [field: SerializeField] public bool CanPushObject { get; set; }

        public override float ReductionAmount { get; set; }
        public override float CurrentAmt { get; set; }
        public override bool InUse { get; set; }
        public override bool CanRegen { get; set; } = true;

        public bool CanUse { get; set; } = true;

        private IPsychoKinesisControllable MoveToMouseKinesis;
        private MouseInScreenBounds mouseInBounds;

        private void Awake()
        {
            CurrentAmt = MAX_AMOUNT;
            mouseInBounds = FindObjectOfType<MouseInScreenBounds>();
        }

        private void Update()
        {
            Drain();
            Regen();
        }

        private void LateUpdate()
        {
            AbilityInput();

            if (MoveToMouseKinesis == null) return;

            if (!InUse && MoveToMouseKinesis.Follow)
            {
                MoveToMouseKinesis.StopFollowing();
            }
        }

        public void ForcePushObject()
        {
            if (InUse && CanPushObject)
            {
                CurrentAmt = 0;
                InUse = false;

                MoveToMouseKinesis.PushThroughKinesis();
            }
        }
        public void AbilityInput()
        {
            if (CanUse)
            {
                if (Input.GetKeyDown(KeyCode.Mouse4) || Input.GetKeyDown(KeyCode.LeftControl))
                {
                    ForcePushObject();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1, moveableObjects);

                    if (hit)
                    {
                        InUse = true;
                        MoveToMouseKinesis = hit.collider.GetComponent<IPsychoKinesisControllable>();

                        ReductionAmount = MoveToMouseKinesis.ReductionAmt;
                        MoveToMouseKinesis.StartFollowing();
                    }
                }
                if (!Input.GetMouseButtonUp(1) && mouseInBounds.IsMouseOverGameWindow || MoveToMouseKinesis == null) return;

                InUse = false;
                MoveToMouseKinesis.StopFollowing();
            }
        }
    }
}
