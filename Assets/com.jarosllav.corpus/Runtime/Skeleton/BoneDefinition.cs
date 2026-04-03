using System;
using UnityEngine;

namespace Corpus.Skeleton
{
    [CreateAssetMenu(menuName = "Corpus/Skeleton/Bone Definition")]
    public class BoneDefinition : ScriptableObject
    {
        [SerializeField]
        private int _id;
        
        [SerializeField]
        private string _code = string.Empty;
        
        public int ID => _id;
        public string Code => _code;
    }
}