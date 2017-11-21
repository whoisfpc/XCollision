﻿using Gizmos =  UnityEngine.Gizmos;

namespace XCollision.XComponent
{
    public sealed class SphereXCollider : XCollider
    {
        public float radius;

        protected override void Awake()
        {
            col = new Core.SphereXCollider(transform.position, radius);
            XPhysicsProxy.Instance.AddCollider(col);
        }

        protected override void OnDrawGizmosSelected()
        {
            var colorBackup = Gizmos.color;

            GizmosHelper.DrawShpere(transform.position, radius, colliderColor);
            if (showBounds && col != null)
            {
                Gizmos.color = boundsColor;
                Gizmos.DrawWireCube(col.bounds.Center, col.bounds.Size);
            }

            Gizmos.color = colorBackup;
        }
    }
}
