using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Azer.EntityComponents;

namespace Tests
{
    public class FlipSpriteTest
    {
        [Test]
        public void FlipSprite_Test()
        {
            GameObject gameObject = new GameObject();

            FlipSprite sprite = gameObject.AddComponent<FlipSprite>();
            sprite.FlipTransformScale(-1);


            Assert.IsTrue(gameObject.transform.localScale.x == -1);
            Assert.IsTrue(!sprite.FacingRight);
        }
    }
}
