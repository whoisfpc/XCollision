using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Mathf = UnityEngine.Mathf;
using Quaternion = UnityEngine.Quaternion;
using Debug = UnityEngine.Debug;

namespace XCollision.Core
{
    public static class ColliderIntersectHelper
    {
        private static bool ProjectOverlapTest(Vector3 axis, Vector3[] srcPoints, Vector3[] dstPoints, out float overlap)
        {
            float srcMin = float.MaxValue;
            float dstMin = float.MaxValue;
            float srcMax = float.MinValue;
            float dstMax = float.MinValue;

            for (int i = 0; i < srcPoints.Length; i++)
            {
                var p = Vector3.Dot(axis, srcPoints[i]);
                if (p < srcMin)
                    srcMin = p;
                if (p > srcMax)
                    srcMax = p;
            }
            for (int i = 0; i < dstPoints.Length; i++)
            {
                var p = Vector3.Dot(axis, dstPoints[i]);
                if (p < dstMin)
                    dstMin = p;
                if (p > dstMax)
                    dstMax = p;
            }
            if (srcMax < dstMin ||srcMin > dstMax)
            {
                overlap = 0;
                return false;
            }
            overlap = Mathf.Min(srcMax, dstMax) - Mathf.Max(srcMin, dstMin);
            if ((srcMax > dstMax && srcMin < dstMin) || (srcMax < dstMax && srcMin > dstMin))
            {
                var min = Mathf.Abs(srcMin - dstMin);
                var max = Mathf.Abs(srcMax - dstMax);
                overlap += Mathf.Min(min, max);
            }
            return true;
        }

        private static bool SATTest(CubeXCollider src, CubeXCollider dst, out Vector3 normal, out float penetration)
        {
            var srcPoints = new Vector3[4];
            var dstPoints = new Vector3[4];
            var srcExtents = src.Size * 0.5f;
            var dstExtents = dst.Size * 0.5f;
            srcPoints[0] = new Vector3(-srcExtents.x, 0, srcExtents.z);
            srcPoints[1] = new Vector3(srcExtents.x, 0, srcExtents.z);
            srcPoints[2] = new Vector3(srcExtents.x, 0, -srcExtents.z);
            srcPoints[3] = new Vector3(-srcExtents.x, 0, -srcExtents.z);
            srcPoints[0] = src.Quaternion * srcPoints[0] + src.Position;
            srcPoints[1] = src.Quaternion * srcPoints[1] + src.Position;
            srcPoints[2] = src.Quaternion * srcPoints[2] + src.Position;
            srcPoints[3] = src.Quaternion * srcPoints[3] + src.Position;
            srcPoints[0].y = srcPoints[1].y = srcPoints[2].y = srcPoints[3].y = 0;

            dstPoints[0] = new Vector3(-dstExtents.x, 0, dstExtents.z);
            dstPoints[1] = new Vector3(dstExtents.x, 0, dstExtents.z);
            dstPoints[2] = new Vector3(dstExtents.x, 0, -dstExtents.z);
            dstPoints[3] = new Vector3(-dstExtents.x, 0, -dstExtents.z);
            dstPoints[0] = dst.Quaternion * dstPoints[0] + dst.Position;
            dstPoints[1] = dst.Quaternion * dstPoints[1] + dst.Position;
            dstPoints[2] = dst.Quaternion * dstPoints[2] + dst.Position;
            dstPoints[3] = dst.Quaternion * dstPoints[3] + dst.Position;
            dstPoints[0].y = dstPoints[1].y = dstPoints[2].y = dstPoints[3].y = 0;

            var axis = new Vector3[4];
            axis[0] = src.Quaternion * Vector3.forward;
            axis[1] = src.Quaternion * Vector3.right;
            axis[2] = dst.Quaternion * Vector3.forward;
            axis[3] = dst.Quaternion * Vector3.right;

            float minOverlap = float.MaxValue;
            Vector3 minAxis = axis[0];
            float overlap;
            for (int i = 0; i < axis.Length; i++)
            {
                if (!ProjectOverlapTest(axis[i], srcPoints, dstPoints, out overlap))
                {
                    normal = Vector3.zero;
                    penetration = 0;
                    return false;
                }
                else
                {
                    if (overlap < minOverlap)
                    {
                        minOverlap = overlap;
                        minAxis = axis[i];
                    }
                }
            }
            normal = minAxis;
            penetration = minOverlap;
            return true;
        }

        public static bool Intersect(CubeXCollider src, CubeXCollider dst, out XContact? contact)
        {
            Vector3 normal;
            float penetration;
            if (!SATTest(src, dst, out normal, out penetration))
            {
                contact = null;
                return false;
            }
            var srcExtents = src.Size * 0.5f;
            var dstExtents = dst.Size * 0.5f;
            var srcMinY = src.Position.y - srcExtents.y;
            var srcMaxY = src.Position.y + srcExtents.y;
            var dstMinY = dst.Position.y - dstExtents.y;
            var dstMaxY = dst.Position.y + dstExtents.y;

            var overlapY = Mathf.Min(srcMaxY, dstMaxY) - Mathf.Max(srcMinY, dstMinY);
            if ((srcMaxY > dstMaxY && srcMinY < dstMinY) || (srcMaxY < dstMaxY && srcMinY > dstMinY))
            {
                var min = Mathf.Abs(srcMinY - dstMinY);
                var max = Mathf.Abs(srcMaxY - dstMaxY);
                overlapY += Mathf.Min(min, max);
            }

            if (overlapY < penetration)
            {
                penetration = overlapY;
                normal = Vector3.up;
            }

            if (Vector3.Dot(normal, dst.Position-src.Position) < 0)
            {
                normal = -normal;
            }

            contact = new XContact(src, dst, normal, penetration);
            return true;
        }

