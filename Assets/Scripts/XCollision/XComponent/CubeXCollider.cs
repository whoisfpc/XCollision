using UnityEngine;

namespace XCollision.XComponent
{
    public class CubeXCollider : XCollider
    {
        public Vector3 size;

        protected override void Awake()
        {
            col = new Core.CubeXCollider(transform.position, size, transform.rotation.eulerAngles.y);
            col.restitution = restitution;
            col.rigidbody.Mass = mass;
            XPhysicsProxy.XPhysics.AddCollider(col);
        }

        protected override void Update()
        {
            base.Update();
            col.Rotation = transform.rotation.eulerAngles.y;
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
            GizmosHelper.DrawWireCube(transform.position, size, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), colliderColor);
        }
    }
}
