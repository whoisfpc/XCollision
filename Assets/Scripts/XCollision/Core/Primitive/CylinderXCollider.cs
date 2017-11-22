using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Mathf = UnityEngine.Mathf;

namespace XCollision.Core
{
    public class CylinderXCollider : XCollider
    {
        public float Radius { get; private set; }
        public float Height { get; private set; }

        public CylinderXCollider(Vector3 position, float radius, float height) : base(position, 0)
        {
            Radius = radius;
            Height = height;
            CalcBounds();
        }

        public override void CalcBounds()
        {
            bounds.Center = Position;
            bounds.Extents = new Vector3(Radius, Height * 0.5f, Radius);
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
