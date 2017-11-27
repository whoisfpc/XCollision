using UnityEngine;

namespace XCollision.XComponent
{
    public sealed class CylinderXCollider : XCollider
    {
        public float radius;
        public float height;

        protected override void Awake()
        {
            col = new Core.CylinderXCollider(transform.position, radius, height, isStatic);
            col.restitution = restitution;
            col.rigidbody.Mass = mass;
            XPhysicsProxy.XPhysics.AddCollider(col);
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void DisplayBounds()
        {
            if (col != null)
            {
                GizmosHelper.DrawWireCube(col.bounds.Center, col.bounds.Size, boundsColor);
            }
        }

        protected override void DisplayCollider()
        {
            GizmosHelper.DrawCylinder(transform.position, radius, height, colliderColor);
        }
    }
}
