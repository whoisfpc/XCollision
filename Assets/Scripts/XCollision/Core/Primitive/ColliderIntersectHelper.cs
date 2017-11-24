﻿using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Mathf = UnityEngine.Mathf;
using Quaternion = UnityEngine.Quaternion;

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
            // 反向旋转sphere的位置，使得可以在cube的坐标系下进行碰撞判断
            var extents = src.Size * 0.5f;
            var invQ = Quaternion.Inverse(src.Quaternion);
            var invP = invQ * (dst.Position - src.Position);

            // 以下所有操作都是在cube的坐标系下，随后的实际方向需要进行坐标系转换
            var n = invP;
            var closest = n;
            closest.x = Mathf.Clamp(closest.x, -extents.x, extents.x);
            closest.y = Mathf.Clamp(closest.y, -extents.y, extents.y);
            closest.z = Mathf.Clamp(closest.z, -extents.z, extents.z);
            var inside = false;

            if (n == closest)
            {
                inside = true;
                var disX = extents.x - Mathf.Abs(n.x);
                var disY = extents.y - Mathf.Abs(n.y);
                var disZ = extents.z - Mathf.Abs(n.z);
                //找到最近的一个面
                if (disX < disY && disX < disZ)
                {
                    // 沿X轴
                    if (n.x > 0)
                        closest.x = extents.x;
                    else
                        closest.x = -extents.x;
                }
                else if (disY < disX && disY < disZ)
                {
                    // 沿Y轴
                    if (n.y > 0)
                        closest.y = extents.y;
                    else
                        closest.y = -extents.y;
                }
                else
                {
                    // 沿Z轴
                    if (n.z > 0)
                        closest.z = extents.z;
                    else
                        closest.z = -extents.z;
                }

            }
            var dir = n - closest;
            var sqrDist = dir.sqrMagnitude;
            var space = dst.Radius;
            var sqrSpace = space * space;
            if (sqrDist < sqrSpace || inside)
            {
                var dist = Mathf.Sqrt(sqrDist);
                var normal = (src.Quaternion * dir).normalized;
                var penetration = space - dist;
                if (inside)
                {
                    normal = -normal;
                    penetration = space + dist;
                }
                if (normal == Vector3.zero)
                    normal = Vector3.up;
                contact = new XContact(src, dst, normal, penetration);
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
            var n = dst.Position - src.Position;

            Vector3 closest = n;
            var extents = src.bounds.Extents;
            closest.x = Mathf.Clamp(closest.x, -extents.x, extents.x);
            closest.y = Mathf.Clamp(closest.y, -extents.y, extents.y);
            closest.z = Mathf.Clamp(closest.z, -extents.z, extents.z);

            var v = new Vector2(closest.x, closest.z);
            if (v.sqrMagnitude > src.Radius * src.Radius)
            {
                v = v.normalized * src.Radius;
                closest.x = v.x;
                closest.z = v.y;
            }

            bool inside = false;

            if (n == closest)
            {
                inside = true;
                // 往上下移动的最短距离
                var dist1 = extents.y - Mathf.Abs(closest.y);
                // 往水平四周移动的最短距离
                var dist2 = src.Radius - v.magnitude;
                
                if (dist1 < dist2)
                {
                    closest.y = closest.y > 0 ? extents.y : -extents.y;
                }
                else
                {
                    v = v.normalized * src.Radius;
                    closest.x = v.x;
                    closest.z = v.y;
                }
            }

            var dir = n - closest;
            var sqrDist = dir.sqrMagnitude;
            var space = dst.Radius;
            var sqrSpace = space * space;
            if (sqrDist < sqrSpace || inside)
            {
                var dist = Mathf.Sqrt(sqrDist);
                var normal = dir.normalized;
                var penetration = space - dist;
                if (inside)
                {
                    normal = -normal;
                    penetration = space + dist;
                }
                if (normal == Vector3.zero)
                    normal = Vector3.up;
                contact = new XContact(src, dst, normal, penetration);
                return true;
            }
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
