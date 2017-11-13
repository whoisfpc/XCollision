using Vector3 = UnityEngine.Vector3;

namespace XCollision.Core
{
    public class XRigibody
    {
        public Vector3 velocity;
        private Vector3 totalForce;

        public void AddForce(Vector3 force)
        {
            totalForce += force;
        }

        public void Update(float dt)
        {
            var acc = totalForce / 1;
            velocity += acc * dt;
            totalForce = Vector3.zero;
        }
    }
}
