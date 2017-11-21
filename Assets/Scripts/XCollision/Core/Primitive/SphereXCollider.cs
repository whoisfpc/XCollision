using Vector3 = UnityEngine.Vector3;

namespace XCollision.Core
{
    public class SphereXCollider : XCollider
    {
        private float radius;

        public SphereXCollider(Vector3 position, float radius) : base(position, 0)
        {
            this.radius = radius;
            CalcBounds();
        }

        public override void CalcBounds()
        {
            bounds.Center = Position;
            bounds.Extents = new Vector3(radius, radius, radius);
        }

        public override void Intersects(XCollider other)
        {
            if (other is CubeXCollider)
            {
                ColliderIntersectHelper.Intersect(this, (CubeXCollider)other);
            }
            if (other is CylinderXCollider)
            {
                ColliderIntersectHelper.Intersect(this, (CylinderXCollider)other);
            }
            if (other is SphereXCollider)
            {
                ColliderIntersectHelper.Intersect(this, (SphereXCollider)other);
            }
        }
    }
}
