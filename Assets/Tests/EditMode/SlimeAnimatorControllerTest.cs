using System;
using System.Collections;
using System.Collections.Generic;
using Azer.GeneralEnemy;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SlimeAnimatorControllerTest
    {
        [Test]
        public void SlimeAnimator_SlimeStateTooLow_Exception()
        {
            GameObject testObject = new GameObject();
            SlimeAnimator animator = testObject.AddComponent<SlimeAnimator>();

            Assert.Throws<ArgumentOutOfRangeException>(() => animator.SetSlimeState(-1));
        }

        [Test]
        public void SlimeAnimator_SlimeStateTooHigh_Exception()
        {
            GameObject testObject = new GameObject();
            SlimeAnimator animator = testObject.AddComponent<SlimeAnimator>();

            Assert.Throws<ArgumentOutOfRangeException>(() => animator.SetSlimeState(4));
        }
        
    }
}
