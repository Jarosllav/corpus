using System;
using UnityEngine;

namespace Corpus.Skeleton
{
    [Serializable]
    public class SkeletonSettings
    {
        [SerializeField]
        private BoneReference[] _bones;
        
        public BoneReference[] Bones => _bones;
    }
}