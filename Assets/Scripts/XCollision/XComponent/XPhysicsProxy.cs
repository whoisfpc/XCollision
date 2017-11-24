using UnityEngine;
using XCollision.Core;

namespace XCollision.XComponent
{
    public class XPhysicsProxy : MonoBehaviour
    {
        public static XPhysics XPhysics
        {
            get
            {
                if (xPhysics == null)
                    xPhysics = new XPhysics();
                return xPhysics;
            }
        }

        public static XPhysicsProxy Instance { get; private set; }
        public bool alwaysShowBounds = true;
        public bool alwaysShowCollider = true;
        public bool useGravity;
        private static XPhysics xPhysics;
        private float lastFixTime;

        private void Awake()
        {
            Instance = this;
            if (xPhysics == null)
                xPhysics = new XPhysics();
            xPhysics.useGravity = useGravity;
            lastFixTime = Time.time;
        }

        private void Update()
        {
            xPhysics.useGravity = useGravity;
        }

        private void FixedUpdate()
        {
            var time = Time.fixedTime;
            XPhysics.Update(time - lastFixTime);
            lastFixTime = time;
        }
    }
}
