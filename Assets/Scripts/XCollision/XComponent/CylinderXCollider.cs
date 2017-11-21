using UnityEngine;

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
            var colorBackup = Gizmos.color;
            Gizmos.color = colliderColor;
            var backup = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.Translate(transform.position);

            var halfH = height / 2;
            GizmosHelper.DrawCircle(Vector3.zero, radius);
            GizmosHelper.DrawCircle(Vector3.down * halfH, radius);
            GizmosHelper.DrawCircle(Vector3.up * halfH, radius);

            Gizmos.DrawLine(Vector3.right * radius + Vector3.down * halfH, Vector3.left * radius + Vector3.down * halfH);
            Gizmos.DrawLine(Vector3.back * radius + Vector3.down * halfH, Vector3.forward * radius + Vector3.down * halfH);
            Gizmos.DrawLine(Vector3.right * radius + Vector3.up * halfH, Vector3.left * radius + Vector3.up * halfH);
            Gizmos.DrawLine(Vector3.back * radius + Vector3.up * halfH, Vector3.forward * radius + Vector3.up * halfH);

            Gizmos.DrawLine(Vector3.right * radius + Vector3.down * halfH, Vector3.right * radius + Vector3.up * halfH);
            Gizmos.DrawLine(Vector3.forward * radius + Vector3.down * halfH, Vector3.forward * radius + Vector3.up * halfH);
            Gizmos.DrawLine(Vector3.left * radius + Vector3.down * halfH, Vector3.left * radius + Vector3.up * halfH);
            Gizmos.DrawLine(Vector3.back * radius + Vector3.down * halfH, Vector3.back * radius + Vector3.up * halfH);

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
