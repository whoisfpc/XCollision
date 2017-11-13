using Vector3 = UnityEngine.Vector3;

namespace XCollision.Core
{
    public abstract class XCollider
    {
        public Vector3 position;
        public Vector3 velocity;
        private Vector3 totalForce;

        public XCollider(Vector3 position)
        {
            this.position = position;
            totalForce = Vector3.zero;
        }

        public void AddForce(Vector3 force)
        {
            totalForce += force;
        }

        public void Update(float dt)
        {
            position += velocity * dt;

            var acc = totalForce / 1;
            velocity += acc * dt;

            totalForce = Vector3.zero;
        }
    }
}
