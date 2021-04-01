using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Azer.Player;
using System;

namespace Tests
{
    public class PlayerAnimatorControllerTests
    {
        [Test]
        public void PlayerAnimator_SwingCountTooLow_Exception()
        {
            GameObject testObject = new GameObject();
            PlayerAnimator animator = testObject.AddComponent<PlayerAnimator>();

            Assert.Throws<ArgumentOutOfRangeException>(() => animator.SetSwingCountParam(-1));
        }

        [Test]
        public void PlayerAnimator_SwingCountTooHigh_Exception()
        {
            GameObject testObject = new GameObject();
            PlayerAnimator animator = testObject.AddComponent<PlayerAnimator>();

            Assert.Throws<ArgumentOutOfRangeException>(() => animator.SetSwingCountParam(4));
        }
    }
}
