using System;
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

#if UNITY_EDITOR
        [SerializeField]
        private bool _drawSkeletonGizmos = true;
        
        [SerializeField]
        private bool _drawPhysicsGizmos = true;
#endif
        
        #endregion
        
        private SkeletonService _skeletonService;
        private PhysicsService _physicsService;
        
        public void Awake()
        {
            _skeletonService = new(_skeletonSettings);
            _physicsService = new(_physicsSettings, _skeletonService);
        }

        public void Start()
        {
            
        }

        public void OnDestroy()
        {
            
        }

        public void Update()
        {
            var deltaTime = Time.deltaTime;
            
        }

        public void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            
            _physicsService.FixedTick(deltaTime);
        }
        

#if UNITY_EDITOR

        public void OnValidate()
        {
            if (Application.isPlaying)
            {
                return;
            }
            
            _skeletonService = new(_skeletonSettings);
            _physicsService = new(_physicsSettings, _skeletonService);
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
