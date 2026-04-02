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

#if UNITY_EDITOR
        [SerializeField]
        private bool _drawSkeletonGizmos = true;
#endif
        
        #endregion
        
        private SkeletonService _skeletonService;
        
        public void Awake()
        {
            _skeletonService = new(_skeletonSettings);
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
            var deltaTime = Time.fixedDeltaTime;
        }
        
#if UNITY_EDITOR

        public void OnDrawGizmos()
        {
            if (_drawSkeletonGizmos) SkeletonService.DrawGizmos(_skeletonService, _skeletonSettings);
        }

        public void OnDrawGizmosSelected()
        {
            if (_drawSkeletonGizmos) SkeletonService.DrawGizmos(_skeletonService, _skeletonSettings, true);
        }
        
#endif
    }
}
