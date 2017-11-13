using UnityEngine;

namespace XCollision.XComponent
{
    public class GizmosHelper
    {
        public static Color defaultColor = Color.white;

        public static void DrawCircle(Vector3 center, float radius)
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

        public static void DrawShpere(Vector3 center, float radius)
        {
            DrawShpere(center, radius, defaultColor);
        }

        public static void DrawShpere(Vector3 center, float radius, Color color)
        {
            var up = Camera.current.transform.position - center;
            var d2 = up.sqrMagnitude;
            var d = up.magnitude;
            var r = Mathf.Sqrt(d2 - radius * radius) * radius / d;

            Gizmos.DrawWireSphere(center, radius);
            DebugExtension.DrawCircle(center + up.normalized * (radius * radius) / d, up, color, r);
        }
    }
}
