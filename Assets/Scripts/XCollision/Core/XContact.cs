using Vector3 = UnityEngine.Vector3;

namespace XCollision.Core
{
    public struct XContact
    {
        public XCollider src;
        public XCollider dst;
        public Vector3 normal;
        public float penetration;

        public XContact(XCollider src, XCollider dst, Vector3 normal, float penetration)
        {
            this.src = src;
            this.dst = dst;
            this.normal = normal;
            this.penetration = penetration;
        }
    }
}
