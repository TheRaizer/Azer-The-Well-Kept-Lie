using UnityEngine;
using UnityEngine.UI;

namespace Azer.EntityComponents
{
    public abstract class DrainManagerTemplate : MonoBehaviour
    {
        protected const float MAX_AMOUNT = 100f;
        [SerializeField] protected float regenAmount = 0f;
        [SerializeField] protected Image amtImage = null;

        public abstract float ReductionAmount { get; set; }
        public abstract float CurrentAmt { get; set; }
        public abstract bool InUse { get; set; }
        public abstract bool CanRegen { get; set; }

        protected void Drain()
        {
            if (InUse)
            {
                CurrentAmt -= Time.deltaTime * ReductionAmount;
                ManageImage();
            }
            else if (CurrentAmt > MAX_AMOUNT)
            {
                CurrentAmt = MAX_AMOUNT;
                ManageImage();
            }

            if (CurrentAmt > 0) return;

            ManageImage();
            CurrentAmt = 0;
            InUse = false;
        }

        protected void Regen()
        {
            if (CurrentAmt < MAX_AMOUNT && CanRegen && !InUse)
            {
                CurrentAmt += Time.deltaTime * regenAmount;
                ManageImage();
            }
        }

        public void BurstReduce(int amt)
        {
            if (CurrentAmt - amt >= 0)
            {
                CurrentAmt -= amt;
            }
            else
            {
                CurrentAmt = 0;
            }
        }

        public bool CanReduceByAmount(int amount)
        {
            return CurrentAmt - amount >= 0;
        }

        private void ManageImage()
        {
            amtImage.fillAmount = CurrentAmt / 100;
        }
    }
}
