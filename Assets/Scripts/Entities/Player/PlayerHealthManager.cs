using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Azer.EntityComponents;

namespace Azer.Player
{
    public class PlayerHealthManager : HealthManagerTemplate
    {
        public override void Hit(int dmg)
        {
            if(dmg <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (CanHit)
            {
                CanHit = false;

                if (CurrentHealth - dmg < 0)
                    CurrentHealth = 0;
                else
                    CurrentHealth -= dmg;
            }
            HealthZero();
        }
    }
}
