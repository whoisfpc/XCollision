using Vector3 = UnityEngine.Vector3;

namespace XCollision.Core
{
    public abstract class XCollider
    {
        public Vector3 position;
        public XRigibody rigibody;
        public Bounds bounds;
        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                var old = rotation;
                rotation = value;
                if (old != rotation)
                {
                    CalcBounds();
                }
            }
        }
        private float rotation;

        public XCollider(Vector3 position, float rotation)
        {
            this.position = position;
            this.rotation = rotation;
            rigibody = new XRigibody();
            bounds = new Bounds();
        }

        public abstract void CalcBounds();

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
