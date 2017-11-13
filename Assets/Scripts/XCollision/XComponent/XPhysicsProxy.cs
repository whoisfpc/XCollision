using UnityEngine;
using XCollision.Core;

namespace XCollision.XComponent
{
    public class XPhysicsProxy : MonoBehaviour
    {
        public static XPhysics Instance
        {
            get
            {
                if (instance == null)
                    instance = new XPhysics();
                return instance;
            }
        }
        private static XPhysics instance;
        private float lastFixTime;

        private void Awake()
        {
            if (instance == null)
                instance = new XPhysics();
            lastFixTime = Time.time;
        }

        private void FixedUpdate()
        {
            var time = Time.fixedTime;
            Instance.Update(time - lastFixTime);
            lastFixTime = time;
        }
    }
}
