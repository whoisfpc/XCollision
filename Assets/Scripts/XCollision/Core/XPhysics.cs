using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;
using Mathf = UnityEngine.Mathf;

namespace XCollision.Core
{
    /// <summary>
    /// XPhysics system, contain main loop
    /// </summary>
    public class XPhysics
    {
        public Vector3 gravity = Vector3.down * 9.8f;
        public bool useGravity = true;

        // TODO: 改用linklist，区分静态和动态物体
        private IList<XCollider> colliders = new List<XCollider>();
        private List<XContact> tmpContacts = new List<XContact>();

        public void AddCollider(XCollider collider)
        {
            // TODO:使用队列，延迟加入。添加和删除碰撞体需要全部延迟到每次update前
            colliders.Add(collider);
        }

        public void RemoveCollider(XCollider collider)
        {
            throw new System.NotImplementedException();
        }

        public void Update(float dt)
        {
            tmpContacts.Clear();

            // Update velocity and position
            for (int i = 0; i < colliders.Count; i++)
            {
                if (useGravity)
                    colliders[i].AddForce(gravity * colliders[i].rigidbody.Mass);
                colliders[i].Update(dt);
            }

            // Detect collision
            for (int i = 0; i < colliders.Count; i++)
            {
                for (int j = i + 1; j < colliders.Count; j++)
                {
                    // collider detect i & j
                    if (!colliders[i].bounds.Intersects(colliders[j].bounds))
                        continue;
                    XContact? contact;
                    if (colliders[i].Intersects(colliders[j], out contact))
                    {
                        tmpContacts.Add(contact.Value);
                    }
                }
            }

            // Resolve collision
            for (int i = 0; i < tmpContacts.Count; i++)
            {
                ResolveCollision(tmpContacts[i]);
            }
        }

        private void ResolveCollision(XContact contact)
        {
            var a = contact.src;
            var b = contact.dst;
            var normal = contact.normal;
            float sv = Vector3.Dot((b.rigidbody.velocity - a.rigidbody.velocity), normal);
            if (sv > 0)
                return;
            float e = (a.restitution + b.restitution) * 0.5f;
            var tm = a.rigidbody.InverseMass + b.rigidbody.InverseMass;
            if (tm == 0)
            {
                return;
            }
            float impulse = -(1 + e) * sv / tm;
            a.rigidbody.velocity -= impulse * a.rigidbody.InverseMass * normal;
            b.rigidbody.velocity += impulse * b.rigidbody.InverseMass * normal;

            PositionalCorrection(contact);
        }

        private void PositionalCorrection(XContact contact)
        {
            var a = contact.src;
            var b = contact.dst;
            var tm = a.rigidbody.InverseMass + b.rigidbody.InverseMass;
            if (tm == 0)
            {
                return;
            }
            float percent = 0.2f;
            float slop = 0.01f;
            Vector3 correction = Mathf.Max(contact.penetration - slop, 0) / tm * percent * contact.normal;
            a.Position = a.rigidbody.position -= a.rigidbody.InverseMass * correction;
            b.Position = b.rigidbody.position += b.rigidbody.InverseMass * correction;
        }
    }
}
