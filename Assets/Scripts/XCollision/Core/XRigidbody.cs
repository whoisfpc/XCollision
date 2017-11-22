using Vector3 = UnityEngine.Vector3;

namespace XCollision.Core
{
    public class XRigidbody
    {
        public float Mass
        {
            get { return mass; }
            set { mass = value; inverseMass = 1 / mass; }
        }
        public Vector3 position;
        public Vector3 velocity;
        private float mass = 1;
        private Vector3 totalForce;
        private float inverseMass = 1;

        public XRigidbody(Vector3 position)
        {
            this.position = position;
        }

        public void AddForce(Vector3 force)
        {
            totalForce += force;
        }

        public void Update(float dt)
        {
            var acc = totalForce * inverseMass;
            velocity += acc * dt;
            position += velocity * dt;
            totalForce = Vector3.zero;
        }
    }
}
