using UnityEngine;

namespace ContactSystem
{
    [RequireComponent(typeof(Collider))]
    public class Detector : DetectorBase
    {
        #region Unity API
        
        private void OnTriggerEnter(Collider other) 
        {
            var gameObject = other.gameObject;
            if(!_filter.Pass(gameObject)) return;

            TryAdd(gameObject);
        }
        
        private void OnTriggerExit(Collider other) 
        {
            var gameObject = other.gameObject;
            if(!_filter.Pass(gameObject)) return;

            TryRemove(gameObject);
        }

        #endregion
    }
}