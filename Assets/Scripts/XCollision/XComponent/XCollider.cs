using UnityEngine;

namespace XCollision.XComponent
{
    public abstract class XCollider : MonoBehaviour
    {
        public static Color colliderColor = new Color(0.451f, 0.855f, 0);
        public static Color boundsColor = Color.yellow;

        protected Core.XCollider col;

        protected virtual void Start()
        {
            
        }

        protected virtual void Update()
        {
            if (col != null)
                transform.position = col.position;
        }

        protected virtual void OnDrawGizmosSelected()
        {
            
        }
    }
}
