using UnityEngine;

namespace XCollision.XComponent
{
    public abstract class XCollider : MonoBehaviour
    {
        public static Color colliderColor = new Color(0.451f, 0.855f, 0);
        public static Color boundsColor = Color.yellow;

        public bool showBounds;
        [Range(0, 1)]
        public float restitution;
        public float mass = 1;
        protected Core.XCollider col;

        public Core.XRigidbody XRigidbody
        {
            get { return col.rigidbody; }
        }

        protected virtual void Awake()
        {
            
        }

        protected virtual void Update()
        {
            if (col != null)
                transform.position = col.Position;
        }

        protected virtual void OnDrawGizmosSelected()
        {
            
        }
    }
}
