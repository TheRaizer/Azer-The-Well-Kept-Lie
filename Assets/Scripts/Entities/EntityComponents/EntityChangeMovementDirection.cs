using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.EntityComponents
{
    public class EntityChangeMovementDirection
    {
        private int normalizedDirX;

        public int ChangeSpeedToDirection(Vector2 dir)
        {
            if (dir.x != 0)
            {
                if (dir.x > 0)
                {
                    normalizedDirX = 1;
                }
                else if (dir.x < 0)
                {
                    normalizedDirX = -1;
                }
            }

            return normalizedDirX;//multiply this returned value by the Mathf.Abs(current X speed) of the entity using this.
        }
    }
}
