using Vector3 = UnityEngine.Vector3;

namespace XCollision.Core
{
    public class SphereCollider : XCollider
    {
        private float radius;

        public SphereCollider(Vector3 position, float radius) : base(position, 0)
        {
            this.radius = radius;
            CalcBounds();
        }

        public override void CalcBounds()
        {
            bounds.Center = Position;
            bounds.Extents = new Vector3(radius, radius, radius);
        }
    }
}
