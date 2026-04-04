using System;
using UnityEngine;
using Corpus.Physics;

namespace Corpus.Motor
{
    public class MotorService
    {
        private const int COLLISION_PROBE_ITERATIONS = 2;
        
        private readonly MotorSettings _settings;
        private readonly PhysicsService _physicsService;
        
        private Vector3 _motion = Vector3.zero;
        private Vector3 _inverseMotion = Vector3.zero;
        
        public Vector3 InverseMotion => _inverseMotion;
        public Vector3 Motion => _motion;

        public MotorService(MotorSettings settings, PhysicsService physicsService)
        {
            _settings = settings;
            _physicsService = physicsService;
        }

        public void FixedTick(float deltaTime)
        {
            var displacement = _physicsService.Velocity * deltaTime;

            Move(displacement);
        }

        public void Move(Vector2 direction)
        {
            var worldDirection = direction.x * _settings.Transform.right 
                                 + direction.y * _settings.Transform.forward;
            Move(worldDirection.normalized);
        }
        
        public void Move(Vector3 worldDirection)
        {
            var motion = _settings.Speed * Time.deltaTime * worldDirection;

            ApplyMotion(motion);
        }

        private void ApplyMotion(Vector3 displacement)
        {
            var position = _settings.Transform.position;

            for (int i = 0; i < COLLISION_PROBE_ITERATIONS; ++i)
            {
                if (_physicsService.ProbeCollisions(displacement, out var result))
                {
                    if (_settings.SlideOnWalls)
                    {
                        displacement -= Vector3.Dot(displacement, result.Normal) * result.Normal;
                    }
                    else
                    {
                        displacement.x = 0f;
                        displacement.z = 0f;
                    }
                }
            }

            position += displacement;
            
            if (_physicsService.IsGrounded)
            {
                position.y = _physicsService.GroundLevel;
            }

            _settings.Transform.position = position;
            
            _motion = displacement;
            _inverseMotion = _settings.Transform.InverseTransformDirection(displacement);
        }
    }
}