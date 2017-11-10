using UnityEngine;

namespace XColision.XComponent
{
    public class GizmosHelper
    {
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
    }
}
