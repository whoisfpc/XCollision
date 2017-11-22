﻿using UnityEngine;

namespace XCollision.XComponent
{
    public sealed class CylinderXCollider : XCollider
    {
        public float radius;
        public float height;

        protected override void Awake()
        {
            col = new Core.CylinderXCollider(transform.position, radius, height);
            XPhysicsProxy.Instance.AddCollider(col);
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void OnDrawGizmosSelected()
        {
            GizmosHelper.DrawCylinder(transform.position, radius, height, colliderColor);

            if (showBounds && col != null)
            {
                GizmosHelper.DrawWireCube(col.bounds.Center, col.bounds.Size, boundsColor);
            }
        }
    }
}
