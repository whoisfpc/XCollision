using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;

namespace XCollision.Core
{
    /// <summary>
    /// XPhysics system, contain main loop
    /// </summary>
    public class XPhysics
    {
        public Vector3 gravity = Vector3.down * 9.8f;

        private IList<XCollider> colliders = new List<XCollider>();

        public void AddCollider(XCollider collider)
        {
            colliders.Add(collider);
        }

        public void RemoveCollider(XCollider collider)
        {

        }

        public void Update(float dt)
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                colliders[i].AddForce(gravity);
                colliders[i].Update(dt);
            }
        }
    }
}
