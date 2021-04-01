using Azer.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.GeneralEnemy
{
    [System.Serializable]
    public class Enemy
    {
        public enum EnemyTypes
        {
            RaizeLeaper,
            Slime,
        }

        [field: SerializeField] public int Health { get; private set; }

        [field: SerializeField] public int Dmg { get; private set; }

        [field: SerializeField] public EnemyTypes EnemyType { get; private set; }

        public bool InAggro { get; set; }

        public Enemy(int _dmg, EnemyTypes _enemyType, int _health)
        {
            Dmg = _dmg;
            EnemyType = _enemyType;
            Health = _health;
        }
    }

    public interface IEnemy
    {
        Enemy ParentEnemy { get; }
    }
}
