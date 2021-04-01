using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.EntityComponents
{
    public class CheckEntityAtWall
    {
        private readonly Transform wallCheck;
        private readonly float length;
        private readonly LayerMask wall;

        public Vector2 DirToWall { get; private set; }

        public bool AtWall { get; set; }

        public CheckEntityAtWall(Transform wallCheck, float length, LayerMask wall)
        {
            this.wallCheck = wallCheck;
            this.length = length;
            this.wall = wall;
        }

        public void WallCheck(Transform tr)
        {
            RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, Vector2.right, length, wall); 

            if (hit)
            {
                DirToWall = (tr.position - wallCheck.position).normalized;
                AtWall = true;
            }
            else
            {
                AtWall = false;
            }
        }
    }
}
