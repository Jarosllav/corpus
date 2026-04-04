using System;
using Corpus.Extensions;
using Corpus.Skeleton;
using UnityEngine;

namespace Corpus.Physics
{
    public class ProbeResult
    {
        public Vector3 Normal;
    }
    
    public class PhysicsService
    {
        public const float TERMINAL_VELOCITY = -90.0f; // m/s
        
        private readonly PhysicsSettings _settings;
        private readonly BoneReference _feetBone;
        private readonly BoneReference _headBone;
        
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _displacement = Vector3.zero;
        private float _groundLevel = 0f;
        private bool _isGrounded = false;
        
        public Vector3 Velocity => _velocity;
        public float GroundLevel => _groundLevel;
        public bool IsGrounded => _isGrounded;
        
        public PhysicsService(PhysicsSettings settings, SkeletonService skeletonService)
        {
            _settings = settings;
            _feetBone = skeletonService.GetBone(_settings.FeetBoneDefinition);
            _headBone = skeletonService.GetBone(_settings.HeadBoneDefinition);
        }

        public void FixedTick(float deltaTime)
        {
            if (_feetBone == null)
            {
                return;
            }
            
            PreProbeGround();
            ApplyGravity(deltaTime);
            
            _displacement = _velocity * deltaTime;
            
            ProbeGround(_displacement);
        }
        
        public bool ProbeCollisions(Vector3 worldDirection, out ProbeResult result)
        {
            var feetOrigin = GetFeetOrigin();
            var headOrigin = GetHeadOrigin();

            if (UnityEngine.Physics.CapsuleCast(feetOrigin, headOrigin, _settings.GroundCheckRadius,
                    worldDirection.normalized, out var hit, worldDirection.magnitude, _settings.CollisionLayerMask))
            {
                result = new()
                {
                    Normal = hit.normal
                };
                
                return true;
            }
            
            result = null;
            return false;
        }
        
        private void PreProbeGround()
        {
            var origin = GetFeetOrigin();
            
            _isGrounded = UnityEngine.Physics.SphereCast(
                origin,
                _settings.GroundCheckRadius,
                Vector3.down,
                out var hit,
                _settings.GroundSkinWidth,
                _settings.GroundLayerMask,
                QueryTriggerInteraction.Ignore);
        }

        private void ProbeGround(Vector3 displacement)
        {
            _isGrounded = UnityEngine.Physics.SphereCast(GetFeetOrigin(), _settings.GroundCheckRadius, 
                displacement.normalized, out var hit, displacement.magnitude - _settings.GroundCheckRadius, _settings.GroundLayerMask);

            if (_isGrounded)
            {
                _groundLevel = hit.point.y + _settings.GroundSkinWidth;
            }
        }

        private void ApplyGravity(float deltaTime)
        {
            if (_isGrounded && _velocity.y <= 0f)
            {
                _velocity.y = 0f;
                return;
            }
            
            var gravity = UnityEngine.Physics.gravity.y * _settings.GravityModifier;
            _velocity.y += gravity * deltaTime;
            _velocity.y = Mathf.Max(_velocity.y, TERMINAL_VELOCITY);
        }
        
        private Vector3 GetFeetOrigin()
        {
            return _feetBone.Transform.position + _settings.GroundCheckOffset;
        }

        private Vector3 GetHeadOrigin()
        {
            return _headBone.Transform.position;
        }
        
#if UNITY_EDITOR

        public static void DrawGizmos(PhysicsService service, PhysicsSettings settings, bool asSelected = false)
        {
            if (service?._feetBone == null || service?._headBone == null)
            {
                return;
            }
            
            var p1 = service._feetBone.Transform.position + settings.GroundCheckOffset;
            var p2 = service._headBone.Transform.position;
            GizmosExtensions.DrawWireCapsule(p1, p2, settings.GroundCheckRadius);
            
            /*Gizmos.DrawWireSphere(service.GetFeetOrigin(), settings.GroundCheckRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(service.GetFeetOrigin() + Vector3.down * settings.GroundSkinWidth, settings.GroundCheckRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(service.GetFeetOrigin() - Vector3.down * settings.GroundSkinWidth, settings.GroundCheckRadius);*/
        }
        
#endif
    }
}