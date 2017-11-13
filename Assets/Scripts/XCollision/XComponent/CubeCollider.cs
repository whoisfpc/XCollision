using UnityEngine;

namespace XCollision.XComponent
{
    public class CubeCollider : XCollider
    {
        public Vector3 size;

        protected override void Start()
        {
            col = new Core.CubeCollider(transform.position, size);
            XPhysicsProxy.Instance.AddCollider(col);
        }

        protected override void OnDrawGizmosSelected()
        {
            var colorBackup = Gizmos.color;
            Gizmos.color = colliderColor;
            var backup = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;

            Gizmos.DrawWireCube(Vector3.zero, size);

            Gizmos.color = colorBackup;
            Gizmos.matrix = backup;
        }
    }
}
