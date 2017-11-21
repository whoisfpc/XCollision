using Vector3 = UnityEngine.Vector3;

namespace XCollision.Core
{
    public abstract class XCollider
    {
        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                bounds.Center = position;
            }
        }
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

        public XRigibody rigibody;
        public Bounds bounds;
        private Vector3 position;
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
            Position += rigibody.velocity * dt;

            rigibody.Update(dt);
        }
    }
}
