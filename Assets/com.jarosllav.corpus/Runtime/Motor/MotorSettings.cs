using System;
using UnityEngine;

namespace Corpus.Motor
{
    [Serializable]
    public class MotorSettings
    {
        [SerializeField]
        private Transform _transform;
        
        [SerializeField]
        private float _speed = 10f;
        
        public Transform Transform => _transform;
        public float Speed => _speed;
    }
}