using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Mathf = UnityEngine.Mathf;
using Quaternion = UnityEngine.Quaternion;

namespace XCollision.Core
{
    public class CubeXCollider : XCollider
    {

        public Vector3 Size { get; private set; }

        public CubeXCollider(Vector3 position, Vector3 size, bool isStatic = false) : this(position, size, 0, isStatic)
        {
        }

        public CubeXCollider(Vector3 position, Vector3 size, float rotation, bool isStatic = false) : base(position, rotation, isStatic)
        {
            Size = size;
            CalcBounds();
        }

        public override void CalcBounds()
        {
            bounds.Center = Vector3.zero;
            bounds.Size = Size;
            var minY = -Size.y / 2;
            var maxY = Size.y / 2;
            var minX = float.MaxValue;
            var maxX = float.MinValue;
            var minZ = float.MaxValue;
            var maxZ = float.MinValue;
            var points = new Vector3[]
            {
                new Vector3(Size.x / 2, 0, Size.z / 2),
                new Vector3(-Size.x / 2, 0, -Size.z / 2),
                new Vector3(-Size.x / 2, 0, Size.z / 2),
                new Vector3(Size.x / 2, 0, -Size.z / 2)
            };
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = Quaternion * points[i];
                minX = Mathf.Min(minX, points[i].x);
                maxX = Mathf.Max(maxX, points[i].x);
                minZ = Mathf.Min(minZ, points[i].z);
                maxZ = Mathf.Max(maxZ, points[i].z);
            }
            bounds.Min = new Vector3(minX, minY, minZ);
            bounds.Max = new Vector3(maxX, maxY, maxZ);
            bounds.Center = Position;
        }

        public override bool Intersects(XCollider other, out XContact? contact)
        {
            if (other is CubeXCollider)
            {
                return ColliderIntersectHelper.Intersect(this, (CubeXCollider)other, out contact);
            }
            if (other is CylinderXCollider)
            {
                return ColliderIntersectHelper.Intersect(this, (CylinderXCollider)other, out contact);
            }
            if (other is SphereXCollider)
            {
                return ColliderIntersectHelper.Intersect(this, (SphereXCollider)other, out contact);
            }
            contact = null;
            return false;
        }
    }
}
