using Vector3 = UnityEngine.Vector3;

namespace XCollision.Core
{
    /// <summary>
    /// AABB(axis-aligned bounding box) for pre collision detect
    /// </summary>
    public class Bounds
    {
        public Vector3 Center
        {
            get
            {
                return center;
            }
            set
            {
                center = value;
                max = center + extents;
                min = center - extents;
            }
        }
        public Vector3 Extents
        {
            get
            {
                return extents;
            }
            set
            {
                extents = value;
                max = center + extents;
                min = center - extents;
                size = extents * 2;
            }
        }
        public Vector3 Max
        {
            get
            {
                return max;
            }
            set
            {
                max = value;
                center = (max + min) / 2;
                extents = (max - min) / 2;
                size = extents * 2;
            }
        }
        public Vector3 Min
        {
            get
            {
                return min;
            }
            set
            {
                min = value;
                center = (max + min) / 2;
                extents = (max - min) / 2;
                size = extents * 2;
            }
        }
        public Vector3 Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                extents = size / 2;
                max = center + extents;
                min = center - extents;
            }
        }

        private Vector3 center;
        private Vector3 extents;
        private Vector3 max;
        private Vector3 min;
        private Vector3 size;

        public Bounds() : this(Vector3.zero, Vector3.zero)
        {
        }

        public Bounds(Vector3 center, Vector3 size)
        {
            this.center = center;
            this.size = size;
            extents = size / 2;
            max = center + extents;
            min = center - extents;
        }
    }
}
