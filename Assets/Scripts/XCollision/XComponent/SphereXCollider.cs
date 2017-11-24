using Gizmos =  UnityEngine.Gizmos;

namespace XCollision.XComponent
{
    public sealed class SphereXCollider : XCollider
    {
        public float radius;

        protected override void Awake()
        {
            col = new Core.SphereXCollider(transform.position, radius);
            col.restitution = restitution;
            col.rigidbody.Mass = mass;
            XPhysicsProxy.XPhysics.AddCollider(col);
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
            GizmosHelper.DrawShpere(transform.position, radius, colliderColor);
        }
    }
}
