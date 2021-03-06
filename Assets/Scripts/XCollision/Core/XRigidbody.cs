﻿using Vector3 = UnityEngine.Vector3;

namespace XCollision.Core
{
    public class XRigidbody
    {
        public float Mass
        {
            get { return mass; }
            set
            {
                if (float.IsInfinity(value) || IsStatic)
                    mass = 0;
                else
                    mass = value;
                InverseMass = mass == 0 ? 0 : 1 / mass;
            }
        }
        public float InverseMass { get; private set; }
        public bool IsStatic { get; private set; }

        public Vector3 position;
        public Vector3 velocity;
        private float mass = 1;
        private Vector3 totalForce;

        public XRigidbody(Vector3 position, bool isStatic = false)
        {
            this.position = position;
            IsStatic = isStatic;
            if (IsStatic)
            {
                InverseMass = 0;
            }
            else
            {
                InverseMass = 1;
            }
        }

        public void AddForce(Vector3 force)
        {
            totalForce += force;
        }

        public void Update(float dt)
        {
            var acc = totalForce * InverseMass;
            velocity += acc * dt;
            position += velocity * dt;
            totalForce = Vector3.zero;
        }
    }
}
