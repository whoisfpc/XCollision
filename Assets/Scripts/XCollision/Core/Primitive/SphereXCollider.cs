using Vector3 = UnityEngine.Vector3;

namespace XCollision.Core
{
    public class SphereXCollider : XCollider
    {
        public float Radius { get; private set; }

        public SphereXCollider(Vector3 position, float radius, bool isStatic = false) : base(position, 0, isStatic)
        {
            Radius = radius;
            CalcBounds();
        }

        public override void CalcBounds()
        {
            bounds.Center = Position;
            bounds.Extents = new Vector3(Radius, Radius, Radius);
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
