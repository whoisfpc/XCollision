using UnityEngine;

namespace XCollision.XComponent
{
    public class CubeXCollider : XCollider
    {
        public Vector3 size;

        protected override void Start()
        {
            col = new Core.CubeCollider(transform.position, size, transform.rotation.eulerAngles.y*Mathf.Deg2Rad);
            XPhysicsProxy.Instance.AddCollider(col);
        }

        protected override void Update()
        {
            base.Update();
            col.Rotation = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        }

        protected override void OnDrawGizmosSelected()
        {
            var colorBackup = Gizmos.color;
            Gizmos.color = colliderColor;
            var backup = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.Rotate(Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));

            Gizmos.DrawWireCube(transform.position, size);

            if (showBounds && col != null)
            {
                Gizmos.matrix = Matrix4x4.identity;
                Gizmos.color = boundsColor;
                Gizmos.DrawWireCube(col.bounds.Center, col.bounds.Size);
            }
            Gizmos.color = colorBackup;
            Gizmos.matrix = backup;
        }
    }
}
