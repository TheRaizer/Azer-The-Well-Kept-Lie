using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Azer.EntityComponents;

namespace Tests
{
    public class EntityJumpTest
    {
        [Test]
        public void EntityJump_Test()
        {
            GameObject entity = new GameObject();
            Rigidbody2D rb = entity.AddComponent<Rigidbody2D>();
            EntityJump jump = new EntityJump(rb)
            {
                JumpForceX = 5,
                JumpForceY = 9
            };

            jump.Jump();
            Assert.IsTrue(rb.velocity == new Vector2(5, 9));
        }

        [Test]
        public void EntityJump_ActionTest()
        {
            GameObject entity = new GameObject();
            Rigidbody2D rb = entity.AddComponent<Rigidbody2D>();
            EntityJump jump = new EntityJump(rb)
            {
                JumpForceX = 5,
                JumpForceY = 9
            };

            bool test = false;

            jump.OnJump = () => test = true;
            jump.Jump();

            Assert.IsTrue(test == true);
        }
    }
}
