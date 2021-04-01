using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Azer.EntityComponents;
using System;
using Azer.Player;
using Azer.GeneralEnemy;

namespace Tests
{
    public class HealthManagerTests
    {
        [Test]
        public void Player_HealthManager_HitNegative_Exception()
        {
            PlayerHealthManager healthManager = new GameObject().AddComponent<PlayerHealthManager>();
            healthManager.SetMaxHealth();

            Assert.Throws<ArgumentOutOfRangeException>(() => healthManager.Hit(-1));
        }

        [Test]
        public void Player_HealthManager_HitTest()
        {
            PlayerHealthManager healthManager = new GameObject().AddComponent<PlayerHealthManager>();
            healthManager.SetMaxHealth();
            healthManager.MaxHeal();

            int maxHealth = healthManager.CurrentHealth;

            healthManager.Hit(1);

            Assert.IsTrue(healthManager.CurrentHealth == maxHealth - 1);
        }

        [Test]
        public void Enemy_HealthManager_HitTest()
        {
            EnemyHealthManager healthManager = new GameObject().AddComponent<EnemyHealthManager>();
            healthManager.SetMaxHealth();
            healthManager.MaxHeal();

            int maxHealth = healthManager.CurrentHealth;

            healthManager.Hit(2);

            Assert.IsTrue(healthManager.CurrentHealth == maxHealth - 2);
        }

        [Test]
        public void Enemy_HealthManager_HitNegative_Exception()
        {
            EnemyHealthManager healthManager = new GameObject().AddComponent<EnemyHealthManager>();
            healthManager.SetMaxHealth();

            Assert.Throws<ArgumentOutOfRangeException>(() => healthManager.Hit(-1));
        }

        [Test]
        public void HealthManager_HealTest()
        {
            PlayerHealthManager healthManager = new GameObject().AddComponent<PlayerHealthManager>();
            healthManager.SetMaxHealth();
            healthManager.MaxHeal();

            healthManager.Hit(1);

            int tempHealth = healthManager.CurrentHealth;

            healthManager.Heal(1);

            Assert.IsTrue(healthManager.CurrentHealth == tempHealth + 1);
        }

        [Test]
        public void HealthManager_HealWithMaxHealthTest()
        {
            PlayerHealthManager healthManager = new GameObject().AddComponent<PlayerHealthManager>();
            healthManager.SetMaxHealth();
            healthManager.MaxHeal();

            int maxHealth = healthManager.CurrentHealth;

            healthManager.Heal(1);

            Assert.IsTrue(healthManager.CurrentHealth == maxHealth);
        }

        [Test]
        public void HealthManager_HealNegative_Exception()
        {
            PlayerHealthManager healthManager = new GameObject().AddComponent<PlayerHealthManager>();
            healthManager.SetMaxHealth();

            Assert.Throws<ArgumentOutOfRangeException>(() => healthManager.Heal(0));
        }
    }
}
