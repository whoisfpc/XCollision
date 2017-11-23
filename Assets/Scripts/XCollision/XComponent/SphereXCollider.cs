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
            XPhysicsProxy.Instance.AddCollider(col);
        }

        protected override void OnDrawGizmosSelected()
        {
            GizmosHelper.DrawShpere(transform.position, radius, colliderColor);
            if (showBounds && col != null)
            {
                GizmosHelper.DrawWireCube(col.bounds.Center, col.bounds.Size, boundsColor);
            }
        }
    }
}
