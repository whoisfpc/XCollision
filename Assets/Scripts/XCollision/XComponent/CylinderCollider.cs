using UnityEngine;

namespace XCollision.XComponent
{
    public sealed class CylinderCollider : XCollider
    {
        public float radius;
        public float height;

        protected override void OnDrawGizmosSelected()
        {
            var colorBackup = Gizmos.color;
            Gizmos.color = colliderColor;
            var backup = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;

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
            Gizmos.color = colorBackup;
            Gizmos.matrix = backup;
        }
    }
}
