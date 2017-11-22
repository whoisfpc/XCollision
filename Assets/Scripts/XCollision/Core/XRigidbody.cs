using Vector3 = UnityEngine.Vector3;

namespace XCollision.Core
{
    public class XRigidbody
    {
        public float Mass
        {
            get { return mass; }
            set { mass = value; InverseMass = mass == 0 ? 0 : 1 / mass; }
        }
        public float InverseMass { get; private set; }
        public Vector3 position;
        public Vector3 velocity;
        private float mass = 1;
        private Vector3 totalForce;

        public XRigidbody(Vector3 position)
        {
            this.position = position;
            InverseMass = 1;
        }

        public void AddForce(Vector3 force)
        {
            totalForce += force;
        }

        public void Update(float dt)
        {
            var acc = totalForce * InverseMass;
            velocity += acc * dt;
            position += velocity * dt;
            totalForce = Vector3.zero;
        }
    }
}
