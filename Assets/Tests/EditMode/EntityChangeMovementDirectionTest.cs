using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Azer.EntityComponents;

namespace Tests
{
    public class EntityChangeMovementDirectionTest
    {
        [Test]
        public void ReturnedNormalizedDirection_PositiveXTest()
        {
            EntityChangeMovementDirection changeDirection = new EntityChangeMovementDirection();

            Vector2 testVector = new Vector2(5, 1);

            Assert.IsTrue(changeDirection.ChangeSpeedToDirection(testVector) == 1);
        }

        [Test]
        public void ReturnedNormalizedDirection_NegativeXTest()
        {
            EntityChangeMovementDirection changeDirection = new EntityChangeMovementDirection();

            Vector2 testVector = new Vector2(-5, 1);

            Assert.IsTrue(changeDirection.ChangeSpeedToDirection(testVector) == -1);
        }
    }
}
