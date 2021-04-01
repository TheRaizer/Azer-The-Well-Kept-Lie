using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Azer.UtilityComponents;

namespace Tests
{
    public class CalculateDirToEntityTest
    {
        [Test]
        public void CalculateDirToEntityTestSimplePasses()
        {
            GameObject testObjectStart = new GameObject();
            GameObject testObjectEnd = new GameObject();

            testObjectStart.transform.position = new Vector3(-1, -1, 0);
            testObjectEnd.transform.position = new Vector3(1, 1, 0);

            CalculateDirectionToEntity calcToEntity = new CalculateDirectionToEntity();

            Assert.IsTrue(calcToEntity.DirToEntity(testObjectStart.transform, testObjectEnd.transform) == new Vector2(1, 1).normalized);
        }
    }
}
