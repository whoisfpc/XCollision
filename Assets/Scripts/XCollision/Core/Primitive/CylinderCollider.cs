using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Mathf = UnityEngine.Mathf;

namespace XCollision.Core
{
    public class CylinderCollider : XCollider
    {
        private float radius;
        private float height;

        public CylinderCollider(Vector3 position, float radius, float height) : base(position, 0)
        {
            this.radius = radius;
            this.height = height;
            CalcBounds();
        }

        public override void CalcBounds()
        {
            bounds.Center = Position;
            bounds.Extents = new Vector3(radius, height * 0.5f, radius);
        }
    }
}
