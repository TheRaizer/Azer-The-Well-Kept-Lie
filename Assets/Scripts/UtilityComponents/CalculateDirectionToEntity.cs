using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.UtilityComponents
{
    public class CalculateDirectionToEntity
    {
        public Vector2 DirToEntity(Transform start, Transform entityEndPoint)
        {
            return ((Vector2)entityEndPoint.position - (Vector2)start.position).normalized;
        }
    }
}
