using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Mathf = UnityEngine.Mathf;

namespace XCollision.Core
{
    public class CubeCollider : XCollider
    {

        private Vector3 size;

        public CubeCollider(Vector3 position, Vector3 size) : this(position, size, 0)
        {
        }

        public CubeCollider(Vector3 position, Vector3 size, float rotation) : base(position, rotation)
        {
            this.size = size;
            CalcBounds();
        }

        public override void CalcBounds()
        {
            bounds.Center = Vector3.zero;
            bounds.Size = size;
            var minY = -size.y / 2;
            var maxY = size.y / 2;
            var minX = float.MaxValue;
            var maxX = float.MinValue;
            var minZ = float.MaxValue;
            var maxZ = float.MinValue;
            var points = new Vector2[]
            {
                new Vector2(size.x / 2, size.z / 2),
                new Vector2(-size.x / 2, -size.z / 2),
                new Vector2(-size.x / 2, size.z / 2),
                new Vector2(size.x / 2, -size.z / 2)
            };
            // clockwise rotate, same with unity behavior
            var rotateU = new Vector2(Mathf.Cos(-Rotation), -Mathf.Sin(-Rotation));
            var rotateV = new Vector2(Mathf.Sin(-Rotation), Mathf.Cos(-Rotation));
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new Vector2(
                    Vector2.Dot(rotateU, points[i]),
                    Vector2.Dot(rotateV, points[i])
                    );
                minX = Mathf.Min(minX, points[i].x);
                maxX = Mathf.Max(maxX, points[i].x);
                minZ = Mathf.Min(minZ, points[i].y);
                maxZ = Mathf.Max(maxZ, points[i].y);
            }
            bounds.Min = new Vector3(minX, minY, minZ);
            bounds.Max = new Vector3(maxX, maxY, maxZ);
            bounds.Center = position;
        }
    }
}
