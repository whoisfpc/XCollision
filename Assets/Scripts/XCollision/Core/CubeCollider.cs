using Vector3 = UnityEngine.Vector3;

namespace XCollision.Core
{
    public class CubeCollider : XCollider
    {
        private Vector3 size;

        public CubeCollider(Vector3 position, Vector3 size) : base(position)
        {
            this.size = size;
            velocity = Vector3.up * 10;
        }
    }
}
