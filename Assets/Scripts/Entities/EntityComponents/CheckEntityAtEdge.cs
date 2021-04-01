using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Azer.EntityComponents
{ 
    public class CheckEntityAtEdge
    {
        private readonly Transform edgePoint;
        private readonly float distance;
        private readonly LayerMask floor;

        public bool AtEdge { get; set; }

        public CheckEntityAtEdge(Transform edgePoint, float distance, LayerMask floor)
        {
            this.edgePoint = edgePoint;
            this.distance = distance;
            this.floor = floor;
        } 

        public void EdgeCheck()
        {
            RaycastHit2D hit = Physics2D.Raycast(edgePoint.position, Vector2.down, distance, floor);

            if (!hit)
            {
                AtEdge = true;
            }
            else
            {
                AtEdge = false;
            }
        }
    }
}
