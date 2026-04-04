using System;
using UnityEngine;
using Corpus.Skeleton;
using UnityEngine.Serialization;

namespace Corpus.Physics
{
    [Serializable]
    public class PhysicsSettings
    {
        [SerializeField]
        private BoneDefinition _feetBoneDefinition;
        
        [SerializeField]
        private BoneDefinition _headBoneDefinition;

        [SerializeField]
        private LayerMask _groundLayerMask;
        
        [SerializeField]
        private LayerMask _collisionLayerMask;
        
        [SerializeField]
        private float _groundCheckRadius;
        
        [SerializeField]
        private Vector3 _groundCheckOffset;
        
        [SerializeField]
        private float _groundSkinWidth;
        
        [SerializeField]
        private float _gravityModifier;
        
        public BoneDefinition FeetBoneDefinition => _feetBoneDefinition;
        public BoneDefinition HeadBoneDefinition => _headBoneDefinition;
        public LayerMask GroundLayerMask => _groundLayerMask;
        public LayerMask CollisionLayerMask => _collisionLayerMask;
        public float GroundCheckRadius => _groundCheckRadius;
        public Vector3 GroundCheckOffset => _groundCheckOffset;
        public float GroundSkinWidth => _groundSkinWidth;
        public float GravityModifier => _gravityModifier;
    }
}