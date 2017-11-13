using Vector3 = UnityEngine.Vector3;

namespace XCollision.Core
{
    public abstract class XCollider
    {
        public Vector3 position;
        public XRigibody rigibody;

        public XCollider(Vector3 position)
        {
            this.position = position;
            rigibody = new XRigibody();
        }

        public void AddForce(Vector3 force)
        {
            rigibody.AddForce(force);
        }

        public void Update(float dt)
        {
            position += rigibody.velocity * dt;

            rigibody.Update(dt);
        }
    }
}
