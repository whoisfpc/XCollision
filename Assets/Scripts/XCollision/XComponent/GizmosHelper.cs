using UnityEngine;

namespace XCollision.XComponent
{
    public class GizmosHelper
    {
        public static Color defaultColor = Color.white;

        private static void DrawCircleUp(Vector3 center, float radius)
        {
            var p = center + Vector3.right * radius;
            for (int i = 0; i < 51; i++)
            {
                var theta = 2 * Mathf.PI * i / 50f;
                var t = center + new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta)) * radius;
                Gizmos.DrawLine(p, t);
                p = t;
            }
        }

        public static void DrawCircle(Vector3 center, Vector3 up, float radius)
        {
            DrawCircle(center, up, radius, defaultColor);
        }

        public static void DrawCircle(Vector3 center, Vector3 up, float radius, Color color)
        {
            Quaternion q = Quaternion.FromToRotation(Vector3.up, up.normalized);
            Matrix4x4 mat = Matrix4x4.TRS(center, q, Vector3.one);
            var colorBackup = Gizmos.color;
            var matrixBackup = Gizmos.matrix;
            Gizmos.color = color;
            Gizmos.matrix = mat;
            DrawCircleUp(Vector3.zero, radius);
            Gizmos.color = colorBackup;
            Gizmos.matrix = matrixBackup;
        }

        public static void DrawShpere(Vector3 center, float radius)
        {
            DrawShpere(center, radius, defaultColor);
        }

        public static void DrawShpere(Vector3 center, float radius, Color color)
        {
            var colorBackup = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawWireSphere(center, radius);
            var up = Camera.current.transform.position - center;
            if (Camera.current.orthographic)
            {
                DrawCircle(center, up.normalized, radius, color);
            }
            else
            {
                var d2 = up.sqrMagnitude;
                var d = up.magnitude;
                var r = Mathf.Sqrt(d2 - radius * radius) * radius / d;
                DrawCircle(center + up.normalized * (radius * radius) / d, up, r, color);
            }
            Gizmos.color = colorBackup;
        }

        public static void DrawCylinder(Vector3 center, float radius, float height, Color color)
        {
            var colorBackup = Gizmos.color;
            var matrixBackup = Gizmos.matrix;
            Gizmos.color = color;
            Gizmos.matrix = Matrix4x4.Translate(center);

            var halfH = height / 2;
            DrawCircleUp(Vector3.zero, radius);
            DrawCircleUp(Vector3.down * halfH, radius);
            DrawCircleUp(Vector3.up * halfH, radius);

            Gizmos.DrawLine(Vector3.right * radius + Vector3.down * halfH, Vector3.left * radius + Vector3.down * halfH);
            Gizmos.DrawLine(Vector3.back * radius + Vector3.down * halfH, Vector3.forward * radius + Vector3.down * halfH);
            Gizmos.DrawLine(Vector3.right * radius + Vector3.up * halfH, Vector3.left * radius + Vector3.up * halfH);
            Gizmos.DrawLine(Vector3.back * radius + Vector3.up * halfH, Vector3.forward * radius + Vector3.up * halfH);

            Gizmos.DrawLine(Vector3.right * radius + Vector3.down * halfH, Vector3.right * radius + Vector3.up * halfH);
            Gizmos.DrawLine(Vector3.forward * radius + Vector3.down * halfH, Vector3.forward * radius + Vector3.up * halfH);
            Gizmos.DrawLine(Vector3.left * radius + Vector3.down * halfH, Vector3.left * radius + Vector3.up * halfH);
            Gizmos.DrawLine(Vector3.back * radius + Vector3.down * halfH, Vector3.back * radius + Vector3.up * halfH);

            Gizmos.color = colorBackup;
            Gizmos.matrix = matrixBackup;
        }

        public static void DrawWireCube(Vector3 center, Vector3 size, Quaternion quaternion, Color color)
        {
            var matrixBackup = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(center, quaternion, Vector3.one);
            DrawWireCube(Vector3.zero, size, color);
            Gizmos.matrix = matrixBackup;
        }

        public static void DrawWireCube(Vector3 center, Vector3 size, Color color)
        {
            var colorBackup = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawWireCube(center, size);
            Gizmos.color = colorBackup;
        }
    }
}
