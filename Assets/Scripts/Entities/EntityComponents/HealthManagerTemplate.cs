using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

namespace Azer.EntityComponents 
{ 
    public abstract class HealthManagerTemplate : MonoBehaviour
    {
        [field: SerializeField]
        public bool CanHit { get; set; } = true;

        [SerializeField] protected int maxHealth = 0;

        public int CurrentHealth { get; protected set; } = 0;

        public Action OnDeath { get; set; }

        void Awake()
        {
            CurrentHealth = maxHealth;
        }

        public abstract void Hit(int dmg);

        protected void HealthZero()
        {
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                OnDeath();
            }
        }

        public void InstaKill()
        {
            CurrentHealth = 0;
            OnDeath();
        }

        public void Heal(int amt)
        {
            if(CurrentHealth <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            if(CurrentHealth + amt > maxHealth)
            {
                CurrentHealth = maxHealth;
            }
            else
                CurrentHealth += amt;
        }
        public void MaxHeal() => CurrentHealth = maxHealth;


#if UNITY_EDITOR
        public void SetMaxHealth()
        {
            maxHealth = 20;
        }
#endif
    }
}


