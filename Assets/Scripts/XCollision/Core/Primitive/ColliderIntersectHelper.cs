using Vector3 = UnityEngine.Vector3;
using Mathf = UnityEngine.Mathf;

namespace XCollision.Core
{
    public static class ColliderIntersectHelper
    {
        public static bool Intersect(CubeXCollider src, CubeXCollider dst, out XContact? contact)
        {
            contact = null;
            return false;
        }

        public static bool Intersect(CubeXCollider src, CylinderXCollider dst, out XContact? contact)
        {
            contact = null;
            return false;
        }

        public static bool Intersect(CubeXCollider src, SphereXCollider dst, out XContact? contact)
        {
            //TMP: 只判断和顶面是否相交, 没有处理边界
            var topP = src.Position;
            topP.y += src.Size.y * 0.5f;
            var dist = Vector3.Dot(Vector3.up, dst.Position - topP);
            if (dist < dst.Radius)
            {
                contact = new XContact(src, dst, Vector3.up, dst.Radius - dist);
                return true;
            }
            // TMP: 球的半径在
            if (dst.Position.y <= topP.y && dst.Position.y >= topP.y - src.Size.y)
            {

            }
            contact = null;
            return false;
        }

        public static bool Intersect(CylinderXCollider src, CubeXCollider dst, out XContact? contact)
        {
            return Intersect(dst, src, out contact);
        }

        public static bool Intersect(CylinderXCollider src, CylinderXCollider dst, out XContact? contact)
        {
            contact = null;
            return false;
        }

        public static bool Intersect(CylinderXCollider src, SphereXCollider dst, out XContact? contact)
        {
            contact = null;
            return false;
        }

        public static bool Intersect(SphereXCollider src, CubeXCollider dst, out XContact? contact)
        {
            return Intersect(dst, src, out contact);
        }

        public static bool Intersect(SphereXCollider src, CylinderXCollider dst, out XContact? contact)
        {
            return Intersect(dst, src, out contact);
        }

        public static bool Intersect(SphereXCollider src, SphereXCollider dst, out XContact? contact)
        {
            var dir = dst.Position - src.Position;
            var sqrDist = dir.sqrMagnitude;
            var space = src.Radius + dst.Radius;
            var sqrSpace = space * space;
            if (sqrDist < sqrSpace)
            {
                var dist = Mathf.Sqrt(sqrDist);
                if (dist != 0)
                {
                    contact = new XContact(src, dst, dir.normalized, space - dist);
                }
                else
                {
                    contact = new XContact(src, dst, Vector3.up, src.Radius);
                }
                return true;
            }
            contact = null;
            return false;
        }
    }
}
