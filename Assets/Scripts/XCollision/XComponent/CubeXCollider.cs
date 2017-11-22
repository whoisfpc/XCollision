using UnityEngine;

namespace XCollision.XComponent
{
    public class CubeXCollider : XCollider
    {
        public Vector3 size;
        public float mass;

        protected override void Awake()
        {
            col = new Core.CubeXCollider(transform.position, size, transform.rotation.eulerAngles.y*Mathf.Deg2Rad);
            col.rigidbody.Mass = mass;
            XPhysicsProxy.Instance.AddCollider(col);
        }

        protected override void Update()
        {
            base.Update();
            col.Rotation = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        }

        protected override void OnDrawGizmosSelected()
        {
            GizmosHelper.DrawWireCube(transform.position, size, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), colliderColor);
            if (showBounds && col != null)
            {
                GizmosHelper.DrawWireCube(col.bounds.Center, col.bounds.Size, boundsColor);
            }
        }
    }
}
