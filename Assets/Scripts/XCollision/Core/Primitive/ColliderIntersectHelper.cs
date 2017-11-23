﻿using Vector3 = UnityEngine.Vector3;
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
            //TMP: 只判断和顶面是否相交
            var topP = src.Position;
            topP.y += src.Size.y * 0.5f;
            var dist = Vector3.Dot(Vector3.up, dst.Position - topP);
            var sqrDist = dist * dist;
            var sqrSpace = dst.Radius * dst.Radius;
            if (sqrDist < sqrSpace)
            {
                contact = new XContact(src, dst, Vector3.up, dst.Radius - dist);
                return true;
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
            var sqrDist = (src.Position - dst.Position).sqrMagnitude;
            var sqrSpace = (src.Radius + dst.Radius);
            sqrSpace *= sqrSpace;
            if (sqrDist < sqrSpace)
            {
                contact = new XContact(src, dst, (dst.Position-src.Position).normalized, Mathf.Sqrt(sqrSpace)-Mathf.Sqrt(sqrDist));
                return true;
            }
            contact = null;
            return false;
        }
    }
}
