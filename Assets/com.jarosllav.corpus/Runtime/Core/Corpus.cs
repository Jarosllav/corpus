using System;
using Corpus.Motor;
using Corpus.Physics;
using Corpus.Skeleton;
using UnityEngine;

namespace Corpus.Core
{
    [ExecuteInEditMode]
    public class Corpus : MonoBehaviour
    {
        #region Inspector
        
        [SerializeField]
        private SkeletonSettings _skeletonSettings;
        
        [SerializeField]
        private PhysicsSettings _physicsSettings;

        [SerializeField]
        private MotorSettings _motorSettings;
        
#if UNITY_EDITOR
        [SerializeField]
        private bool _drawSkeletonGizmos = true;
        
        [SerializeField]
        private bool _drawPhysicsGizmos = true;
#endif
        
        #endregion
        
        private SkeletonService _skeletonService;
        private PhysicsService _physicsService;
        private MotorService _motorService;
        
        public void Awake()
        {
            _skeletonService = new(_skeletonSettings);
            _physicsService = new(_physicsSettings, _skeletonService);
            _motorService = new(_motorSettings, _physicsService);
        }

        public void Start()
        {
            
        }

        public void OnDestroy()
        {
            
        }

        public void Update()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return;
            }
#endif
            var deltaTime = Time.deltaTime;
            
        }

        public void FixedUpdate()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return;
            }
#endif
            var deltaTime = Time.fixedDeltaTime;
            
            _physicsService.FixedTick(deltaTime);
            _motorService.FixedTick(deltaTime);
        }

#if UNITY_EDITOR

        public void OnValidate()
        {
            if (Application.isPlaying)
            {
                return;
            }
            
            _skeletonService = new(_skeletonSettings);
            _inputService = new(_inputSettings, new InputReader());
            _physicsService = new(_physicsSettings, _skeletonService);
            _motorService = new(_motorSettings, _physicsService);
        }

        public void OnDrawGizmos()
        {
            if (_drawSkeletonGizmos) SkeletonService.DrawGizmos(_skeletonService, _skeletonSettings);
            if (_drawPhysicsGizmos) PhysicsService.DrawGizmos(_physicsService, _physicsSettings);
        }

        public void OnDrawGizmosSelected()
        {
            if (_drawSkeletonGizmos) SkeletonService.DrawGizmos(_skeletonService, _skeletonSettings, true);
            if (_drawPhysicsGizmos) PhysicsService.DrawGizmos(_physicsService, _physicsSettings, true);
        }
        
#endif
    }
}
