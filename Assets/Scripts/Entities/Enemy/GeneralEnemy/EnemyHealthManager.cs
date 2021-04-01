using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azer.EntityComponents;
using System;

namespace Azer.GeneralEnemy {

    public class EnemyHealthManager : HealthManagerTemplate
    {
        private IEnemy enemy;

        private void Awake()
        {
            enemy = GetComponent<IEnemy>();
        }

        private void Start()
        {
            maxHealth = enemy.ParentEnemy.Health;
            CurrentHealth = maxHealth;
        }

        public override void Hit(int dmg)
        {
            if(dmg <= 0)
            {
                Debug.Log(dmg);
                throw new ArgumentOutOfRangeException();
            }

            if(CanHit)
            {
                if (CurrentHealth - dmg < 0)
                    CurrentHealth = 0;
                else
                {
                    CurrentHealth -= dmg;
                    if(enemy != null)
                        enemy.ParentEnemy.InAggro = true;
                }
            }
            HealthZero();
        }
    }
}