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
        public bool useGravity = true;

        private IList<XCollider> colliders = new List<XCollider>();

        public void AddCollider(XCollider collider)
        {
            colliders.Add(collider);
        }

        public void RemoveCollider(XCollider collider)
        {
            throw new System.NotImplementedException();
        }

        public void Update(float dt)
        {
            for (int i = 0; i < colliders.Count; i++)
            {
                if (useGravity)
                    colliders[i].AddForce(gravity);
                colliders[i].Update(dt);
            }

            for (int i = 0; i < colliders.Count; i++)
            {
                for (int j = i + 1; j < colliders.Count; j++)
                {
                    // collider detect i & j
                    if (!colliders[i].bounds.Intersects(colliders[j].bounds))
                        continue;
                    colliders[i].Intersects(colliders[j]);
                }
            }
        }
    }
}
