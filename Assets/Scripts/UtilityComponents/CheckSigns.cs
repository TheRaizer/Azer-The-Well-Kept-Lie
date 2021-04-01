using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.UtilityComponents
{
    public class CheckSigns
    {
        public static bool SameSign(float a, float b)
        {
            return a * b >= 0.0f;
        }
    }
}