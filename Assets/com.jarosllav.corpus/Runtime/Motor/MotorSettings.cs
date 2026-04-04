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
        private bool _slideOnWalls = true;
        
        [SerializeField]
        private float _speed = 10f;
        
        public Transform Transform => _transform;
        public bool SlideOnWalls => _slideOnWalls;
        public float Speed => _speed;
    }
}