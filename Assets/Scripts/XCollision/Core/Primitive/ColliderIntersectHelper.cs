using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCollision.Core
{
    public static class ColliderIntersectHelper
    {
        public static void Intersect(CubeXCollider src, CubeXCollider dst)
        {

        }

        public static void Intersect(CubeXCollider src, CylinderXCollider dst)
        {

        }

        public static void Intersect(CubeXCollider src, SphereXCollider dst)
        {

        }

        public static void Intersect(CylinderXCollider src, CubeXCollider dst)
        {
            Intersect(dst, src);
        }

        public static void Intersect(CylinderXCollider src, CylinderXCollider dst)
        {

        }

        public static void Intersect(CylinderXCollider src, SphereXCollider dst)
        {

        }

        public static void Intersect(SphereXCollider src, CubeXCollider dst)
        {
            Intersect(dst, src);
        }

        public static void Intersect(SphereXCollider src, CylinderXCollider dst)
        {
            Intersect(dst, src);
        }

        public static void Intersect(SphereXCollider src, SphereXCollider dst)
        {

        }
    }
}
