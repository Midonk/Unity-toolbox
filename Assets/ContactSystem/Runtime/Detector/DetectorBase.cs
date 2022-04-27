using System;
using System.Collections.Generic;
using UnityEngine;

namespace ContactSystem
{
    public abstract class DetectorBase : MonoBehaviour
    {
        #region Exposed

        [Header("Filter")]
        [SerializeField] protected LayerTagFilter _filter;

        #endregion


        #region Public Properties

        public GameObject[] Elements => _detectedElements.ToArray();  
            
        #endregion


        #region Events

        public event Action<GameObject> ElementDetected;
        public event Action<GameObject> ElementLost;
            
        #endregion


        #region Main

        /// <summary>
        ///     Handle the case where a valid element has been detected
        /// </summary>
        /// <param name="element">Detected element</param>
        protected void Detect(GameObject element)
        {
            _detectedElements.Add(element);
            ElementDetected?.Invoke(element);
        }
            
        /// <summary>
        ///     Handle the case where a valid element has been lost
        /// </summary>
        /// <param name="element">Lost element</param>
        protected void Lose(GameObject element)
        {
            _detectedElements.Remove(element);
            ElementLost?.Invoke(element);
        }

        #endregion


        #region Plumbery

        protected void TryAdd(GameObject element)
        {
            if(_detectedElements.Contains(element)) return;

            Detect(element);
        }

        protected void TryRemove(GameObject element)
        {
            if(!_detectedElements.Contains(element)) return;

            Lose(element);
        }
            
        #endregion


        #region Utils

        /// <summary>
        ///     Clear the detected elements list
        /// </summary>
        public void Clear()
        {
            _detectedElements.Clear();
        }

        #endregion


        #region Private Fields

        private List<GameObject> _detectedElements = new List<GameObject>();
            
        #endregion
    }
}