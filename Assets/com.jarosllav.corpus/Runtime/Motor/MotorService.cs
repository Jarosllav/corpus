using System;
using UnityEngine;
using Corpus.Physics;

namespace Corpus.Motor
{
    public class MotorService
    {
        private readonly MotorSettings _settings;
        private readonly PhysicsService _physicsService;
        
        private Vector3 _inverseMotion = Vector3.zero;

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
            
            _inverseMotion = _settings.Transform.InverseTransformDirection(worldDirection);
        }

        private void ApplyMotion(Vector3 displacement)
        {
            var position = _settings.Transform.position;
            
            if (_physicsService.ProbeCollisions(displacement))
            {
                
            }

            position += displacement;
            
            if (_physicsService.IsGrounded)
            {
                position.y = _physicsService.GroundLevel;
            }
            
            _settings.Transform.position = position;
        }
    }
}