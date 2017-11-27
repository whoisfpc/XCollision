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
        private IList<XCollider> dynamicColliders = new List<XCollider>();
        private IList<XCollider> staticColliders = new List<XCollider>();
        private List<XContact> tmpContacts = new List<XContact>();

        public void AddCollider(XCollider collider)
        {
            // TODO:使用队列，延迟加入。添加和删除碰撞体需要全部延迟到每次update前
            if (collider.rigidbody.IsStatic)
                staticColliders.Add(collider);
            else
                dynamicColliders.Add(collider);
        }

        public void RemoveCollider(XCollider collider)
        {
            throw new System.NotImplementedException();
        }

        public void Update(float dt)
        {
            tmpContacts.Clear();

            // Update velocity and position
            for (int i = 0; i < dynamicColliders.Count; i++)
            {
                if (useGravity)
                    dynamicColliders[i].AddForce(gravity * dynamicColliders[i].rigidbody.Mass);
                dynamicColliders[i].Update(dt);
            }

            // Detect collision
            for (int i = 0; i < dynamicColliders.Count; i++)
            {
                for (int j = i + 1; j < dynamicColliders.Count; j++)
                {
                    // collider detect i & j
                    if (!dynamicColliders[i].bounds.Intersects(dynamicColliders[j].bounds))
                        continue;
                    XContact? contact;
                    if (dynamicColliders[i].Intersects(dynamicColliders[j], out contact))
                    {
                        tmpContacts.Add(contact.Value);
                    }
                }
                for (int j = 0; j < staticColliders.Count; j++)
                {
                    if (!dynamicColliders[i].bounds.Intersects(staticColliders[j].bounds))
                        continue;
                    XContact? contact;
                    if (dynamicColliders[i].Intersects(staticColliders[j], out contact))
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