        public static bool Intersect(CubeXCollider src, CylinderXCollider dst, out XContact? contact)
        {
            var invP = Quaternion.Inverse(src.Quaternion) * (dst.Position - src.Position);
            Vector3 n = Vector3.zero;
            n.x = invP.x;
            n.z = invP.z;

            var extents = src.Size * 0.5f;
            var halfHa = extents.y;
            var topA = halfHa;
            var bottomA = -halfHa;
            var halfHb = dst.Height * 0.5f;
            var topB = invP.y + halfHb;
            var bottomB = invP.y - halfHb;

            var space = dst.Radius;
            var sqrSpace = space * space;

            // 相撞时，俯视图下的圆和矩形必然相交
            var closest = n;
            closest.x = Mathf.Clamp(closest.x, -extents.x, extents.x);
            closest.z = Mathf.Clamp(closest.z, -extents.z, extents.z);

            if ((n-closest).sqrMagnitude > sqrSpace)
            {
                contact = null;
                return false;
            }

            // 处理相交的情况
            Vector3 normal;
            float penetration;
            float verticalP = float.PositiveInfinity;
            float horizontalP;

            var inside = false;
            if (n == closest)
            {
                inside = true;
                var disX = extents.x - Mathf.Abs(n.x);
                var disZ = extents.z - Mathf.Abs(n.z);
                //找到最近的一个面
                if (disX < disZ)
                {
                    // 沿X轴
                    if (n.x > 0)
                        closest.x = extents.x;
                    else
                        closest.x = -extents.x;
                }
                else
                {
                    // 沿Z轴
                    if (n.z > 0)
                        closest.z = extents.z;
                    else
                        closest.z = -extents.z;
                }
                horizontalP = space + (n - closest).magnitude;
            }
            else
            {
                horizontalP = space - (n - closest).magnitude;
            }

            if (Mathf.Sign(topA - topB) != Mathf.Sign(bottomB - bottomA))
            {
                // 斜向相撞
                if (topB > topA)
                {
                    verticalP = topA - bottomB;
                }
                else
                {
                    verticalP = topB - bottomA;
                }
            }

            if (horizontalP < verticalP)
            {
                normal = (src.Quaternion * (n-closest)).normalized;
                if (inside)
                    normal = -normal;
                penetration = horizontalP;
            }
            else
            {
                normal = topB > topA ? Vector3.up : Vector3.down;
                penetration = verticalP;
            }
            if (normal == Vector3.zero)
                normal = Vector3.up;
            contact = new XContact(src, dst, normal, penetration);
            return true;
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

        // 没有处理在内部的情况
        public static bool Intersect(CylinderXCollider src, CylinderXCollider dst, out XContact? contact)
        {
            var space = src.Radius + dst.Radius;
            var sqrSpace = space * space;
            Vector3 n = Vector3.zero;
            n.x = dst.Position.x - src.Position.x;
            n.z = dst.Position.z - src.Position.z;
            if (n.sqrMagnitude > sqrSpace)
            {
                contact = null;
                return false;
            }

            var halfHa = src.Height * 0.5f;
            var topA = src.Position.y + halfHa;
            var bottomA = src.Position.y - halfHa;
            var halfHb = dst.Height * 0.5f;
            var topB = dst.Position.y + halfHb;
            var bottomB = dst.Position.y - halfHb;

            Vector3 normal;
            float penetration;

            if (Mathf.Sign(topA-topB) == Mathf.Sign(bottomB-bottomA))
            {
                // 水平相撞
                normal = n.normalized;
                penetration = space - n.magnitude;
                
            }
            else if (n.sqrMagnitude < Mathf.Pow(dst.Radius-src.Radius, 2f))
            {
                // 垂直相撞
                if (dst.Position.y > src.Position.y)
                {
                    normal = Vector3.up;
                    penetration = topA - bottomB;
                }
                else
                {
                    normal = Vector3.down;
                    penetration = topB - bottomA;
                }
            }
            else
            {
                var horizontalP = space - n.magnitude;
                float verticalP;
                // 斜向相撞
                if (topB > topA)
                {
                    verticalP = topA - bottomB;
                }
                else
                {
                    verticalP = topB - bottomA;
                }

                if (horizontalP < verticalP)
                {
                    normal = n.normalized;
                    penetration = horizontalP;
                }
                else
                {
                    normal = topB > topA ? Vector3.up : Vector3.down;
                    penetration = verticalP;
                }
            }
            if (normal == Vector3.zero)
                normal = Vector3.up;
            contact = new XContact(src, dst, normal, penetration);
            return true;
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
