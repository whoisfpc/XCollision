using Vector3 = UnityEngine.Vector3;

namespace XCollision.Core
{
    public class XRigibody
    {
        public Vector3 velocity;
        public float mass = 1;
        private Vector3 totalForce;

        public void AddForce(Vector3 force)
        {
            totalForce += force;
        }

        public void Update(float dt)
        {
            var acc = totalForce / mass;
            velocity += acc * dt;
            totalForce = Vector3.zero;
        }
    }
}
