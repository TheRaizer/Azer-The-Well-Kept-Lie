using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class RaizeLeaperAnimatorTests
    {
        [Test]
        public void RaizeLeaperAnimator_IdleTickTooLow_Test()
        {
            GameObject testObject = new GameObject();
            RaizeLeaperAnimator animator = testObject.AddComponent<RaizeLeaperAnimator>();

            Assert.Throws<ArgumentOutOfRangeException>(() => animator.SetIdleTick(-1));
        }

        [Test]
        public void RaizeLeaperAnimator_IdleTickTooHigh_Test()
        {
            GameObject testObject = new GameObject();
            RaizeLeaperAnimator animator = testObject.AddComponent<RaizeLeaperAnimator>();

            Assert.Throws<ArgumentOutOfRangeException>(() => animator.SetIdleTick(3));
        }

        [Test]
        public void RaizeLeaperAnimator_LeapStageTooLow_Test()
        {
            GameObject testObject = new GameObject();
            RaizeLeaperAnimator animator = testObject.AddComponent<RaizeLeaperAnimator>();

            Assert.Throws<ArgumentOutOfRangeException>(() => animator.SetLeapStage(-1));
        }

        [Test]
        public void RaizeLeaperAnimator_LeapStageTooHigh_Test()
        {
            GameObject testObject = new GameObject();
            RaizeLeaperAnimator animator = testObject.AddComponent<RaizeLeaperAnimator>();

            Assert.Throws<ArgumentOutOfRangeException>(() => animator.SetLeapStage(3));
        }
    }
}
