using System.Collections.Generic;
using UnityEngine;

namespace ContactSystem
{
    [RequireComponent(typeof(Collider))]
    public class Detector<T> : ContactBase where T : Object
    {
        #region Public Properties

        public T[] Elements => _detectedElements.ToArray();  
            
        #endregion


        #region Events

        public delegate void DetectionHandler(T element);
        public event DetectionHandler OnElementDetected;
        public event DetectionHandler OnElementLost;
            
        #endregion


        #region Unity API

        private void OnTriggerEnter(Collider other) 
        {
            var gameObject = other.gameObject;
            if(!PassFilter(gameObject)) return;

            TryAdd(gameObject);
        }
        
        private void OnTriggerExit(Collider other) 
        {
            var gameObject = other.gameObject;
            if(!PassFilter(gameObject)) return;

            TryRemove(gameObject);
        }

        #endregion


        #region Main

        private void TryAdd(GameObject gameObject)
        {
            var element = gameObject.GetComponent<T>();
            if(!element)
            {
                Debug.LogWarning($"Missing {typeof(T)} on the detected object '{element}'", this);
                return;
            }
            
            if(_detectedElements.Contains(element)) return;

            Detect(element);
        }

        private void TryRemove(GameObject gameObject)
        {
            var element = gameObject.GetComponent<T>();
            if(!_detectedElements.Contains(element)) return;

            Lose(element);
        }

        private void Detect(T element)
        {
            _detectedElements.Add(element);
            OnElementDetected?.Invoke(element);
        }
            
        private void Lose(T element)
        {
            _detectedElements.Remove(element);
            OnElementLost?.Invoke(element);
        }

        public void Clear()
        {
            _detectedElements.Clear();
        }

        #endregion


        #region Private Fields

        [SerializeField]
        private List<T> _detectedElements = new List<T>();
            
        #endregion
    }
}