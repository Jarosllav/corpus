using System;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

#endif

namespace Corpus.Skeleton
{
    public class SkeletonService
    {
        private readonly SkeletonSettings _settings;
        
        public SkeletonService(SkeletonSettings settings)
        {
            _settings = settings;    
        }

        public BoneReference GetBone(int id)
        {
            foreach (var bone in _settings.Bones)
            {
                if (bone.Definition.ID == id)
                {
                    return bone;
                }
            }
            
            return null;
        }
        
        public BoneReference GetBone(string code)
        {
            foreach (var bone in _settings.Bones)
            {
                if (bone.Definition.Code == code)
                {
                    return bone;
                }
            }
            
            return null;
        }

        public BoneReference GetBone(BoneDefinition definition)
        {
            foreach (var bone in _settings.Bones)
            {
                if (bone.Definition == definition)
                {
                    return bone;
                }
            }
            
            return null;
        }
        
#if UNITY_EDITOR

        public static void DrawGizmos(SkeletonService service, SkeletonSettings settings, bool asSelected = false)
        {
            foreach (var bone in settings.Bones)
            {
                Handles.Label(bone.Transform.position, bone.Definition.Code);
            }
        }
        
#endif
    }
}