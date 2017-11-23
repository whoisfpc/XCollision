using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

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
        // Rotation around Y axis, degree
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
                    Quaternion = Quaternion.Euler(0, rotation, 0);
                    CalcBounds();
                }
            }
        }
        public Quaternion Quaternion { get; private set; }

        public XRigidbody rigidbody;
        public Bounds bounds;
        public float restitution;
        private Vector3 position;
        private float rotation;

        public XCollider(Vector3 position, float rotation)
        {
            this.position = position;
            this.rotation = rotation;
            Quaternion = Quaternion.Euler(0, rotation, 0);
            rigidbody = new XRigidbody(position);
            bounds = new Bounds();
            restitution = 0;
        }

        public abstract void CalcBounds();

        public abstract bool Intersects(XCollider other, out XContact? contact);

        public void AddForce(Vector3 force)
        {
            rigidbody.AddForce(force);
        }

        public void Update(float dt)
        {
            rigidbody.Update(dt);
            bounds.Center = position = rigidbody.position;
        }
    }
}
