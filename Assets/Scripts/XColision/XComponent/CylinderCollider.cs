using UnityEngine;

namespace XColision.XComponent
{
    public sealed class CylinderCollider : XCollider
    {
        public float radius;
        public float height;

        protected override void OnDrawGizmosSelected()
        {
            var colorBackup = Gizmos.color;
            Gizmos.color = Color.blue;
            var backup = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;
            GizmosHelper.DrawCircle(Vector3.zero, radius);
            GizmosHelper.DrawCircle(Vector3.up * height / 2, radius);
            GizmosHelper.DrawCircle(Vector3.up * height, radius);


            Gizmos.DrawLine(Vector3.right * radius, Vector3.left * radius);
            Gizmos.DrawLine(Vector3.back * radius, Vector3.forward * radius);
            Gizmos.DrawLine(Vector3.right * radius + Vector3.up * height, Vector3.left * radius + Vector3.up * height);
            Gizmos.DrawLine(Vector3.back * radius + Vector3.up * height, Vector3.forward * radius + Vector3.up * height);

            Gizmos.DrawLine(Vector3.right * radius, Vector3.right * radius + Vector3.up * height);
            Gizmos.DrawLine(Vector3.forward * radius, Vector3.forward * radius + Vector3.up * height);
            Gizmos.DrawLine(Vector3.left * radius, Vector3.left * radius + Vector3.up * height);
            Gizmos.DrawLine(Vector3.back * radius, Vector3.back * radius + Vector3.up * height);
            Gizmos.color = colorBackup;
            Gizmos.matrix = backup;
        }
    }
}
